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
    // Power UP
    public GameObject uiGameOver, uiPowerUP, buttonArmaPet;
    public bool armaPetAtivada = false;

    private void Awake()
    {
        sliderHP = barraHP.GetComponent<Slider>();
        sliderXP = barraXP.GetComponent<Slider>();
        txtNivel = GameObject.Find("txtNível").GetComponent<TextMeshProUGUI>();
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
        txtNivel.text = "Nível: " + nivel;
        txtXP.text = XP + "/" + valorXPNivel;
    }

    private void SubirNivel()
    {
        nivel += 1;
        uiPowerUP.SetActive(true);
        Time.timeScale = 0.0f;
    }
    
    public void PowerUPAtivaArmaNova(GameObject armaNova)
    {
        armaNova.SetActive(true);
        armaPetAtivada = true;
        jogador.GetComponent<DisparoArmaPet>().enabled = true;
        Destroy(buttonArmaPet);
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
