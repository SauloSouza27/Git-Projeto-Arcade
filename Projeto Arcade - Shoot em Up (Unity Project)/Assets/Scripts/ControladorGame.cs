using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControladorGame : MonoBehaviour
{
    public static ControladorGame instancia;

    // HP, XP e nivel
    public int XP, XPtotal = 0, nivel = 1, HP;
    public float valorXPNivel = 100.0f, multiplicadorQuantidadeXPporNivel = 50.0f;
    // HUD
    public GameObject barraHP, prefabCoracaoHP, barraXP, jogador;
    public TextMeshProUGUI txtNivel, txtXP;
    private Slider sliderXP;
    public Button configuracao;
    // spawn inimigos
    public GameObject spawnsInimigoPequeno;
    private bool jaAtrasouSpawn = false;
    public List<GameObject> spawnsCima, spawnsLaterais, spawnsBaixo;
    // Power UP
    public GameObject uiGameOver, uiVitoria, uiPowerUP, armaPrincipal, armaDouble, armaTriple, buttonSubirNivel, buttonArmaDouble,
        buttonArmaTriple, buttonArmaPet, buttonPetDiminuiCooldown, buttonArmaOrbeGiratorio, buttonUpgradeOrbe1, buttonVelocidadeOrbe, buttonArmaSerra, buttonDiminuiCooldown;
    public bool armaPrincipalAtivada = true, armaDoubleAtivada = false, armaTripleAtivada = false, armaPetAtivada = false, armaOrbeGiratorioAtivada = false, upgradeOrbe1 = false, armaSerraAtivada = false;
    private int contadorMaxVelocidadeAtaque = 0, contadorMaxVelocidadeAtaquePet = 0, contadorMaxVelocidadeOrbe = 0;
    private readonly float multVelAtak = 0.75f;
    // inimigos Morcego Drone
    public GameObject nivel2, nivel3, nivel4, nivel5, nivel7, nivel8;
    // inimigos Piramide
    public GameObject piramideSuperiores, piramideLaterais;
    // boss fase1
    public GameObject boss, petsBoss;
    // spawns inimigos Rola Bosta
    public GameObject rolaBostaDir, rolaBostaEsq;

    private void Awake()
    {
        sliderXP = barraXP.GetComponent<Slider>();
        txtNivel = GameObject.Find("txtNivel").GetComponent<TextMeshProUGUI>();
        txtXP = GameObject.Find("txtXP").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        Time.timeScale = 1;

        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    void Update()
    {
        if (Time.timeScale == 0) return;

        HP = jogador.GetComponent<ControlaPersonagem>().pontosVida;
        AtualizaBarraHP();

        if (Input.GetButtonDown("Subir de Nivel") && buttonSubirNivel.activeSelf)
        {
            SubirNivel();
        }

        if (Input.GetButtonDown("Configuracao"))
        {
            configuracao.onClick.Invoke();
        }

        if (HP <= 0)
        {
            jogador.GetComponent<ControlaPersonagem>().MorteJogador();
            uiGameOver.SetActive(true);
        }

        if(SceneManager.GetActiveScene().name == "Fase 1")
        {
            ControlaFase1();
        }
        
        if (SceneManager.GetActiveScene().name == "Fase 2")
        {
            ControlaFase2();
        }
    }

    private void ControlaFase1()
    {
        if (nivel == 2)
        {
            StartCoroutine(AtivaInimigo(nivel2, 3.0f));
        }

        if (nivel == 3)
        {
            StartCoroutine(AtivaInimigo(nivel3, 3.0f));
        }

        if (nivel == 4)
        {
            StartCoroutine(AtivaInimigo(nivel4, 4.0f));
        }

        if (nivel == 5)
        {
            StartCoroutine(AtivaInimigo(nivel5, 5.0f));
        }

        if (nivel == 6)
        {
            StartCoroutine(AtivaInimigo(piramideSuperiores, 4.0f));
        }

        if (nivel == 7)
        {
            StartCoroutine(AtivaInimigo(nivel7, 4.0f));
        }

        if (nivel == 8)
        {
            StartCoroutine(AtivaInimigo(nivel8, 4.0f));
            if (!jaAtrasouSpawn)
            {
                StartCoroutine(AtrasaSpawnInimigosPequenos(spawnsInimigoPequeno, 10.0f));
                jaAtrasouSpawn = true;
            }
        }

        if (nivel == 9)
        {
            StartCoroutine(AtivaInimigo(piramideLaterais, 4.0f));
        }

        if (nivel == 10)
        {
            spawnsInimigoPequeno.SetActive(false);
            StartCoroutine(AtivaInimigo(boss, 4.0f));
            StartCoroutine(AtivaInimigo(petsBoss, 22.0f));
        }

        if (boss.GetComponent<MovimentoBoss>().bossIsDead == true)
        {
            StartCoroutine(AtivaMenuVitoria(uiVitoria, 3.0f));
        }
    }

    private void ControlaFase2()
    {

    }

    public void AtualizaBarraHP()
    {
        int numCoracoes = barraHP.transform.childCount;
        if (HP < numCoracoes)
        {
            Destroy(barraHP.transform.GetChild(numCoracoes - 1).gameObject);
        }
        if (HP > numCoracoes)
        {
            Instantiate(prefabCoracaoHP, barraHP.transform);
        }
    }

    public void SomaXP(int xpInimigo)
    {
        XP += xpInimigo;
        AtualizaBarraXP();
    }

    private void AtualizaBarraXP()
    {
        sliderXP.value = (XP / valorXPNivel);

        if (XP >= valorXPNivel && nivel < 10)
        {
            sliderXP.value = sliderXP.maxValue;
            buttonSubirNivel.SetActive(true);
            spawnsInimigoPequeno.SetActive(false);
        }
        txtNivel.text = "Nivel: " + nivel;
        txtXP.text = XP + "/" + valorXPNivel;
    }
    public void SubirNivel()
    {
        XPtotal += XP;
        if (XP == valorXPNivel)
        {
            XP = 0;
        }
        if (XP > valorXPNivel)
        {
            XP -= (int)valorXPNivel;
        }
        nivel += 1;
        valorXPNivel += nivel * multiplicadorQuantidadeXPporNivel;
        uiPowerUP.SetActive(true);
        Time.timeScale = 0.0f;
        buttonSubirNivel.SetActive(false);

        AtualizaBarraXP();
    }
    private void AtivaSpawnInimigosPequenos()
    {
        spawnsInimigoPequeno.SetActive(true);
    }

    private IEnumerator AtrasaSpawnInimigosPequenos(GameObject spawnsInimigosPequenos, float delay)
    {
        spawnsInimigosPequenos.SetActive(false);
        yield return new WaitForSeconds(delay);
        spawnsInimigosPequenos.SetActive(true);
    }
    public IEnumerator AtivaInimigo(GameObject inimgo, float delay)
    {
        yield return new WaitForSeconds(delay);
        inimgo.SetActive(true);
        yield break;
    }

    private IEnumerator AtivaMenuVitoria(GameObject uiVitoria, float delay)
    {
        yield return new WaitForSeconds(delay);
        uiVitoria.SetActive(true);
        Time.timeScale = 0.0f;
        yield break;
    }

    // powerUp buttons
    public void PowerUPAtivaArmaPet(GameObject armaPet)
    {
        armaPet.SetActive(true);
        armaPetAtivada = true;
        jogador.GetComponent<DisparoArmaPet>().enabled = true;
        Destroy(buttonArmaPet);
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
    }
    public void PowerUPAtivaArmaOrbeGiratorio(GameObject armaOrbeGiratorio)
    {
        armaOrbeGiratorio.SetActive(true);
        armaOrbeGiratorioAtivada = true;
        jogador.GetComponent<RespostaOrbeGiratorio>().enabled = true;
        Destroy(buttonArmaOrbeGiratorio);
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
    }
    public void PowerUPUpgradeArmaOrbeGiratorio(GameObject segundoOrbe)
    {
        segundoOrbe.SetActive(true);
        upgradeOrbe1 = true;
        Destroy(buttonUpgradeOrbe1);
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
    }

    public void PowerUPUpgradeVelocidadeOrbe()
    {
        if (contadorMaxVelocidadeOrbe < 3)
        {
            jogador.GetComponent<ControlaPersonagem>().velocidadeRotacaoOrbeGiratorio *= 1.25f;
            contadorMaxVelocidadeOrbe++;
            uiPowerUP.SetActive(false);
            Time.timeScale = 1.0f;
            AtivaSpawnInimigosPequenos();
        }
        if (contadorMaxVelocidadeOrbe >= 3)
        {
            jogador.GetComponent<ControlaPersonagem>().velocidadeRotacaoOrbeGiratorio *= 1.25f;
            Destroy(buttonVelocidadeOrbe);
            uiPowerUP.SetActive(false);
            Time.timeScale = 1.0f;
            AtivaSpawnInimigosPequenos();
        }
    }

    public void PowerUPAtivaArmaSerra(GameObject armaOrbeArmaSerra)
    {
        armaOrbeArmaSerra.SetActive(true);
        armaSerraAtivada = true;
        jogador.GetComponent<DisparoArmaSerra>().enabled = true;
        Destroy(buttonArmaSerra);
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
    }
    public void PowerUPArmaDouble()
    {
        armaDouble.SetActive(true);
        armaPrincipal.SetActive(false);
        jogador.GetComponent<DisparoArma>().enabled = false;
        jogador.GetComponent<DisparoArmaDouble>().enabled = true;
        armaPrincipalAtivada = false;
        armaDoubleAtivada = true;
        Destroy(buttonArmaDouble);
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
    }
    public void PowerUPArmaTriple()
    {
        armaTriple.SetActive(true);
        armaDouble.SetActive(false);
        jogador.GetComponent<DisparoArmaDouble>().enabled = false;
        jogador.GetComponent<DisparoArmaTriple>().enabled = true;
        armaDoubleAtivada = false;
        armaTripleAtivada = true;
        Destroy(buttonArmaTriple);
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
    }
    public void PowerUPDiminuiCooldownArmaPrincipal()
    {
        if(contadorMaxVelocidadeAtaque < 3)
        {
            jogador.GetComponent<DisparoArma>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaDouble>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaTriple>().cooldown *= multVelAtak;
            contadorMaxVelocidadeAtaque++;
            uiPowerUP.SetActive(false);
            Time.timeScale = 1.0f;
            AtivaSpawnInimigosPequenos();
        }
        if(contadorMaxVelocidadeAtaque >= 3)
        {
            jogador.GetComponent<DisparoArma>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaDouble>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaTriple>().cooldown *= multVelAtak;
            Destroy(buttonDiminuiCooldown);
            uiPowerUP.SetActive(false);
            Time.timeScale = 1.0f;
            AtivaSpawnInimigosPequenos();
        }
        
    }
    public void PowerUPDiminuiCooldownArmaPet()
    {
        if (contadorMaxVelocidadeAtaquePet < 3)
        {
            jogador.GetComponent<DisparoArmaPet>().cooldown *= multVelAtak;
            contadorMaxVelocidadeAtaquePet++;
            uiPowerUP.SetActive(false);
            Time.timeScale = 1.0f;
            AtivaSpawnInimigosPequenos();
        }
        if (contadorMaxVelocidadeAtaquePet >= 3)
        {
            jogador.GetComponent<DisparoArmaPet>().cooldown *= multVelAtak;
            Destroy(buttonPetDiminuiCooldown);
            uiPowerUP.SetActive(false);
            Time.timeScale = 1.0f;
            AtivaSpawnInimigosPequenos();
        }
    }
    public void PowerUPAumentaHP()
    {
        jogador.GetComponent<ControlaPersonagem>().pontosVida += 1;
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
    }
}
