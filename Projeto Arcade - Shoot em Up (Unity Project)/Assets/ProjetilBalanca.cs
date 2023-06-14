using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilBalanca : MonoBehaviour
{
    public float velocidadeRotacao = 250.0f;
    private bool go;
    private Vector3 posicaoAnterior;
    public GameObject projetilBalanca;
    public GameObject balancaBase, balanca;
    private Vector3 locationBalanca;

    void Start()
    {
        go = true;

        posicaoAnterior = transform.position;

        locationBalanca = new Vector3(balanca.transform.position.x, balanca.transform.position.y + 1, balanca.transform.position.z) - balanca.transform.up * 30f;
    }


    void Update()
    {
        if (Time.timeScale == 0) return;

        ControleArmaBalanca();
    }
    private void ControleArmaBalanca()
    {
        transform.Rotate(0, 0, Time.deltaTime * velocidadeRotacao);

        Vector3 dir = locationBalanca - projetilBalanca.transform.position;
        float distancia = dir.magnitude;

        if (distancia > 4 && go)
        {
            balancaBase.GetComponent<MeshRenderer>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, locationBalanca, Time.deltaTime * 40);
        }
        if (distancia <= 4)
        {
            go = false;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(balancaBase.transform.position.x, balancaBase.transform.position.y + 1, balancaBase.transform.position.z), Time.deltaTime * 40);
        }
        if (!go && Vector3.Distance(balancaBase.transform.position, transform.position) < 1.45)
        {
            balancaBase.GetComponent<MeshRenderer>().enabled = true;
            Destroy(projetilBalanca);
        }
    }
}
