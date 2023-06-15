using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoProjetilBalanca : MonoBehaviour
{
    private GameObject alvo;
    public GameObject balanca, projetilBalanca, pontaSaidaBalanca;
    [Range(0, 6)] public float cooldown = 3.0f, velocidadeRotacao = 2.0f;
    private float contadorCooldown;
    public bool inverteRotacao = false;
    // movimento giro
    public float velocidadeGiro = 5.0f;
    public bool isPos1 = false, isPos2 = false, isPos3 = false, isPos4 = false;
    private Vector3 pos1, pos2, pos3, pos4;
    private bool seMovimenta = false;

    private void Awake()
    {
        pos1 = new Vector3(26f, 29f, 0f);
        pos2 = new Vector3(-26f, 29f, 0f);
        pos3 = new Vector3(-26f, 3.0f, 0f);
        pos4 = new Vector3(26f, 3.0f, 0f);
    }
    void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
        contadorCooldown = cooldown;
    }

    void Update()
    {
        MovimentoBalanca();

        if (seMovimenta)
        {
            MovimentoGiro();
        }

        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0 && !seMovimenta)
        {
            DisparaBalanca();
            contadorCooldown = cooldown;
            seMovimenta = true;
        }
    }
    private void DisparaBalanca()
    {
        GameObject instancia = Instantiate(projetilBalanca, pontaSaidaBalanca.transform.position, pontaSaidaBalanca.transform.rotation);
        instancia.GetComponent<ProjetilBalanca>().balanca = this.gameObject;
        instancia.GetComponent<ProjetilBalanca>().balancaBase = this.transform.GetChild(1).gameObject;
        instancia.GetComponent<ProjetilBalanca>().invertRotacao = inverteRotacao;
    }

    private void MovimentoBalanca()
    {
        // rotacao corpo/mira
        Vector3 direcao = alvo.transform.position - balanca.transform.position;
        direcao = direcao.normalized;
        direcao.z = 0;
        balanca.transform.up = Vector3.Slerp(balanca.transform.up, -1f * direcao, velocidadeRotacao * Time.deltaTime);
    }

    private void MovimentoGiro()
    {
        if (isPos1)
        {
            float velocidade = velocidadeGiro * 2f;
            if (transform.position != pos2)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos2, Time.deltaTime * velocidade);
            }
            else
            {
                isPos1 = false;
                isPos2 = true;
                seMovimenta = false;
                contadorCooldown = cooldown;
            }
            return;
        }
        if (isPos2)
        {
            float velocidade = velocidadeGiro;
            if (transform.position != pos3)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos3, Time.deltaTime * velocidade);
            }
            else
            {
                isPos2 = false;
                isPos3 = true;
                seMovimenta = false;
                contadorCooldown = cooldown;
            }
            return;
        }
        if (isPos3)
        {
            float velocidade = velocidadeGiro * 2f;
            if (transform.position != pos4)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos4, Time.deltaTime * velocidade);
            }
            else
            {
                isPos3 = false;
                isPos4 = true;
                seMovimenta = false;
                contadorCooldown = cooldown;
            }
            return;
        }
        if (isPos4)
        {
            float velocidade = velocidadeGiro;
            if (transform.position != pos1)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos1, Time.deltaTime * velocidade);
            }
            else
            {
                isPos4 = false;
                isPos1 = true;
                seMovimenta = false;
                contadorCooldown = cooldown;
            }
            return;
        }
    }
}
