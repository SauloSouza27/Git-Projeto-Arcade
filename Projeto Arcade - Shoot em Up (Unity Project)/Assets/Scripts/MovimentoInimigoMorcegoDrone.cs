using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoMorcegoDrone : MonoBehaviour
{
    GameObject alvo;
    // Pontos de vida
    public int pontosVida = 1;
    // XP quando morre
    public int xpInimigo = 10;
    // Materiais
    MeshRenderer[] renderers;
    Material[] materiais;
    private void Awake()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
        // Busca materiais do inimigo
        renderers = GetComponentsInChildren<MeshRenderer>();
        materiais = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            materiais[i] = renderers[i].material;
        }
    }
    private void Update()
    {

    }
    private void OnCollisionEnter(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("BalaPersonagem"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<DisparoArma>().danoArmaPrincipal;
            if (pontosVida > 0)
            {
                pontosVida -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
        if (colisor.gameObject.CompareTag("BalaPet"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;
            if (pontosVida > 0)
            {
                pontosVida -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
        if (colisor.gameObject.CompareTag("OrbeGiratorio"))
        {
            int dano = alvo.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;
            if (pontosVida > 0)
            {
                pontosVida -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
        if (colisor.gameObject.CompareTag("ProjetilSerra"))
        {
            int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;
            if (pontosVida > 0)
            {
                pontosVida -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
        if (colisor.gameObject.CompareTag("Player"))
        {
            int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;
            if (pontosVida > 0)
            {
                pontosVida -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
    }
}

    
