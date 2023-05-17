using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoBesouro : MonoBehaviour
{
    public GameObject controladorGame, alvo, besouro, bosta;
    // Pontos de vida
    public int pontosVida = 2;
    // XP quando morre
    public int xpInimigo = 5;
    // movimento
    public float velocidadeMovimento = 2.0f, velocidadeRotacao = 2.5f, velocidadeRotacaoBosta = 2.0f;
    private bool mudaDirecao = true, primeiroGiro = true;
    private float contadorCooldown;
    [Range(0.0f, 8.0f)] public float cooldownMudaDirecao = 2.0f;
    // materiais inimgo
    private MeshRenderer[] renderers;
    private Material[] materiais;
    // efeito explosão
    public GameObject fxExplosionPrefab;
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
        bosta.transform.Rotate(velocidadeRotacaoBosta * Time.deltaTime, 0, 0, Space.Self);
        // direcao
        besouro.transform.Translate(0, velocidadeMovimento * Time.deltaTime, 0, Space.Self);
        // rotacao
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        
        if (mudaDirecao)
        {
            besouro.transform.Rotate(0, 0, velocidadeRotacao * Time.deltaTime, Space.Self);
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
            besouro.transform.Rotate(0, 0, - velocidadeRotacao * Time.deltaTime, Space.Self);
        }
        if (contadorCooldown == 0 && mudaDirecao == false)
        {
            mudaDirecao = true;
            contadorCooldown = cooldownMudaDirecao;
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
    private void OnCollisionEnter(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("BalaPersonagem"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<ControlaPersonagem>().danoArmaPrincipal;

            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("BalaPet"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;

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
    }
}
