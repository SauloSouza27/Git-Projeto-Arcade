using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteMovimentoCabecaBossFase1 : MonoBehaviour
{
    public GameObject jogador, cabecaPiramide;
    public float velocidade = 2.0f, tempoParado = 2.0f;
    private Vector3 posAlvo;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        posAlvo = jogador.transform.position;
    }

    private void Update()
    {
        MovimentaCabecaBoss();
    }

    private void MovimentaCabecaBoss()
    {
        Vector3 posCabeca = cabecaPiramide.transform.position;
        if (Vector3.Distance(posAlvo, posCabeca) > 0.8)
        {
            cabecaPiramide.transform.position = Vector3.Lerp(posCabeca, posAlvo, velocidade * Time.deltaTime);
        }
        if (Vector3.Distance(posAlvo, posCabeca) < 0.8)
        {
            Invoke(nameof(BuscaNovaPosicaoPlayer), tempoParado);
        }
    }

    private void BuscaNovaPosicaoPlayer()
    {
        posAlvo = jogador.transform.position;
        CancelInvoke(nameof(BuscaNovaPosicaoPlayer));
    }
}
