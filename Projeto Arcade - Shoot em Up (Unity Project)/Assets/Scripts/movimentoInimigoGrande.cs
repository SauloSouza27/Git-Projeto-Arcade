using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoInimigoGrande : MonoBehaviour
{
    public GameObject pontaArma, alvo;
    // Variáveis do movimento                                //Colocar valor negativo para alterar a direção inicial
    public float contadorMovimento, limiteMovimento = 2.0f, velocidade = 10, velocidadeRotacao = 2.0f;
    // Pontos de vida
    public float pontosVida = 40.0f;
    public float danoContato = 40.0f;

    void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
        contadorMovimento = limiteMovimento;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.deltaTime;
        
        // Movimento vai e vem(padrão direita pra esquerda)
        if (contadorMovimento <= limiteMovimento && contadorMovimento >= 0)
        {
            transform.Translate(t * velocidade * Vector3.right, Space.World);
            contadorMovimento -= t;
        }
        if (contadorMovimento >= -limiteMovimento && contadorMovimento <= 0)
        {
            transform.Translate(t * velocidade * - Vector3.right, Space.World);
            contadorMovimento -= t;
        }
        if (contadorMovimento < -limiteMovimento)
        {
            contadorMovimento = limiteMovimento;
        }
        // Olhar personagem
        Vector3 direcao = alvo.transform.position - transform.position;
        direcao.y = 0;
        transform.forward = Vector3.Slerp(transform.forward, -1 * direcao.normalized, velocidadeRotacao * Time.deltaTime);
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
        if (colisor.gameObject.CompareTag("Player"))
        {
            float dano = colisor.gameObject.GetComponent<controlaPersonagem>().danoContato;
            if (pontosVida > 0)
            {
                pontosVida -= dano;
                Material material = gameObject.GetComponent<Renderer>().material;
                material.color += 0.5f * Color.red;
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                Debug.Log("20 XP");
            }
        }
    }
}
