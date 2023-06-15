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
    public float distanciaMinNovaPos = 0.5f;

    private void Awake()
    {
        pos1 = new Vector3(27f, 28f, 0f);
        pos2 = new Vector3(-27f, 28f, 0f);
        pos3 = new Vector3(-27f, 3.5f, 0f);
        pos4 = new Vector3(27f, 3.5f, 0f);
    }
    void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!seMovimenta)
        {
            MovimentoBalanca();
        }
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
        balanca.transform.up = Vector3.Slerp(balanca.transform.up, -1 * direcao, velocidadeRotacao * Time.deltaTime);
    }

    private void MovimentoGiro()
    {
        if (isPos1)
        {
            // rotacao olhar direcao nova posicao
            Vector3 dir = pos2 - transform.position;
            balanca.transform.up = Vector3.Slerp(balanca.transform.up, -1 * dir, velocidadeRotacao * Time.deltaTime);
            // mover para pos nova
            if (Vector3.Distance(transform.position, pos2) > distanciaMinNovaPos)
            {
                transform.position = Vector3.Lerp(transform.position, pos2, Time.deltaTime * velocidadeGiro);
            }
            if (Vector3.Distance(transform.position, pos2) <= distanciaMinNovaPos)
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
            // rotacao olhar direcao nova posicao
            Vector3 dir = pos3 - transform.position;
            balanca.transform.up = Vector3.Slerp(balanca.transform.up, -1 * dir, velocidadeRotacao * Time.deltaTime);
            // mover para pos nova
            if (Vector3.Distance(transform.position, pos3) > distanciaMinNovaPos)
            {
                transform.position = Vector3.Lerp(transform.position, pos3, Time.deltaTime * velocidadeGiro);
            }
            if (Vector3.Distance(transform.position, pos3) <= distanciaMinNovaPos)
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
            // rotacao olhar direcao nova posicao
            Vector3 dir = pos4 - transform.position;
            balanca.transform.up = Vector3.Slerp(balanca.transform.up, -1 * dir, velocidadeRotacao * Time.deltaTime);
            // mover para pos nova
            if (Vector3.Distance(transform.position, pos4) > distanciaMinNovaPos)
            {
                transform.position = Vector3.Lerp(transform.position, pos4, Time.deltaTime * velocidadeGiro);
            }
            if (Vector3.Distance(transform.position, pos4) <= distanciaMinNovaPos)
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
            // rotacao olhar direcao nova posicao
            Vector3 dir = pos1 - transform.position;
            balanca.transform.up = Vector3.Slerp(balanca.transform.up, -1 * dir, velocidadeRotacao * Time.deltaTime);
            // mover para pos nova
            if (Vector3.Distance(transform.position, pos1) > distanciaMinNovaPos)
            {
                transform.position = Vector3.Lerp(transform.position, pos1, Time.deltaTime * velocidadeGiro);
            }
            if (Vector3.Distance(transform.position, pos1) <= distanciaMinNovaPos)
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
