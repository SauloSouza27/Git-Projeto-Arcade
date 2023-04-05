using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class movimentoInimigoPequeno : MonoBehaviour
{
    public GameObject alvo;
    public float velocidadeDeslocamento = 5.0f, velocidadeRotacao = 5.0f;
    // Pontos de vida
    public float pontosVida = 20.0f;
    public float danoContato = 20.0f;

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
        if (colisor.gameObject.CompareTag("Bala Personagem"))
        {
            Destroy(colisor.gameObject);
            float dano = alvo.GetComponent<disparoArma>().danoArmaPrincipal;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                Material material = gameObject.GetComponent<Renderer>().material;
                material.color += 0.5f * Color.red;
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                Debug.Log("05 XP");
            }
        }
        if (colisor.gameObject.CompareTag("Bala Pet"))
        {
            Destroy(colisor.gameObject);
            float dano = alvo.GetComponent<disparoArmaPet>().danoArmaPet;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                Material material = gameObject.GetComponent<Renderer>().material;
                material.color += 0.5f * Color.red;
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                Debug.Log("05 XP");
            }
        }
        if (colisor.gameObject.CompareTag("Player"))
        {
            float dano = alvo.GetComponent<controlaPersonagem>().danoContato;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                Material material = gameObject.GetComponent<Renderer>().material;
                material.color += 0.5f * Color.red;
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                Debug.Log("05 XP");
            }
        }
    }
}
