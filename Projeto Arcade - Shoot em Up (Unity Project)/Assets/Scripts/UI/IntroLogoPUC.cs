using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLogoPUC : MonoBehaviour
{

    public float timeLoadScene = 4.0f;
    private float timer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CarregaCena("Menu Inicial");
        }
        timer += Time.deltaTime;
        if (timer < timeLoadScene) return;

        CarregaCena("Menu Inicial");
    }

    public void CarregaCena(string nomeCena)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nomeCena);
    }
}
