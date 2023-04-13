using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorGame : MonoBehaviour
{
    public static ControladorGame instancia;

    // XP e nivel
    public int XP, nivel = 1;
    public float HP;
    public float multiplicadorQuantidadeXPporNivel = 50.0f, valorXPNivel = 100.0f;
    public GameObject barraHP, barraXP, jogador;
    public TextMeshProUGUI txtNivel, txtXP;
    private Slider sliderXP, sliderHP;
    // spawn inimigos
    private GameObject[] spawnInimigoPequeno, spawnInimigoPiramide, spawnInimigoDroneMorcego;
    private bool paraSpawnInimigoPiramide = false, paraSpawnInimigoDroneMorcego = false;
    // Power UP
    public GameObject uiGameOver, uiPowerUP, buttonArmaPet, buttonArmaOrbeGiratorio;
    public bool armaPetAtivada = false, armaOrbeGiratorioAtivada = false;

    private void Awake()
    {
        sliderHP = barraHP.GetComponent<Slider>();
        sliderXP = barraXP.GetComponent<Slider>();
        txtNivel = GameObject.Find("txtN�vel").GetComponent<TextMeshProUGUI>();
        txtXP = GameObject.Find("txtXP").GetComponent<TextMeshProUGUI>();
        //busca pontos de spawn na cena
        spawnInimigoPequeno = GameObject.FindGameObjectsWithTag("SpawnInimigoPequeno");
        spawnInimigoPiramide = GameObject.FindGameObjectsWithTag("SpawnInimigoPiramide");
        spawnInimigoDroneMorcego = GameObject.FindGameObjectsWithTag("SpawnInimigoMorcegoDrone");
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
        if (nivel == 3)
        {
            foreach (GameObject go in spawnInimigoDroneMorcego)
            {
                SpawnInimigoMorcegoDrone spawn = go.GetComponent<SpawnInimigoMorcegoDrone>();
                if (paraSpawnInimigoDroneMorcego == false)
                {
                    spawn.AtivaSpawnInimigoMorcegoDrone();
                }
            }
            paraSpawnInimigoDroneMorcego = true;
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

    public void AtualizaBarraHP(float hpAtual)
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
        
        float valorMaxSlider = sliderXP.maxValue;
        float valorSlider = sliderXP.value;
        if (valorSlider == valorMaxSlider)
        {
            SubirNivel();
            if (XP - valorXPNivel == 0)
            {
                sliderXP.value = 0;
                sliderXP.maxValue += (nivel * multiplicadorQuantidadeXPporNivel);
                valorXPNivel += sliderXP.maxValue;
            }
            if (XP - valorXPNivel > 0)
            {
                float diferenca = XP - valorXPNivel;
                sliderXP.value = 0;
                sliderXP.value += diferenca;
                sliderXP.maxValue += (nivel * multiplicadorQuantidadeXPporNivel);
                valorXPNivel += sliderXP.maxValue;
            }
        }
        txtNivel.text = "N�vel: " + nivel;
        txtXP.text = XP + "/" + valorXPNivel;
    }

    private void SubirNivel()
    {
        nivel += 1;
        uiPowerUP.SetActive(true);
        Time.timeScale = 0.0f;
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
    public void PowerUPAumentaDanoArmaPrincipal()
    {
        jogador.GetComponent<DisparoArma>().danoArmaPrincipal *= 1.15f;
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void PowerUPDiminuiCooldownArmaPrincipal()
    {
        jogador.GetComponent<DisparoArma>().cooldown *= 0.90f;
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void PowerUPAumentaHP()
    {
        sliderHP.maxValue += 20.0f;
        jogador.GetComponent<ControlaPersonagem>().pontosVida += 20.0f;
        uiPowerUP.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
