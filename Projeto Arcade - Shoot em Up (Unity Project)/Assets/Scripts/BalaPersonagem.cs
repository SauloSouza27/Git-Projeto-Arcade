using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPersonagem : MonoBehaviour
{
    public float velocidade = 15.0f, duracaoBala = 200.0f;
    public GameObject projetil;
    public bool projetilInimigo = false, rotacaoTiro = false;
    public GameObject projetilRotacao, fonteTiro;
    public float velocidadeRotacao = 200.0f;
    public int danoProjetil = 1;

    
    void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentoProjetil();

        DestroiBala();
    }

    void DestroiBala()
    {
        Destroy(projetil, duracaoBala * Time.deltaTime);
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
