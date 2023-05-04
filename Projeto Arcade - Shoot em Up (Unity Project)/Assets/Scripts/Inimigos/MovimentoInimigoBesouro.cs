using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoBesouro : MonoBehaviour
{
    public GameObject besouro;
    // movimento
    public float velocidadeMovimento = 2.0f, velocidadeRotacao = 2.5f;
    private bool mudaDirecao = true;

    private void Start()
    {

    }
    void Update()
    {
        Movimento();
    }
     private void Movimento()
    {
        // direcao
        besouro.transform.Translate(0, velocidadeMovimento * Time.deltaTime, 0);
        // rotacao
        Debug.Log(transform.eulerAngles.z);
        Vector3 rotacaoDir = new Vector3(0, 0, 60);
        Vector3 rotacaoEsq = new Vector3(0, 0, 0);
        if (mudaDirecao)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, rotacaoDir, Time.deltaTime * velocidadeRotacao);
        }
        if (transform.eulerAngles.z >= 59)
        {
            mudaDirecao = false;
        }
        if (!mudaDirecao)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, rotacaoEsq, Time.deltaTime * velocidadeRotacao);
        }
        if (transform.eulerAngles.z <= 1)
        {
            mudaDirecao = true;
        }
    }
}
