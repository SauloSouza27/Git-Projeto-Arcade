using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBalanca : MonoBehaviour
{
    private GameObject alvo;
    // Pontos de vida
    public int pontosVida = 60;
    // XP quando morre
    public int xpInimigo = 300;
    public GameObject balanca, projetilBalanca, pontaSaidaBalanca;
    [Range(0, 6)] public float cooldown = 3.0f, velocidadeRotacao = 2.0f;
    private float contadorCooldown;
    public bool inverteRotacao = false;
    // movimento giro
    public float velocidadeGiro = 5.0f;
    public bool isPos1 = false, isPos2 = false, isPos3 = false, isPos4 = false;
    private Vector3 pos1, pos2, pos3, pos4;
    private bool seMovimenta = false;
    // materiais inimgo
    private MeshRenderer[] renderers;
    private Material[] materiais;
    // efeito explosão
    public GameObject fxExplosionPrefab, fxExpHit, fxExpHitPet;

    private void Awake()
    {
        pos1 = new Vector3(26f, 29f, 0f);
        pos2 = new Vector3(-26f, 29f, 0f);
        pos3 = new Vector3(-26f, 3.0f, 0f);
        pos4 = new Vector3(26f, 3.0f, 0f);
        // Busca materiais do inimigo
        renderers = GetComponentsInChildren<MeshRenderer>();
        materiais = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            materiais[i] = renderers[i].material;
        }
    }
    void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
        contadorCooldown = 7.0f;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentoRotacaoBalanca();

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

    private void MovimentoRotacaoBalanca()
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
    private void CaluclaDanoInimigo(int dano)
    {
        if (pontosVida > 0)
        {
            pontosVida -= dano;

            foreach (Material material in materiais)
            {
                StartCoroutine(Utilidades.PiscaCorRoutine(material));
            }
        }
        if (pontosVida <= 0)
        {
            Instantiate(fxExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            ControladorGame.instancia.SomaXP(xpInimigo);
        }
    }
    private void FXExplosionHit(GameObject fxExpHit, Collision colisor)
    {
        ContactPoint point = colisor.GetContact(0);
        Vector3 pos = point.point;
        Instantiate(fxExpHit, pos, colisor.transform.rotation);
    }
    private void OnCollisionEnter(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("BalaPersonagem"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<ControlaPersonagem>().danoArmaPrincipal;

            FXExplosionHit(fxExpHit, colisor);
            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("BalaPet"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;

            FXExplosionHit(fxExpHitPet, colisor);
            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("OrbeGiratorio"))
        {
            int dano = alvo.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;

            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("ProjetilSerra"))
        {
            int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;

            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("Player"))
        {
            int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;

            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("Escudo"))
        {
            int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;

            CaluclaDanoInimigo(dano);
        }
    }
    private void OnCollisionExit(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("ProjetilSerra"))
        {
            int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;

            CaluclaDanoInimigo(dano);
        }
    }
}
