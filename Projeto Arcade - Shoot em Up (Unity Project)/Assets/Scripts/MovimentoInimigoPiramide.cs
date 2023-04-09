using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoPiramide : MonoBehaviour
{
    private GameObject alvo;
    public GameObject cabecaPiramide, pontaArma;
    // Controle rotaçao
    public float velocidadeRotacao = 2.0f;
    // Pontos de vida
    public float pontosVida = 40.0f;
    public float danoContato = 40.0f;
    public int xpInimigo = 100;

    private void Awake()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    void Update()
    {
        MovimentaInimigoPiramide();
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
                Material material = gameObject.GetComponent<Renderer>().material;
                StartCoroutine(Utilidades.PiscaCorRoutine(material));
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
                Material material = gameObject.GetComponent<Renderer>().material;
                StartCoroutine(Utilidades.PiscaCorRoutine(material));
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
        if (colisor.gameObject.CompareTag("Player"))
        {
            float dano = colisor.gameObject.GetComponent<ControlaPersonagem>().danoContato;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                Material material = gameObject.GetComponent<Renderer>().material;
                StartCoroutine(Utilidades.PiscaCorRoutine(material));
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
    }

    private void MovimentaInimigoPiramide()
    {
        // Rotacao corpo
        Vector3 direcao = alvo.transform.position - transform.position;
        direcao = direcao.normalized;
        transform.up = Vector3.Slerp(transform.up, -1 * direcao, velocidadeRotacao * Time.deltaTime);
        // Mira cabeca
        cabecaPiramide.transform.rotation = Quaternion.LookRotation(cabecaPiramide.transform.forward, direcao);
    }
}
