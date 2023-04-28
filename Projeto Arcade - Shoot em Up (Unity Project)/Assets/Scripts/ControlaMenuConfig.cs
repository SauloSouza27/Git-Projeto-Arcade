using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaMenuConfig : MonoBehaviour
{
    private static ControlaMenuConfig instancia;
    public GameObject jogador;
    public GameObject[] botoesFase;
    public AudioSource musica;
    public AudioSource[] SFXs;
    // som musica
    public Slider sliderMusica;
    private float volumeOriginalMusica;
    // som SFX
    public Slider sliderSFX;
    private float[] volumesOriginaisSFX;

    private void Start()
    {
        volumeOriginalMusica = musica.volume;

        // busca SFX
        SFXs = jogador.GetComponents<AudioSource>();
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
        musica.volume = volumeOriginalMusica * sliderMusica.value;
    }
    private void ControlaSFX()
    {
        for (int i = 0; i < SFXs.Length; i++)
        {
            SFXs[i].volume = sliderSFX.value;
        }
    }
}
