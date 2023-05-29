using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoEspinhoso : MonoBehaviour
{
    private GameObject alvo, controladorGame;
    // movimento
    public float velocidadeMovimento = 5.0f, velocidadeSeguir = 5.0f, velocidadeRotacao = 5.0f, distanciaMinSeguir = 15, velocidadeMudaCor = 1.0f, tempoPisca = 1.0f;
    private bool aumentaBrilho = true;
    private float count, intensidadeCor = 1.0f, tempoExplodir = 3.0f;
    private Vector3 posAlvo;
    private bool achouAlvo = false;
    // Pontos de vida
    public int pontosVida = 1;
    // XP quando morre
    public int xpInimigo = 10;
    // pisca Explosion
    private MeshRenderer render;
    // materiais inimgo
    private MeshRenderer[] renderers;
    private Material[] materiais;
    // efeito explosão
    public GameObject fxExplosionPrefab, fxTimerExplosionPrefab;
    // trail
    public TrailRenderer trail;
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
    }

    private void Start()
    {
        // contador pisca brilho
        count = tempoPisca;
    }

    private void OnEnable()
    {
        
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentaInimigoEspinhoso();

        Utilidades.DestroyOutOfScreen(transform.position, gameObject);
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
            if (!achouAlvo)
            {
                Instantiate(fxExplosionPrefab, transform.position, transform.rotation);
            }
            if (achouAlvo)
            {
                Instantiate(fxTimerExplosionPrefab, transform.position, transform.rotation);
            }

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
            AlternaCorEmission();
            //deslocamento
            transform.position = Vector3.Lerp(transform.position, posAlvo, velocidadeSeguir/(100.0f + Time.deltaTime));
            //rotaçao
            Vector3 dirSeguir = posAlvo - transform.position;
            transform.up = Vector3.Slerp(transform.up, -1 * dirSeguir, velocidadeRotacao * Time.deltaTime);
            tempoExplodir -= Time.deltaTime;
            if (tempoExplodir <= 0)
            {
                Instantiate(fxTimerExplosionPrefab, transform.position, transform.rotation);
                ControladorGame.instancia.SomaXP(xpInimigo);
                Destroy(gameObject);
            }
        }
    }

    private void AlternaCorEmission()
    {
        
        if (aumentaBrilho)
        {
            if (count > 0)
            {
                AumentaIntensidadeEmissao();
                count -= Time.deltaTime;
            }
            if (count <= 0)
            {
                count = tempoPisca;
                aumentaBrilho = false;
            }
        }
        if (!aumentaBrilho)
        {
            if (count > 0)
            {
                DiminuiIntensidadeEmissao();
                count -= Time.deltaTime;
            }
            if (count <= 0)
            {
                count = tempoPisca;
                aumentaBrilho = true;
            }
        }
    }

    private void AumentaIntensidadeEmissao()
    {
        Color brilhoForte = new Color(0.6745f, 0.2078f, 0.8784f) * intensidadeCor;
        render.material.SetColor("_EmissionColor", brilhoForte);
        intensidadeCor += velocidadeMudaCor * Time.deltaTime;
    }
    private void DiminuiIntensidadeEmissao()
    {
        Color brilhoFraco = new Color(0.6745f, 0.2078f, 0.8784f) * intensidadeCor;
        render.material.SetColor("_EmissionColor", brilhoFraco);
        intensidadeCor -= velocidadeMudaCor * Time.deltaTime;
    }
    private void BuscaNovaPosicaoPlayer()
    {
        posAlvo = alvo.transform.position;
        achouAlvo = true;
        trail.endColor = new Color(1, 0, 1);
    }
}
