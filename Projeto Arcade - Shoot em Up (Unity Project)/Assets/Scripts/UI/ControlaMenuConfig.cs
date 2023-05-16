using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ControlaMenuConfig : MonoBehaviour
{
    private static ControlaMenuConfig instancia;
    public GameObject jogador;
    public GameObject[] botoesFase;
    public AudioMixer mixer;
    // som musica
    public Slider sliderMusica;
    private float volumeOriginalMusica;
    // som SFX
    public Slider sliderSFX;
    private float[] volumesOriginaisSFX;

    private void Start()
    {

    }
    private void OnEnable()
    {
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
