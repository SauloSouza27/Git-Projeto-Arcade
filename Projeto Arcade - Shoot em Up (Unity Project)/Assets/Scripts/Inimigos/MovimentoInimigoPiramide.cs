using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MovimentoInimigoPiramide : MonoBehaviour
{
    private GameObject alvo, controladorGame;
    public GameObject cabecaPiramide, pontaArma, balaPiramide;
    // Controle rotaçao
    public float velocidadeRotacao = 2.0f;
    public bool petBoss = false;
    // Pontos de vida
    public int pontosVida = 3;
    // XP quando morre
    public int xpInimigo = 100;
    // Tiro
    [Range(0, 3)] public float cooldown = 1.0f;
    private float contadorCooldown;
    public bool inverteRotacaoTiro = false;
    // materiais inimgo
    private MeshRenderer[] renderers;
    private Material[] materiais;
    // efeito explosão
    public GameObject fxExplosionPrefab, fxExpHit, fxExpHitPet;
    private void Awake()
    {
        controladorGame = GameObject.FindGameObjectWithTag("ControladorGame");
        alvo = GameObject.FindGameObjectWithTag("Player");
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
        contadorCooldown = 4.0f;
    }
    private void OnEnable()
    {
        if(controladorGame.GetComponent<ControladorGame>().nivel >= 8)
        {
            pontosVida = 12;
            xpInimigo = 150;
        }

        if (controladorGame.GetComponent<ControladorGame>().nivel >= 10)
        {
            pontosVida = 20;
            xpInimigo = 150;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (!petBoss)
        {
            MovimentaInimigoPiramide();
        }
         if (petBoss)
        {
            MovimentaInimigoPiramideBossPet();
        }

        // Cooldown e controle tiro
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0)
        {
            Tiro();
            contadorCooldown = cooldown;
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
    private void MovimentaInimigoPiramide()
    {
        // Rotacao corpo
        Vector3 direcao = alvo.transform.position - transform.position;
        direcao = direcao.normalized;
        transform.up = Vector3.Slerp(transform.up, -1 * direcao, velocidadeRotacao * Time.deltaTime);
        // Mira cabeca
        //cabecaPiramide.transform.up = Vector3.Slerp(cabecaPiramide.transform.up, -1 * direcao, 3 * anguloRotacao * Time.deltaTime);
        cabecaPiramide.transform.rotation = Quaternion.LookRotation(cabecaPiramide.transform.forward, - direcao);
        pontaArma.transform.rotation = Quaternion.LookRotation(pontaArma.transform.forward, direcao);
    }

    private void MovimentaInimigoPiramideBossPet()
    {
        Vector3 direcao = alvo.transform.position - cabecaPiramide.transform.position;
        direcao = direcao.normalized;
        cabecaPiramide.transform.rotation = Quaternion.LookRotation(cabecaPiramide.transform.forward, - direcao);
    }

    // Tiro
    private void Tiro()
    {
        if (inverteRotacaoTiro)
        {
            Instantiate(balaPiramide, pontaArma.transform.position, pontaArma.transform.rotation);
        }
        else
        {
            GameObject instaciaBala = Instantiate(balaPiramide, pontaArma.transform.position, pontaArma.transform.rotation);
            instaciaBala.GetComponent<BalaPersonagem>().velocidadeRotacao *= -1;
        }
    }
}
