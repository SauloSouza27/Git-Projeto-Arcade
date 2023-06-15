using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroLogoPUC : MonoBehaviour
{

    public float timeLoadScene = 5.0f;
    private float timer;

    public float tempoFade = 0.5f;
    public Image logo;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

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

    private void CarregaCena(string nomeCena)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nomeCena);
    }

    private IEnumerator FadeIn()
    {
        float alpha = logo.color.a;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * tempoFade;

            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, alpha);
            
            yield return null;
        }

        yield return null;
    }
}
