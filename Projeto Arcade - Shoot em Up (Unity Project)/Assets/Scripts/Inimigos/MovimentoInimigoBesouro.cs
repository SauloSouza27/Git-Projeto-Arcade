using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoBesouro : MonoBehaviour
{
    public GameObject besouro;
    // movimento
    public float velocidadeMovimento = 2.0f, velocidadeRotacao = 2.0f, tempoMudancaDirecao = 2.0f, maxRotZ = 30.0f, minRotZ = 330.0f;
    private bool mudaDirecao = false;

    private void Start()
    {

    }
    void Update()
    {
        Debug.Log(mudaDirecao);
        Movimento();
    }
     private void Movimento()
    {
        // direcao
        besouro.transform.Translate(0, velocidadeMovimento * Time.deltaTime, 0);
        // rotacao
        besouro.transform.Rotate(0, 0, velocidadeRotacao * Time.deltaTime);
    }
}
