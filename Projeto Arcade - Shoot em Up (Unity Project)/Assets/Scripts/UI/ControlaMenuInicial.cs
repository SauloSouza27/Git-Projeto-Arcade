using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenuInicial : MonoBehaviour
{
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
