using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBoss : MonoBehaviour
{
    private GameObject alvo, controladorGame;
    // Controle rotaçao
    public GameObject cabecaPiramide, corpoPiramide;
    public float velocidadeRotacao = 2.0f;
    // arma cabeca
    public int controleArmasDestruidas = 0;
    public GameObject[] armasBoss;
    // pets boss
    public GameObject petsBoss;
    // vidas do boss
    private bool tomaDano = false;
    public int vidaCorpo = 40, vidaCabeca = 40;
    public bool bossIsDead = false;
    // Materiais
    MeshRenderer[] renderers;
    Material[] materiais;
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

        GetComponent<BoxCollider>().enabled = false;
    }


    private void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentaBossPiramide();

        if (armasBoss == null)
        {
            GetComponent<BoxCollider>().enabled = true;
            tomaDano = true;
        }
    }
    // controle vida boss
    private void OnCollisionEnter(Collision colisor)
    {
        if (tomaDano && corpoPiramide != null)
        {
            if (colisor.gameObject.CompareTag("BalaPersonagem"))
            {
                Destroy(colisor.gameObject);
                int dano = alvo.GetComponent<ControlaPersonagem>().danoArmaPrincipal;
                if (vidaCorpo > 0)
                {
                    vidaCorpo -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCorpo <= 0)
                {
                    Destroy(corpoPiramide);
                }
            }
            if (colisor.gameObject.CompareTag("BalaPet"))
            {
                Destroy(colisor.gameObject);
                int dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;
                if (vidaCorpo > 0)
                {
                    vidaCorpo -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCorpo <= 0)
                {
                    Destroy(corpoPiramide);
                }
            }
            if (colisor.gameObject.CompareTag("OrbeGiratorio"))
            {
                int dano = alvo.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;
                if (vidaCorpo > 0)
                {
                    vidaCorpo -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCorpo <= 0)
                {
                    Destroy(corpoPiramide);
                }
            }
            if (colisor.gameObject.CompareTag("ProjetilSerra"))
            {
                int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;
                if (vidaCorpo > 0)
                {
                    vidaCorpo -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCorpo <= 0)
                {
                    Destroy(corpoPiramide);
                }
            }
            if (colisor.gameObject.CompareTag("Player"))
            {
                int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;
                if (vidaCorpo > 0)
                {
                    vidaCorpo -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCorpo <= 0)
                {
                    Destroy(corpoPiramide);
                }
            }
        }
        if (tomaDano && corpoPiramide == null)
        {
            if (colisor.gameObject.CompareTag("BalaPersonagem"))
            {
                Destroy(colisor.gameObject);
                int dano = alvo.GetComponent<ControlaPersonagem>().danoArmaPrincipal;
                if (vidaCabeca > 0)
                {
                    vidaCabeca -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCabeca <= 0)
                {
                    bossIsDead = true;
                    Destroy(cabecaPiramide);
                }
            }
            if (colisor.gameObject.CompareTag("BalaPet"))
            {
                Destroy(colisor.gameObject);
                int dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;
                if (vidaCabeca > 0)
                {
                    vidaCabeca -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCabeca <= 0)
                {
                    bossIsDead = true;
                    Destroy(cabecaPiramide);
                }
            }
            if (colisor.gameObject.CompareTag("OrbeGiratorio"))
            {
                int dano = alvo.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;
                if (vidaCabeca > 0)
                {
                    vidaCabeca -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCabeca <= 0)
                {
                    bossIsDead = true;
                    Destroy(cabecaPiramide);
                }
            }
            if (colisor.gameObject.CompareTag("ProjetilSerra"))
            {
                int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;
                if (vidaCabeca > 0)
                {
                    vidaCabeca -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCabeca <= 0)
                {
                    bossIsDead = true;
                    Destroy(cabecaPiramide);
                }
            }
            if (colisor.gameObject.CompareTag("Player"))
            {
                int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;
                if (vidaCabeca > 0)
                {
                    vidaCabeca -= dano;

                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCabeca <= 0)
                {
                    bossIsDead = true;
                    Destroy(cabecaPiramide);
                }
            }
        }
    }
    private void OnCollisionStay(Collision colisor)
    {
        float contadorCooldown, cooldown = 0.5f;
        contadorCooldown = cooldown;
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (colisor.gameObject.CompareTag("ProjetilSerra"))
        {
            int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;
            if (vidaCorpo > 0 && contadorCooldown == 0)
            {
                vidaCorpo -= dano;
                contadorCooldown = cooldown;
                foreach (Material material in materiais)
                {
                    StartCoroutine(controladorGame.GetComponent<ControladorGame>().AtivaInimigo(petsBoss, 4.0f));
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (vidaCorpo <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void MovimentaBossPiramide()
    {
        Vector3 direcao = alvo.transform.position - transform.position;
        direcao = direcao.normalized;

        if (corpoPiramide != null)
        {
            // Rotacao corpo
            corpoPiramide.transform.up = Vector3.Slerp(corpoPiramide.transform.up, - direcao, velocidadeRotacao * Time.deltaTime);
        }
        if (cabecaPiramide != null)
        {
            // Mira cabeca
            //cabecaPiramide.transform.up = Vector3.Slerp(cabecaPiramide.transform.up, -1 * direcao, 3 * velocidadeRotacao * Time.deltaTime);
            cabecaPiramide.transform.rotation = Quaternion.LookRotation(cabecaPiramide.transform.forward, -direcao);
        }
    }
}
