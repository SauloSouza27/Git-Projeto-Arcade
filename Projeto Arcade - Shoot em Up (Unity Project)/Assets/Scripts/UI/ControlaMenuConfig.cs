using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ControlaMenuConfig : MonoBehaviour
{
    public GameObject[] botoesFase;
    public AudioMixer mixer;
    // som musica
    public Slider sliderMusica;
    // som SFX
    public Slider sliderSFX;

    private void Start()
    {

    }
    private void OnEnable()
    {
        // ajusta valor slider ao abrir
        mixer.GetFloat("MusicVol", out float valueMusic);
        sliderMusica.value = valueMusic;
        mixer.GetFloat("SFXVol", out float valueSFX);
        sliderSFX.value = valueSFX;

        if (SceneManager.GetActiveScene().name == "Menu Inicial")
        {
            foreach(GameObject go in botoesFase)
            {
                go.SetActive(false);
            }
        }
        if (SceneManager.GetActiveScene().name == "Fase 1" || SceneManager.GetActiveScene().name == "Fase 2")
        {
            foreach (GameObject go in botoesFase)
            {
                go.SetActive(true);
            }
        }

    }
    private void Update()
    {
        
    }
    public void CarregaCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
        Time.timeScale = 1;
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
    public void ControlaMusica()
    {
        mixer.SetFloat("MusicVol", sliderMusica.value);
    }
    public void ControlaSFX()
    {
        mixer.SetFloat("SFXVol", sliderSFX.value);
    }
}
