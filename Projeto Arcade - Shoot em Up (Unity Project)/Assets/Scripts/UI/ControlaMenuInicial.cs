using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenuInicial : MonoBehaviour
{
    public GameObject planetaFase2;

    private void Update()
    {
        if (ProgressoPlayer.instancia.concluiuFase1)
        {
            planetaFase2.SetActive(true);
        }
    }

    public void CarregaCena(string nomeCena)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nomeCena);
    }

    public void FecharJogo()
    {
        Application.Quit();
    }
}
