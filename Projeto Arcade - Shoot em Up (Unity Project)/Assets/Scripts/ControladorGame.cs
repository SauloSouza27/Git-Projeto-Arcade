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
    public GameObject barraHP, prefabCoracaoHP, barraXP, jogador;
    public TextMeshProUGUI txtNivel, txtXP;
    private Slider sliderXP;
    // spawn inimigos
    public GameObject spawnsInimigoPequeno;
    public List<GameObject> spawnsCima, spawnsLaterais, spawnsBaixo;
    // Power UP
    public GameObject uiGameOver, uiVitoria, uiPowerUP, armaPrincipal, armaDouble, armaTriple, buttonSubirNivel, buttonArmaDouble,
        buttonArmaTriple, buttonArmaPet, buttonArmaOrbeGiratorio, buttonArmaSerra, buttonDiminuiCooldown;
    public bool armaPrincipalAtivada = true, armaDoubleAtivada = false, armaTripleAtivada = false, armaPetAtivada = false, armaOrbeGiratorioAtivada = false, armaSerraAtivada = false;
    private int contadorMaxVelocidadeAtaque = 0;
    private readonly float multVelAtak = 0.75f;
    // inimigos Morcego Drone
    public GameObject droneCimaBaixo, droneEsqDirHori, droneDirEsqTrans, droneEsqDirTrans, droneCruzado, droneCascadeDirEsqTrans, droneCascadeDirEsqHori;
    // inimigos Piramide
    public GameObject piramideSuperiores, piramideLaterais;
    // boss
    public GameObject boss, petsBoss;
    public Button Configuracao;

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
            Configuracao.onClick.Invoke();
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
        
    }

    private void ControlaFase1()
    {
        if (nivel == 2)
        {
            StartCoroutine(AtivaInimigo(droneCimaBaixo, 3.0f));
        }

        if (nivel == 4)
        {
            StartCoroutine(AtivaInimigo(droneEsqDirHori, 4.0f));
        }

        if (nivel == 5)
        {
            StartCoroutine(AtivaInimigo(droneDirEsqTrans, 5.0f));
            StartCoroutine(AtivaInimigo(droneEsqDirTrans, 12.0f));
        }

        if (nivel == 6)
        {
            StartCoroutine(AtivaInimigo(piramideSuperiores, 4.0f));
        }

        if (nivel == 7)
        {
            StartCoroutine(AtivaInimigo(droneCruzado, 4.0f));
        }

        if (nivel == 8)
        {
            StartCoroutine(AtivaInimigo(droneCascadeDirEsqTrans, 4.0f));
            StartCoroutine(AtivaInimigo(droneCascadeDirEsqHori, 24.0f));
        }

        if (nivel == 9)
        {
            //foreach (GameObject go in spawnsBaixo)
            //{
            //    go.SetActive(true);
            //}
            StartCoroutine(AtivaInimigo(piramideLaterais, 4.0f));
        }

        if (nivel == 10)
        {
            spawnsInimigoPequeno.SetActive(false);
            StartCoroutine(AtivaInimigo(boss, 6.0f));
            StartCoroutine(AtivaInimigo(petsBoss, 16.0f));
        }

        if (boss.GetComponent<MovimentoBoss>().bossIsDead == true)
        {
            StartCoroutine(AtivaMenuVitoria(uiVitoria, 4.0f));
        }
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
    public IEnumerator AtivaInimigo(GameObject inimgo, float delay)
    {
        yield return new WaitForSeconds(delay);
        inimgo.SetActive(true);
    }

    private IEnumerator AtivaMenuVitoria(GameObject uiVitoria, float delay)
    {
        yield return new WaitForSeconds(delay);
        uiVitoria.SetActive(true);
        Time.timeScale = 0.0f;
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
        if(contadorMaxVelocidadeAtaque < 6)
        {
            jogador.GetComponent<DisparoArma>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaDouble>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaTriple>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaPet>().cooldown *= multVelAtak;
            uiPowerUP.SetActive(false);
            Time.timeScale = 1.0f;
            AtivaSpawnInimigosPequenos();
            contadorMaxVelocidadeAtaque++;
        }
        if(contadorMaxVelocidadeAtaque >= 6)
        {
            jogador.GetComponent<DisparoArma>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaDouble>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaTriple>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaPet>().cooldown *= multVelAtak;
            Destroy(buttonDiminuiCooldown);
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
