using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoBesouro : MonoBehaviour
{
    public GameObject  besouro, bosta;
    private GameObject alvo;
    // Pontos de vida
    public int pontosVida = 6, bostaVida = 6;
    // XP quando morre
    public int xpInimigo = 5;
    // movimento
    public float velocidadeMovimento = 4.0f, anguloRotacao = 25.0f, velocidadeRotacaoBosta = 80.0f;
    private bool mudaDirecao = true, primeiroGiro = true;
    private float contadorCooldown;
    public float cooldownMudaDirecao = 2.0f;
    // materiais inimgo
    private MeshRenderer[] renderers;
    private Material[] materiais;
    // materiais bosta
    private MeshRenderer[] renderersBosta;
    private Material[] materiaisBosta;
    public CapsuleCollider colliderBosta;
    // efeito explosão
    public GameObject fxExplosionPrefab;
    private void Awake()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
        // Busca materiais do inimigo
        renderers = bosta.GetComponentsInChildren<MeshRenderer>();
        materiais = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            materiais[i] = renderers[i].material;
        }
        // Busca materiais da bosta
        renderersBosta = bosta.GetComponents<MeshRenderer>();
        materiaisBosta = new Material[renderersBosta.Length];
        for (int i = 0; i < renderersBosta.Length; i++)
        {
            materiaisBosta[i] = renderersBosta[i].material;
        }
    }

    private void Start()
    {
        cooldownMudaDirecao /= 2;
        contadorCooldown = cooldownMudaDirecao;
    }
    void Update()
    {
        Movimento();

        Utilidades.DestroyOutOfScreen(transform.position, gameObject);
    }
     private void Movimento()
    {
        //  rotacao bosta
        if (bosta != null)
        {
            bosta.transform.Rotate(velocidadeRotacaoBosta * Time.deltaTime, 0, 0, Space.Self);
        }
        // direcao
        besouro.transform.Translate(0, velocidadeMovimento * Time.deltaTime, 0, Space.Self);
        // rotacao
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        
        if (mudaDirecao)
        {
            besouro.transform.Rotate(0, 0, anguloRotacao * Time.deltaTime, Space.Self);
        }
        if (contadorCooldown == 0 && mudaDirecao == true)
        {
            mudaDirecao = false;
            if (primeiroGiro)
            {
                primeiroGiro = false;
                cooldownMudaDirecao *= 2;
            }
            contadorCooldown = cooldownMudaDirecao;
        }
        if (!mudaDirecao)
        {
            besouro.transform.Rotate(0, 0, - anguloRotacao * Time.deltaTime, Space.Self);
        }
        if (contadorCooldown == 0 && mudaDirecao == false)
        {
            mudaDirecao = true;
            contadorCooldown = cooldownMudaDirecao;
        }
    }

    private void CaluclaDanoBosta(int dano)
    {
        if (pontosVida > 0)
        {
            bostaVida -= dano;

            foreach (Material material in materiaisBosta)
            {
                StartCoroutine(Utilidades.PiscaCorRoutine(material));
            }
        }
        if (bostaVida <= 0)
        {
            Instantiate(fxExplosionPrefab, bosta.transform.position, bosta.transform.rotation);
            Destroy(bosta.gameObject);
        }
    }

    private void CaluclaDanoBesouro(int dano)
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
    private void OnCollisionEnter(Collision colisor)
    {
        if (bosta != null)
        {
            if (colisor.GetContact(0).thisCollider == colliderBosta)
            {
                if (colisor.gameObject.CompareTag("BalaPersonagem"))
                {
                    Destroy(colisor.gameObject);
                    int dano = alvo.GetComponent<ControlaPersonagem>().danoArmaPrincipal;

                    CaluclaDanoBosta(dano);
                }
                if (colisor.gameObject.CompareTag("BalaPet"))
                {
                    Destroy(colisor.gameObject);
                    int dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;

                    CaluclaDanoBosta(dano);
                }
                if (colisor.gameObject.CompareTag("OrbeGiratorio"))
                {
                    int dano = alvo.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;

                    CaluclaDanoBosta(dano);
                }
                if (colisor.gameObject.CompareTag("ProjetilSerra"))
                {
                    int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;

                    CaluclaDanoBosta(dano);
                }
                if (colisor.gameObject.CompareTag("Player"))
                {
                    int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;

                    CaluclaDanoBosta(dano);
                }
            }
            else
            {
                if (colisor.gameObject.CompareTag("BalaPersonagem"))
                {
                    Destroy(colisor.gameObject);
                }
                if (colisor.gameObject.CompareTag("BalaPet"))
                {
                    Destroy(colisor.gameObject);
                }
            }
        }
        
        if (bosta == null)
        {
            if (colisor.gameObject.CompareTag("BalaPersonagem"))
            {
                Destroy(colisor.gameObject);
                int dano = alvo.GetComponent<ControlaPersonagem>().danoArmaPrincipal;

                CaluclaDanoBesouro(dano);
            }
            if (colisor.gameObject.CompareTag("BalaPet"))
            {
                Destroy(colisor.gameObject);
                int dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;

                CaluclaDanoBesouro(dano);
            }
            if (colisor.gameObject.CompareTag("OrbeGiratorio"))
            {
                int dano = alvo.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;

                CaluclaDanoBesouro(dano);
            }
            if (colisor.gameObject.CompareTag("ProjetilSerra"))
            {
                int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;

                CaluclaDanoBesouro(dano);
            }
            if (colisor.gameObject.CompareTag("Player"))
            {
                int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;

                CaluclaDanoBesouro(dano);
            }
        }
    }
}
