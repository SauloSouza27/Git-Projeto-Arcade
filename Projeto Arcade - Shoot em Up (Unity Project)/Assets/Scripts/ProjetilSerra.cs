using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilSerra : MonoBehaviour
{

    // Arma Serra
    private bool estaMexendo = false;
    private bool go;
    private Vector3 posicaoAnterior;
    public GameObject projetilSerra;
    private GameObject serraBase, serraMaior, jogador;
    private Vector3 locationInFrontOfPlayer;

    void Start()
    {
        // Controladores arma serra
        go = true;

        posicaoAnterior = transform.position;

        jogador = GameObject.FindWithTag("Player");

        serraMaior = transform.GetChild(1).gameObject;

        serraMaior.GetComponent<MeshRenderer>().enabled = false;

        serraBase = GameObject.Find("Serra Base");

        locationInFrontOfPlayer = new Vector3(jogador.transform.position.x, jogador.transform.position.y + 1, jogador.transform.position.z) + jogador.transform.up * 15f;

        StartCoroutine(Boom());
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        ControleArmaSerra();
    }

    private void ControleArmaSerra()
    {
        transform.Rotate(0, 0, Time.deltaTime * 250);

        if (go)
        {
            serraBase.GetComponent<MeshRenderer>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, locationInFrontOfPlayer, Time.deltaTime * 40);
        }
        if (!go)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(jogador.transform.position.x, jogador.transform.position.y + 1, jogador.transform.position.z), Time.deltaTime * 40);
        }
        if (!go && Vector3.Distance(jogador.transform.position, transform.position) < 1.45)
        {
            serraBase.GetComponent<MeshRenderer>().enabled = true;
            Destroy(projetilSerra);
        }
        EstaAMexer();

        if (!estaMexendo)
        {
            serraMaior.GetComponent<MeshRenderer>().enabled = true;
        }
        if (estaMexendo)
        {
            serraMaior.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    // Paradinha da serra
    IEnumerator Boom()
    {
        go = true;
        yield return new WaitForSeconds(2.0f);
        go = false;
    }

    // Função para saber se a serra está mexendo
    private void EstaAMexer()
    {
        if (transform.position != posicaoAnterior)
        {
            estaMexendo = true;
        }
        else
        {
            if (estaMexendo)
            {
                // The game object has stopped moving, do something here
                estaMexendo = false;
            }
        }

        posicaoAnterior = transform.position;
    }
}
