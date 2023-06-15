using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilBalanca : MonoBehaviour
{
    public float velocidadeProjetil = 40.0f, velocidadeRotacao = 250.0f, tempoParado = 0.3f;
    private float timerParado;
    private bool go;
    public GameObject projetilBalanca;
    public GameObject balancaBase, balanca;
    private Vector3 locationAlvo;
    private GameObject alvo;
    public bool invertRotacao = false;

    private void Awake()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        go = true;

        locationAlvo = alvo.transform.position;
    }


    void Update()
    {
        if (Time.timeScale == 0) return;

        if (balanca == null)
        {
            Destroy(gameObject);
        }

        ControleArmaBalanca();
    }
    private void ControleArmaBalanca()
    {
        if (!invertRotacao)
        {
            transform.Rotate(0, 0, Time.deltaTime * velocidadeRotacao);
        }
        if (invertRotacao)
        {
            transform.Rotate(0, 0, - Time.deltaTime * velocidadeRotacao);
        }

        Vector3 dir = locationAlvo - projetilBalanca.transform.position;
        float distancia = Vector3.Distance(projetilBalanca.transform.position, locationAlvo);

        if (distancia > 3 && go)
        {
            balancaBase.GetComponent<MeshRenderer>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, locationAlvo, Time.deltaTime * velocidadeProjetil);
        }
        if (distancia <= 3)
        {
            go = false;

            timerParado += Time.deltaTime;
            if (timerParado <= tempoParado) return;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(balancaBase.transform.position.x, balancaBase.transform.position.y + 1, balancaBase.transform.position.z), Time.deltaTime * velocidadeProjetil);
        }
        if (distancia >= 3 && !go)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(balancaBase.transform.position.x, balancaBase.transform.position.y + 1, balancaBase.transform.position.z), Time.deltaTime * velocidadeProjetil);
        }
        if (!go && Vector3.Distance(balancaBase.transform.position, transform.position) < 1.45)
        {
            balancaBase.GetComponent<MeshRenderer>().enabled = true;
            Destroy(projetilBalanca);
        }
    }
}
