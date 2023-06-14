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
    public Button configuracao, sairConfig;
    // spawn inimigos
    public GameObject spawnsInimigoPequeno;
    public List<GameObject> spawnsCima, spawnsLaterais, spawnsBaixo;
    // Power UP
    public GameObject menuConfig, uiGameOver, uiVitoria, uiPowerUP, escudoCD, serraCD, armaPrincipal, armaDouble, armaTriple, escudo, buttonSubirNivel, buttonArmaDouble,
        buttonArmaTriple, buttonDiminuiCooldown, buttonEscudo, buttonUPEscudo, buttonArmaPet, buttonPetDiminuiCooldown, buttonArmaOrbeGiratorio, buttonUpgradeOrbe1, buttonVelocidadeOrbe, buttonArmaSerra;
    public bool armaPrincipalAtivada = true, armaDoubleAtivada = false, armaTripleAtivada = false, armaPetAtivada = false, defesaEscudoAtivada = false, armaOrbeGiratorioAtivada = false, upgradeOrbe1 = false, armaSerraAtivada = false;
    private int contadorMaxUpEscudo = 0, contadorMaxVelocidadeAtaque = 0, contadorMaxVelocidadeAtaquePet = 0, contadorMaxVelocidadeOrbe = 0;
    private readonly float multVelAtak = 0.75f;
    // inimigos Morcego Drone
    public GameObject nivel1, nivel2, nivel3, nivel4, nivel5, nivel6, nivel7, nivel8, nivel9, nivel10;
    // inimigos Piramide
    public GameObject piramideSuperiores, piramideLaterais;
    // boss fase1
    public GameObject bossFase1, petsBossFase1;

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
        if (Input.GetButtonDown("Configuracao"))
        {
            if (!menuConfig.activeSelf)
            {
                configuracao.onClick.Invoke();
            }
            else if (menuConfig.activeSelf)
            {
                sairConfig.onClick.Invoke();
            }
        }

        if (Time.timeScale == 0) return;

        HP = jogador.GetComponent<ControlaPersonagem>().pontosVida;
        AtualizaBarraHP();

        if (Input.GetButtonDown("Subir de Nivel") && buttonSubirNivel.activeSelf)
        {
            SubirNivel();
        }

        if (HP <= 0)
        {
            jogador.GetComponent<ControlaPersonagem>().MorteJogador();
            uiGameOver.SetActive(true);
        }
    }
    public void ControladorNiveisFases()
    {
        if (SceneManager.GetActiveScene().name == "Fase 1")
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
        }

        if (nivel == 9)
        {
            StartCoroutine(AtivaInimigo(piramideLaterais, 4.0f));
        }

        if (nivel == 10)
        {
            spawnsInimigoPequeno.SetActive(false);
            StartCoroutine(AtivaInimigo(bossFase1, 1.0f));
            StartCoroutine(AtivaInimigo(petsBossFase1, 22.0f));
        }
    }

    private void ControlaFase2()
    {
        if (nivel == 2)
        {
            nivel1.SetActive(false);
            StartCoroutine(AtivaInimigo(nivel2, 3.0f));
        }
        if (nivel == 3)
        {
            nivel2.SetActive(false);
            StartCoroutine(AtivaInimigo(nivel3, 3.0f));
        }
        if (nivel == 4)
        {
            nivel3.SetActive(false);
            StartCoroutine(AtivaInimigo(nivel4, 2.0f));
        }
        if (nivel == 5)
        {
            nivel4.SetActive(false);
            StartCoroutine(AtivaInimigo(nivel5, 3.0f));
        }
        if (nivel == 6)
        {
            nivel5.SetActive(false);
            StartCoroutine(AtivaInimigo(nivel6, 4.0f));
        }
        if (nivel == 7)
        {
            nivel6.SetActive(false);
            StartCoroutine(AtivaInimigo(nivel7, 3.0f));
        }
        if (nivel == 8)
        {
            nivel7.SetActive(false);
            StartCoroutine(AtivaInimigo(nivel8, 3.0f));
        }
        if (nivel == 9)
        {
            nivel8.SetActive(false);
            StartCoroutine(AtivaInimigo(nivel9, 4.0f));
        }
        if (nivel == 10)
        {
            nivel9.SetActive(false);
            StartCoroutine(AtivaInimigo(nivel10, 2.0f));
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
            if (SceneManager.GetActiveScene().name == "Fase 1")
            {
                spawnsInimigoPequeno.SetActive(false);
            }
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
        if (nivel < 10 && SceneManager.GetActiveScene().name == "Fase 1")
        {
            spawnsInimigoPequeno.SetActive(true);
        }
    }

    public IEnumerator AtivaInimigo(GameObject inimgo, float delay)
    {
        yield return new WaitForSeconds(delay);
        inimgo.SetActive(true);
        yield break;
    }

    // powerUp buttons
    public void PowerUPAtivaArmaPet(GameObject armaPet)
    {
        armaPet.SetActive(true);
        armaPetAtivada = true;
        jogador.GetComponent<DisparoArmaPet>().enabled = true;
        Destroy(buttonArmaPet);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }
    public void PowerUPAtivaDefesaEscudo(GameObject defesaEscudo)
    {
        defesaEscudo.SetActive(true);
        escudoCD.SetActive(true);
        defesaEscudoAtivada = true;
        Destroy(buttonEscudo);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }
    public void PowerUPRecargaDefesaEscudo()
    {
        if (contadorMaxUpEscudo < 3)
        {
            escudo.GetComponent<Escudo>().respawnTime -= 1.5f;
            contadorMaxUpEscudo++;
        }
        if (contadorMaxUpEscudo >= 3)
        {
            escudo.GetComponent<Escudo>().respawnTime -= 1.5f;
            Destroy(buttonUPEscudo);
        }
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }
    public void PowerUPAtivaArmaOrbeGiratorio(GameObject armaOrbeGiratorio)
    {
        armaOrbeGiratorio.SetActive(true);
        armaOrbeGiratorioAtivada = true;
        jogador.GetComponent<RespostaOrbeGiratorio>().enabled = true;
        Destroy(buttonArmaOrbeGiratorio);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }
    public void PowerUPUpgradeArmaOrbeGiratorio(GameObject orbeTriplo)
    {
        orbeTriplo.SetActive(true);
        jogador.GetComponent<ControlaPersonagem>().armaOrbeGiratorio.SetActive(false);
        upgradeOrbe1 = true;
        Destroy(buttonUpgradeOrbe1);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }

    public void PowerUPUpgradeVelocidadeOrbe()
    {
        if (contadorMaxVelocidadeOrbe < 3)
        {
            jogador.GetComponent<ControlaPersonagem>().velocidadeRotacaoOrbeGiratorio *= 1.25f;
            contadorMaxVelocidadeOrbe++;
        }
        if (contadorMaxVelocidadeOrbe >= 3)
        {
            jogador.GetComponent<ControlaPersonagem>().velocidadeRotacaoOrbeGiratorio *= 1.25f;
            Destroy(buttonVelocidadeOrbe);
        }
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }

    public void PowerUPAtivaArmaSerra(GameObject armaOrbeArmaSerra)
    {
        armaOrbeArmaSerra.SetActive(true);
        serraCD.SetActive(true);
        armaSerraAtivada = true;
        jogador.GetComponent<DisparoArmaSerra>().enabled = true;
        Destroy(buttonArmaSerra);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
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
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }
    public void PowerUPArmaTriple()
    {
        armaTriple.SetActive(true);
        armaDouble.SetActive(false);
        jogador.GetComponent<DisparoArmaDouble>().enabled = false;
        jogador.GetComponent<DisparoArmaTriple>().enabled = true;
        armaDoubleAtivada = false;
        armaTripleAtivada = true;
        jogador.GetComponent<ControlaPersonagem>().danoArmaPrincipal = 1;
        Destroy(buttonArmaTriple);
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }
    public void PowerUPDiminuiCooldownArmaPrincipal()
    {
        if(contadorMaxVelocidadeAtaque < 3)
        {
            jogador.GetComponent<DisparoArma>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaDouble>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaTriple>().cooldown *= multVelAtak;
            contadorMaxVelocidadeAtaque++;
        }
        if(contadorMaxVelocidadeAtaque >= 3)
        {
            jogador.GetComponent<DisparoArma>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaDouble>().cooldown *= multVelAtak;
            jogador.GetComponent<DisparoArmaTriple>().cooldown *= multVelAtak;
            Destroy(buttonDiminuiCooldown);
        }
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }
    public void PowerUPDiminuiCooldownArmaPet()
    {
        if (contadorMaxVelocidadeAtaquePet < 3)
        {
            jogador.GetComponent<DisparoArmaPet>().cooldown *= multVelAtak;
            contadorMaxVelocidadeAtaquePet++;
        }
        if (contadorMaxVelocidadeAtaquePet >= 3)
        {
            jogador.GetComponent<DisparoArmaPet>().cooldown *= multVelAtak;
            Destroy(buttonPetDiminuiCooldown);
        }
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }
    public void PowerUPAumentaHP()
    {
        jogador.GetComponent<ControlaPersonagem>().pontosVida += 1;
        Time.timeScale = 1.0f;
        AtivaSpawnInimigosPequenos();
        uiPowerUP.SetActive(false);
    }
}
