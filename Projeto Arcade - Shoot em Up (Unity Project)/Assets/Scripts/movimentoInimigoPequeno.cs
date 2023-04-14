using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MovimentoInimigoPequeno : MonoBehaviour
{
    private GameObject alvo;
    public float velocidadeDeslocamento = 5.0f, velocidadeRotacao = 5.0f;
    // Pontos de vida
    public float pontosVida = 20.0f;
    public float danoContato = 20.0f;
    // XP quando morre
    public int xpInimigo = 20;

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

    void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentaInimigoPequeno();
    }
    private void OnCollisionEnter(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("BalaPersonagem"))
        {
            Destroy(colisor.gameObject);
            float dano = alvo.GetComponent<DisparoArma>().danoArmaPrincipal;
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
            float dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;
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
            float dano = alvo.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;
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
            float dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;
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
            float dano = alvo.GetComponent<ControlaPersonagem>().danoContato;
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
    private void OnCollisionStay(Collision colisor)
    {

        if (colisor.gameObject.CompareTag("ProjetilSerra"))
        {
            float contadorCooldown = 0;
            float cooldownDano = 0.5f;
            float dano = alvo.GetComponent<DisparoArmaSerra>().danoSerraDPS;
            Utilidades.CalculaCooldown(contadorCooldown);
            contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
            if(contadorCooldown == 0 && pontosVida > 0)
            {
                pontosVida -= dano;
                contadorCooldown = cooldownDano;

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
    private void MovimentaInimigoPequeno()
    {
        //Movimento de seguir jogador
        Vector3 dir = alvo.transform.position - transform.position;
        transform.position += Time.deltaTime * velocidadeDeslocamento * dir.normalized;
        //rotaçao
        transform.up = Vector3.Slerp(transform.up, -1 * dir, velocidadeRotacao * Time.deltaTime);
    }
}
