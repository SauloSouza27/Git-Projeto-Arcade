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
    private GameObject[] spawnInimigoPequeno, spawnInimigoPiramide, spawnInimigoDroneMorcego;
    private bool paraSpawnInimigoPiramide = false, paraSpawnInimigoDroneMorcego = false;
    // Power UP
    public GameObject uiGameOver, uiPowerUP, buttonSubirNivel, buttonArmaPet, buttonArmaOrbeGiratorio, buttonArmaSerra;
    private Vector3 tamanhoOriginalButtonSubirNivel;
    public bool armaPetAtivada = false, armaOrbeGiratorioAtivada = false, armaSerraAtivada = false;
    // dano nos inimigos
    private float vidaInimigo;

    private void Awake()
    {
        sliderHP = barraHP.GetComponent<Slider>();
        sliderXP = barraXP.GetComponent<Slider>();
        txtNivel = GameObject.Find("txtNivel").GetComponent<TextMeshProUGUI>();
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
            Transform btTransform = buttonSubirNivel.transform;
            tamanhoOriginalButtonSubirNivel = buttonSubirNivel.transform.localScale;
            if(btTransform.localScale == tamanhoOriginalButtonSubirNivel)
            {
                btTransform.localScale = Vector3.Slerp(btTransform.localScale, btTransform.localScale * 1.5f, 10.0f);
            }
            if(btTransform.localScale == tamanhoOriginalButtonSubirNivel * 1.5f)
            {
                btTransform.localScale = Vector3.Slerp(btTransform.localScale, tamanhoOriginalButtonSubirNivel, 10.0f);
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

    // Dano nos inimigos
    public void CalculaDanoNosInimigos(Collision colidido, int pontosVida, int xpInimigo, Material[] materiais)
    {
        if (colidido.gameObject.CompareTag("BalaPersonagem"))
        {
            Destroy(colidido.gameObject);
            int dano = jogador.GetComponent<DisparoArma>().danoArmaPrincipal;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                vidaInimigo = pontosVida;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(colidido.gameObject);
                SomaXP(xpInimigo);
            }
        }
        if (colidido.gameObject.CompareTag("BalaPet"))
        {
            Destroy(colidido.gameObject);
            int dano = jogador.GetComponent<DisparoArmaPet>().danoArmaPet;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                vidaInimigo = pontosVida;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(colidido.gameObject);
                SomaXP(xpInimigo);
            }
        }
        if (colidido.gameObject.CompareTag("OrbeGiratorio"))
        {
            int dano = jogador.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                vidaInimigo = pontosVida;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(colidido.gameObject);
                SomaXP(xpInimigo);
            }
        }
        if (colidido.gameObject.CompareTag("ProjetilSerra"))
        {
            int dano = jogador.GetComponent<DisparoArmaSerra>().danoSerra;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                vidaInimigo = pontosVida;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(colidido.gameObject);
                SomaXP(xpInimigo);
            }
        }
        if (colidido.gameObject.CompareTag("Player"))
        {
            int dano = jogador.GetComponent<ControlaPersonagem>().danoContato;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                vidaInimigo = pontosVida;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(colidido.gameObject);
                SomaXP(xpInimigo);
            }
        }
    }
}
