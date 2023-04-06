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
    public GameObject barraXP, jogador;
    public TextMeshProUGUI txtNivel, txtXP;
    // Power UP
    public GameObject uiPowerUP;

    private void Awake()
    {
        txtNivel = GameObject.Find("txtNível").GetComponent<TextMeshProUGUI>();
        txtXP = GameObject.Find("txtXP").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    
    void Update()
    {
        CalculaBarraXP();
    }

    public void SomaXPBarra(int xpInimigo)
    {
        barraXP.GetComponent<Slider>().value += xpInimigo;
    }
    public void SomaXP(int xpInimigo)
    {
        XP += xpInimigo;
    }

    private void CalculaBarraXP()
    {
        txtNivel.text = "Nível: " + nivel;
        Slider sliderXP = barraXP.GetComponent<Slider>();
        int valorMaxSlider = (int)sliderXP.maxValue;
        int valorSlider = (int)sliderXP.value;
        txtNivel.text = "Nível: " + nivel;
        txtXP.text = XP + "/" + valorMaxSlider;
        if (valorSlider == valorMaxSlider)
        {
            SubirNivel();
            barraXP.GetComponent<Slider>().value = 0;
            barraXP.GetComponent<Slider>().maxValue += 50;
        }
    }

    private void SubirNivel()
    {
        nivel += 1;
        uiPowerUP.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
