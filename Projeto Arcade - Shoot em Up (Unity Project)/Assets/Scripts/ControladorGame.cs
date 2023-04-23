using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorGame : MonoBehaviour
{
    public static ControladorGame instancia;

    // XP e nivel
    public int XP, nivel = 1, HP;
    public float multiplicadorQuantidadeXPporNivel = 50.0f, valorXPNivel = 100.0f;
    public GameObject barraHP, barraXP, jogador;
    public TextMeshProUGUI txtNivel, txtXP;
    private Slider sliderXP, sliderHP;
    // spawn inimigos
    public List<GameObject> spawnsCima, spawnsLaterais, spawnsBaixo;
    private GameObject[] spawnInimigoPiramide, spawnsInimigoPequeno;
    private bool paraSpawnInimigoPiramide = false;
    // Power UP
    public GameObject uiGameOver, uiPowerUP, armaPrincipal, armaDouble, armaTriple, buttonSubirNivel, buttonArmaDouble,
        buttonArmaTriple, buttonArmaPet, buttonArmaOrbeGiratorio, buttonArmaSerra, buttonDiminuiCooldown;
    public bool armaPrincipalAtivada = true, armaDoubleAtivada = false, armaTripleAtivada = false, armaPetAtivada = false, armaOrbeGiratorioAtivada = false, armaSerraAtivada = false;
    private int contadorMaxVelocidadeAtaque = 0;
    private readonly float multVelAtak = 0.75f;
    // inimigos Morcego Drone
    public GameObject droneCimaBaixo, droneEsqDirHori, droneDirEsqTrans, droneEsqDirTrans, droneCruzado, droneCascadeDirEsqTrans, droneCascadeDirEsqHori;
    // inimigos Piramide
    public GameObject piramideLaterais;

    private void Awake()
    {
        sliderHP = barraHP.GetComponent<Slider>();
        sliderXP = barraXP.GetComponent<Slider>();
        txtNivel = GameObject.Find("txtNivel").GetComponent<TextMeshProUGUI>();
        txtXP = GameObject.Find("txtXP").GetComponent<TextMeshProUGUI>();
        //busca pontos de spawn na cena
        spawnInimigoPiramide = GameObject.FindGameObjectsWithTag("SpawnInimigoPiramide");
        spawnsInimigoPequeno = GameObject.FindGameObjectsWithTag("SpawnInimigoPequeno");
        foreach (GameObject go in spawnsBaixo)
        {
            go.SetActive(false);
        }
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
        AtualizaBarraHP(HP);

        if (sliderHP.value > 0)
        {
            AtualizaBarraXP();
        }
        if (sliderHP.value <= 0)
        {
            jogador.GetComponent<ControlaPersonagem>().MorteJogador();
            uiGameOver.SetActive(true);
        }

        if (nivel == 2)
        {
            StartCoroutine(AtivaMorcegoDrone(droneCimaBaixo, 3f));
        }

        if(nivel == 4)
        {
            StartCoroutine(AtivaMorcegoDrone(droneEsqDirHori, 4f));
        }

        if(nivel == 5)
        {
            StartCoroutine(AtivaMorcegoDrone(droneDirEsqTrans, 5f));
            StartCoroutine(AtivaMorcegoDrone(droneEsqDirTrans, 12f));
        }

        if (nivel == 6)
        {
            foreach (GameObject go in spawnInimigoPiramide)
            {
                SpawnInimigoPiramide spawn = go.GetComponent<SpawnInimigoPiramide>();
                if (paraSpawnInimigoPiramide == false)
                {
                    spawn.AtivaSpawnInimigoPiramide();
                }
            }
            paraSpawnInimigoPiramide = true;
        }

        if (nivel == 7)
        {
            StartCoroutine(AtivaMorcegoDrone(droneCruzado, 4f));
        }

        if (nivel == 8)
        {
            StartCoroutine(AtivaMorcegoDrone(droneCascadeDirEsqTrans, 4f));
            StartCoroutine(AtivaMorcegoDrone(droneCascadeDirEsqHori, 12f));
        }

        if (nivel == 9)
        {
            foreach(GameObject go in spawnsBaixo)
            {
                go.SetActive(true);
            }
            StartCoroutine(AtivaMorcegoDrone(piramideLaterais, 4f));
        }
    }

    public void AtualizaBarraHP(int hpAtual)
    {
        sliderHP.value = hpAtual;
    }

    public void SomaXP(int xpInimigo)
    {
        XP += xpInimigo;
        sliderXP.value += xpInimigo;
    }

    private void AtualizaBarraXP()
    {
        
        int valorMaxSlider = (int)sliderXP.maxValue;
        int valorSlider = (int)sliderXP.value;
        if (valorSlider >= valorMaxSlider)
        {
            sliderXP.value = valorMaxSlider;
            XP = (int)valorXPNivel;
            buttonSubirNivel.SetActive(true);
            foreach (GameObject a in spawnsInimigoPequeno)
            {
                a.SetActive(false);
            }
        }
        txtNivel.text = "Nivel: " + nivel;
        txtXP.text = XP + "/" + valorXPNivel;
    }
    public void SubirNivel()
    {
        nivel += 1;
        sliderXP.value = 0;
        sliderXP.maxValue += (nivel * multiplicadorQuantidadeXPporNivel);
        valorXPNivel += sliderXP.maxValue;
        uiPowerUP.SetActive(true);
        Time.timeScale = 0.0f;
        buttonSubirNivel.SetActive(false);
    }
    private void AtivaSpawnInimigosPequenos()
    {
        foreach(GameObject go in spawnsInimigoPequeno)
        {
            go.SetActive(true);
        }
    }
    private IEnumerator AtivaMorcegoDrone(GameObject morcegoDrone, float delay)
    {
        yield return new WaitForSeconds(delay);
        morcegoDrone.SetActive(true);
    }
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
        if(contadorMaxVelocidadeAtaque < 3)
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
        if(contadorMaxVelocidadeAtaque >= 3)
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
