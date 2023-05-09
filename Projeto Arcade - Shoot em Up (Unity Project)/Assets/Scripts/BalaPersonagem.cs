using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPersonagem : MonoBehaviour
{
    public float velocidade = 15.0f;
    public GameObject projetil;
    public bool projetilInimigo = false, rotacaoTiro = false;
    public GameObject projetilRotacao;
    public float velocidadeRotacao = 200.0f;
    public int danoProjetil = 1;
    // destroi quando sai da tela
    private Vector3 maxDistance = new Vector3(40.0f, 40.0f, 0.0f);
    private Vector3 minDistance = new Vector3(-40.0f, -5.0f, 0.0f);


    void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentoProjetil();

        DestroyOutOfScreen(projetil.transform.position);
    }

    private void DestroyOutOfScreen(Vector3 pos)
    {
        if (pos.x > maxDistance.x || pos.x < minDistance.x || pos.y > maxDistance.y || pos.y < minDistance.y)
        {
            Destroy(gameObject);
        }
    }

    // Movimento bala
    void MovimentoProjetil()
    {
        projetil.transform.Translate(0, velocidade * Time.deltaTime, 0);

        if (rotacaoTiro)
        {
            projetilRotacao.transform.Rotate(Time.deltaTime * velocidadeRotacao * transform.forward);
        }
    }

    private void OnCollisionEnter(Collision colidido)
    {
        if(colidido.gameObject.CompareTag("OrbeGiratorio") && projetilInimigo)
        {
            Destroy(gameObject);
        }
    }
}
