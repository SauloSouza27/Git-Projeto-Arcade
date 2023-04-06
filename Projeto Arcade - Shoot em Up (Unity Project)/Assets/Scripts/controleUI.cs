using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleUI : MonoBehaviour
{
    public GameObject barraXP, jogador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Slider sliderXP = barraXP.GetComponent<Slider>();
        int valorMaxSlider = (int)sliderXP.maxValue;
        int valorSlider = (int)sliderXP.value;
        if (valorSlider == valorMaxSlider)
        {
            barraXP.GetComponent<Slider>().value = 0;
            barraXP.GetComponent<Slider>().maxValue += 50;
        }
    }

    public void SomaXPBarra(int xpInimigo)
    {
        barraXP.GetComponent<Slider>().value += xpInimigo;
    }
}
