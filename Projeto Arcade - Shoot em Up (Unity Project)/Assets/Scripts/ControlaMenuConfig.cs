using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaMenuConfig : MonoBehaviour
{
    public GameObject jogador;
    // som musica
    public AudioSource musicaMenuInicial, musicaFase1;
    public Slider sliderMusica;
    // som SFX
    public AudioSource[] listaSFX;
    public Slider sliderSFX;
    float[] volumesOriginais;

    private void Start()
    {
        // guarda som original de cada SFX
        if (jogador != null)
        {
            listaSFX = jogador.GetComponents<AudioSource>();
            volumesOriginais = new float[listaSFX.Length];
            for (int i = 0; i < listaSFX.Length; i++)
            {
                volumesOriginais[i] = listaSFX[i].volume;
            }
        }
    }
    private void Update()
    {
        ControlaMusica();
        ControlaSFX();
    }
    public void CarregaCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void FecharJogo()
    {
        Application.Quit();
    }

    public void PausaJogo(bool pausa)
    {
        if (pausa)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1;
    }
    // Controla soms
    private void ControlaMusica()
    {
        if (musicaFase1 != null)
        {
            musicaFase1.volume = sliderMusica.value;
        }
        if (musicaMenuInicial != null)
        {
            musicaMenuInicial.volume = sliderMusica.value;
        }
    }
    private void ControlaSFX()
    {
        for (int i = 0; i < listaSFX.Length; i++)
        {
            listaSFX[i].volume = volumesOriginais[i] * sliderSFX.value;
        }
    }
}
