using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilBalanca : MonoBehaviour
{
    public float velocidadeRotacao = 250.0f;
    private bool go;
    private Vector3 posicaoAnterior;
    public GameObject projetilBalanca;
    private GameObject balancaBase;
    private Vector3 locationBalanca;
    // Start is called before the first frame update
    void Start()
    {
        go = true;

        posicaoAnterior = transform.position;

        balancaBase = GameObject.Find("Base Balanca");

        locationBalanca = new Vector3(balancaBase.transform.position.x, balancaBase.transform.position.y + 1, balancaBase.transform.position.z) - balancaBase.transform.up * 30f;

        StartCoroutine(Boom());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        ControleArmaBalanca();
    }
    private void ControleArmaBalanca()
    {
        transform.Rotate(0, 0, Time.deltaTime * velocidadeRotacao);

        if (go)
        {
            balancaBase.GetComponent<MeshRenderer>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, locationBalanca, Time.deltaTime * 40);
        }
        if (!go)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(balancaBase.transform.position.x, balancaBase.transform.position.y + 1, balancaBase.transform.position.z), Time.deltaTime * 40);
        }
        if (!go && Vector3.Distance(balancaBase.transform.position, transform.position) < 1.45)
        {
            balancaBase.GetComponent<MeshRenderer>().enabled = true;
            Destroy(projetilBalanca);
        }
    }
    IEnumerator Boom()
    {
        go = true;
        yield return new WaitForSeconds(1.0f);
        go = false;
    }
}
