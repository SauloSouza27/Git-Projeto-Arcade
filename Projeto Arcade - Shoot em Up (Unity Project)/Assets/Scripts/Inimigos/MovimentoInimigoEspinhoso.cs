using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoEspinhoso : MonoBehaviour
{
    private GameObject alvo, controladorGame;
    // movimento
    public float velocidadeMovimento = 5.0f, velocidadeSeguir = 5.0f, velocidadeRotacao = 5.0f, distanciaMinSeguir = 15;
    private Vector3 posAlvo;
    private bool achouAlvo = false;
    // Pontos de vida
    public int pontosVida = 1;
    // XP quando morre
    public int xpInimigo = 10;
    // pisca Explosion
    private MeshRenderer render;
    private Material material, materialOriginal;
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

        // Busca material do Aviso Explosão
        render = GetComponentInChildren<MeshRenderer>();
        materialOriginal = render.material;
        material = new Material(render.material);
    }

    private void OnEnable()
    {
        if (controladorGame.GetComponent<ControladorGame>().nivel >= 8)
        {
            xpInimigo = 15;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentaInimigoEspinhoso();
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

    private void MovimentaInimigoEspinhoso()
    {
        Vector3 dir = alvo.transform.position - transform.position;
        // Movimento basico
        if (dir.magnitude > distanciaMinSeguir && !achouAlvo)
        {
            transform.Translate(0, -velocidadeMovimento * Time.deltaTime, 0);
        }
        //Movimento de seguir jogador
        if (dir.magnitude <= distanciaMinSeguir)
        {
            if (!achouAlvo)
            {
                BuscaNovaPosicaoPlayer();
            }
        }

        if (achouAlvo)
        {
            transform.position = Vector3.Lerp(transform.position, posAlvo, velocidadeSeguir * Time.deltaTime);
            //rotaçao
            Vector3 dirSeguir = posAlvo - transform.position;
            transform.up = Vector3.Slerp(transform.up, -1 * dirSeguir, velocidadeRotacao * Time.deltaTime);
        }
    }
    private void BuscaNovaPosicaoPlayer()
    {
        posAlvo = alvo.transform.position;
        achouAlvo = true;
    }
}
