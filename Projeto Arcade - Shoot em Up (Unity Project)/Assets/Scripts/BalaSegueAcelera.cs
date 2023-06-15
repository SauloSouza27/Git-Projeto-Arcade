using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSegueAcelera : MonoBehaviour
{
    public float velocidade = 15.0f, velocidadeRotacao =5.0f, distanciaSeguir = 10.0f, fatorAumentoVelocidade = 1.0f;
    public GameObject projetil;

    private GameObject alvo;
    private bool paraSeguir = false;

    private void Awake()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentoProjetil();

        Utilidades.DestroyOutOfScreen(projetil.transform.position, gameObject);
    }

    // Movimento bala
    void MovimentoProjetil()
    {
        Vector3 dir = alvo.transform.position - projetil.transform.position;
        float distancia = dir.magnitude;

        if(distancia >= distanciaSeguir)
        {
            if (!paraSeguir)
            {
                //Movimento de seguir jogador
                projetil.transform.position += Time.deltaTime * velocidade * dir.normalized;
                velocidade += fatorAumentoVelocidade * Time.deltaTime;
                //rotaçao olhar jogador
                projetil.transform.up = Vector3.Slerp(projetil.transform.up, dir, velocidadeRotacao * Time.deltaTime);
            }
            if (paraSeguir)
            {
                //Ir reto até sumir
                projetil.transform.Translate(0, velocidade * Time.deltaTime, 0);
                velocidade += fatorAumentoVelocidade * Time.deltaTime;
            }
        }

        if (distancia < distanciaSeguir)
        {
            paraSeguir = true;
            //Ir reto até sumir
            projetil.transform.Translate(0, velocidade * Time.deltaTime, 0);
            velocidade += fatorAumentoVelocidade * Time.deltaTime;
        }
    }
}
