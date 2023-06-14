using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroLogoPUC : MonoBehaviour
{

    public float timeLoadScene = 5.0f;
    private float timer;

    public float tempoFade = 1.0f;
    public Image logo;
    private bool fadeIn = true, fechouCirculo = false;

    private void Update()
    {
        Debug.Log(logo.color);
        FadeInOut();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CarregaCena("Menu Inicial");
        }
        timer += Time.deltaTime;
        if (timer < timeLoadScene) return;

        CarregaCena("Menu Inicial");
    }

    private void CarregaCena(string nomeCena)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nomeCena);
    }

    private void FadeInOut()
    {
        if (!fechouCirculo)
        {
            if (fadeIn)
            {
                for (float i = 0; i <= 1; i += Time.deltaTime * tempoFade/1000)
                {
                    logo.color = new Color(1, 1, 1, i);
                    if (i == 1)
                    {
                        fadeIn = false;
                    }
                }
            }
            if (!fadeIn)
            {
                for (float i = 1; i >= 0; i -= Time.deltaTime * tempoFade/1000)
                {
                    logo.color = new Color(1, 1, 1, i);
                    if (i == 0)
                    {
                        fadeIn = true;
                        fechouCirculo = true;
                    }
                }
            }
        }
    }
}
