using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoMumia : MonoBehaviour
{
    private GameObject alvo;
    // mumias
    public GameObject mumiaEsq, mumiaCentro, mumiaDir;
    // move
    public float velocidadeDeslocamento = 5.0f, velocidadeRotacao = 5.0f;

    private void Awake()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable()
    {
        
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentaInimigoMumia();
    }

    private void MovimentaInimigoMumia()
    {
        //Movimento de seguir jogador
        Vector3 dir = alvo.transform.position - transform.position;
        transform.position += Time.deltaTime * velocidadeDeslocamento * dir.normalized;
        //rotaçao
        Vector3 dirEsq, dirDir, dirCentro;

        if (mumiaEsq != null)
        {
            dirEsq = alvo.transform.position - mumiaEsq.transform.position;
            mumiaEsq.transform.up = Vector3.Slerp(mumiaEsq.transform.up, -1 * dirEsq, velocidadeRotacao * Time.deltaTime);
        }
        if (mumiaDir != null)
        {
            dirDir = alvo.transform.position - mumiaEsq.transform.position;
            mumiaDir.transform.up = Vector3.Slerp(mumiaDir.transform.up, -1 * dirDir, velocidadeRotacao * Time.deltaTime);
        }        
        if (mumiaCentro != null)
        {
            dirCentro = alvo.transform.position - mumiaEsq.transform.position;
            mumiaCentro.transform.up = Vector3.Slerp(mumiaCentro.transform.up, -1 * dirCentro, velocidadeRotacao * Time.deltaTime);
        }   
    }
}
