using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MovimentoInimigoPiramide : MonoBehaviour
{
    private GameObject alvo;
    public GameObject cabecaPiramide, pontaArma, balaPiramide;
    // Controle rotaçao
    public float velocidadeRotacao = 2.0f;
    // Pontos de vida
    public float pontosVida = 40.0f;
    public float danoContato = 40.0f;
    public float danoTiro = 40.0f;
    // XP quando morre
    public int xpInimigo = 100;
    // Tiro
    [Range(0, 3)] public float cooldown = 1.0f;
    private float contadorCooldown;
    public bool inverteRotacaoTiro = false;

    MeshRenderer[] renderers;
    Material[] materiais;
    private void Awake()
    {
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

    void Update()
    {
        MovimentaInimigoPiramide();

        // Cooldown e controle tiro
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0)
        {
            Tiro();
            contadorCooldown = cooldown;
        }
    }

    private void OnCollisionEnter(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("BalaPersonagem"))
        {
            Destroy(colisor.gameObject);
            float dano = alvo.GetComponent<DisparoArma>().danoArmaPrincipal;
            if (pontosVida > 0)
            {
                pontosVida -= dano;

                foreach(Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (pontosVida <= 0)
            {
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
        if (colisor.gameObject.CompareTag("BalaPet"))
        {
            Destroy(colisor.gameObject);
            float dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;
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
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
        if (colisor.gameObject.CompareTag("Player"))
        {
            float dano = colisor.gameObject.GetComponent<ControlaPersonagem>().danoContato;
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
                Destroy(gameObject);
                ControladorGame.instancia.SomaXP(xpInimigo);
            }
        }
    }

    private void MovimentaInimigoPiramide()
    {
        // Rotacao corpo
        Vector3 direcao = alvo.transform.position - transform.position;
        direcao = direcao.normalized;
        transform.up = Vector3.Slerp(transform.up, -1 * direcao, velocidadeRotacao * Time.deltaTime);
        // Mira cabeca
        cabecaPiramide.transform.up = Vector3.Slerp(cabecaPiramide.transform.up, -1 * direcao, 3 * velocidadeRotacao * Time.deltaTime);
    }

    // Tiro
    private void Tiro()
    {
        if (inverteRotacaoTiro)
        {
            Quaternion rotacaoPontaArma = pontaArma.transform.rotation;
            rotacaoPontaArma = new Quaternion(rotacaoPontaArma.x, rotacaoPontaArma.y, -rotacaoPontaArma.z, rotacaoPontaArma.w);
            Instantiate(balaPiramide, pontaArma.transform.position, pontaArma.transform.rotation);
        }
        else
        {
            Instantiate(balaPiramide, pontaArma.transform.position, pontaArma.transform.rotation);
        }
    }
}
