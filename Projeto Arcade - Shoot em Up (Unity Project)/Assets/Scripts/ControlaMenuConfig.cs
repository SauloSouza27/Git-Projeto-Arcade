using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaMenuConfig : MonoBehaviour
{
    private static ControlaMenuConfig instancia;
    public GameObject[] botoesFase;
    public AudioSource musica;
    public List<AudioSource> SFXs;
    // som musica
    public Slider sliderMusica;
    private float volumeOriginalMusica;
    // som SFX
    public Slider sliderSFX;
    private float[] volumesOriginaisSFX;

    private void Start()
    {
        if (instancia == null)
        {
            instancia = this;
        }else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        volumeOriginalMusica = musica.volume;
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
        for (int i = 0; i < SFXs.Count; i++)
        {
            SFXs[i].volume = volumesOriginaisSFX[i] * sliderSFX.value;
        }
    }
}
