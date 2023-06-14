using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoProjetilBalanca : MonoBehaviour
{
    private GameObject alvo;
    public GameObject balanca, projetilBalanca, pontaSaidaBalanca;
    [Range(0, 6)] public float cooldown = 3.0f, velocidadeRotacao = 2.0f;
    private float contadorCooldown;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MovimentoBalanca();
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0)
        {
            DisparaBalanca();
            contadorCooldown = cooldown;
        }
    }
    private void DisparaBalanca()
    {
        GameObject instancia = Instantiate(projetilBalanca, pontaSaidaBalanca.transform.position, pontaSaidaBalanca.transform.rotation);
        instancia.GetComponent<ProjetilBalanca>().balanca = this.gameObject;
        instancia.GetComponent<ProjetilBalanca>().balancaBase = this.transform.GetChild(1).gameObject;
    }

    private void MovimentoBalanca()
    {
        Vector3 direcao = alvo.transform.position - balanca.transform.position;
        direcao = direcao.normalized;
        direcao.z = 0;
        balanca.transform.up = Vector3.Slerp(balanca.transform.up, -1 * direcao, velocidadeRotacao * Time.deltaTime);
    }
}
