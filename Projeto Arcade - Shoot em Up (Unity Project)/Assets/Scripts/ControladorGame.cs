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
    private GameObject[] spawnInimigoPequeno, spawnInimigoPiramide;
    private bool paraSpawnInimigoPiramide = false;
    // inimigos piramide
    public GameObject droneBaixoCima;
    // Power UP
    public GameObject uiGameOver, uiPowerUP, buttonSubirNivel, buttonArmaPet, buttonArmaOrbeGiratorio, buttonArmaSerra;
    public bool armaPetAtivada = false, armaOrbeGiratorioAtivada = false, armaSerraAtivada = false;

    private void Awake()
    {
        sliderHP = barraHP.GetComponent<Slider>();
        sliderXP = barraXP.GetComponent<Slider>();
        txtNivel = GameObject.Find("txtNivel").GetComponent<TextMeshProUGUI>();
        txtXP = GameObject.Find("txtXP").GetComponent<TextMeshProUGUI>();
        //busca pontos de spawn na cena
        spawnInimigoPequeno = GameObject.FindGameObjectsWithTag("SpawnInimigoPequeno");
        spawnInimigoPiramide = GameObject.FindGameObjectsWithTag("SpawnInimigoPiramide");
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
            droneBaixoCima.SetActive(true);
        }

        if (nivel == 5)
        {
            foreach (GameObject go in spawnInimigoPiramide)
            {
                SpawnInimigoPiramide spawn = go.GetComponent<SpawnInimigoPiramide>();
                if(paraSpawnInimigoPiramide == false)
                {
                    spawn.AtivaSpawnInimigoPiramide();
                }
            }
            paraSpawnInimigoPiramide = true;
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
    public void PowerUPAtivaArmaPet(GameObject armaPet)
    {
        armaPet.SetActive(true);
        armaPetAtivada = true;
        jogador.GetComponent<DisparoArmaPet>().enabled = true;
        Destroy(buttonArmaPet);
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void PowerUPAtivaArmaOrbeGiratorio(GameObject armaOrbeGiratorio)
    {
        armaOrbeGiratorio.SetActive(true);
        armaOrbeGiratorioAtivada = true;
        jogador.GetComponent<RespostaOrbeGiratorio>().enabled = true;
        Destroy(buttonArmaOrbeGiratorio);
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PowerUPAtivaArmaSerra(GameObject armaOrbeArmaSerra)
    {
        armaOrbeArmaSerra.SetActive(true);
        armaSerraAtivada = true;
        jogador.GetComponent<DisparoArmaSerra>().enabled = true;
        Destroy(buttonArmaSerra);
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void PowerUPAumentaDanoArmaPrincipal()
    {
        jogador.GetComponent<DisparoArma>().danoArmaPrincipal += 1;
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void PowerUPDiminuiCooldownArmaPrincipal()
    {
        jogador.GetComponent<DisparoArma>().cooldown *= 0.70f;
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void PowerUPAumentaHP()
    {
        jogador.GetComponent<ControlaPersonagem>().pontosVida += 1;
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
