using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MovimentoInimigoPequeno : MonoBehaviour
{
    public GameObject alvo;
    public float velocidadeDeslocamento = 5.0f, velocidadeRotacao = 5.0f;
    // Pontos de vida
    public float pontosVida = 20.0f;
    public float danoContato = 20.0f;
    // XP quando morre
    public int xpInimigo = 5;
    // hit projetil pisca cor
    private bool isPiscaCorAtivo = false;

    void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Movimento de seguir jogador
        Vector3 dir = alvo.transform.position - transform.position;
        transform.position += Time.deltaTime * velocidadeDeslocamento * dir.normalized;
        //rotaçao
        transform.up = Vector3.Slerp(transform.up, -1 * dir, velocidadeRotacao * Time.deltaTime);
        Debug.DrawRay(transform.position, dir, Color.green);
    }
    private void OnCollisionEnter(Collision colisor)
    {
        bool ativapiscaCor = Utilidades.AtivaPiscaCor(isPiscaCorAtivo);

        if (colisor.gameObject.CompareTag("Bala Personagem"))
        {
            Destroy(colisor.gameObject);
            float dano = alvo.GetComponent<DisparoArma>().danoArmaPrincipal;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                Material material = gameObject.GetComponent<Renderer>().material;
                isPiscaCorAtivo = ativapiscaCor;
                StartCoroutine(Utilidades.PiscaCorRoutine(material, isPiscaCorAtivo));
                isPiscaCorAtivo = false;
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
                ControladorGame.instancia.SomaXPBarra(xpInimigo);
            }
        }
        if (colisor.gameObject.CompareTag("Bala Pet"))
        {
            Destroy(colisor.gameObject);
            float dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                Material material = gameObject.GetComponent<Renderer>().material;
                isPiscaCorAtivo = ativapiscaCor;
                StartCoroutine(Utilidades.PiscaCorRoutine(material, isPiscaCorAtivo));
                isPiscaCorAtivo = false;
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
                ControladorGame.instancia.SomaXPBarra(xpInimigo);
            }
        }
        if (colisor.gameObject.CompareTag("Player"))
        {
            float dano = alvo.GetComponent<ControlaPersonagem>().danoContato;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                Material material = gameObject.GetComponent<Renderer>().material;
                isPiscaCorAtivo = ativapiscaCor;
                StartCoroutine(Utilidades.PiscaCorRoutine(material, isPiscaCorAtivo));
                isPiscaCorAtivo = false;
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
                ControladorGame.instancia.SomaXPBarra(xpInimigo);
            }
        }
    }
}
