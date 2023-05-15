using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBoss : MonoBehaviour
{
    private GameObject alvo, controladorGame;
    // Controle rotaçao
    public GameObject cabecaPiramide, corpoPiramide;
    public float velocidadeRotacao = 2.0f;
    // Controle movimento cabeca quando perde o corpo
    public float velocidadeCabeca = 1.0f, tempoParado = 1.0f;
    private Vector3 posAlvo;
    // arma cabeca
    public GameObject[] armasBoss;
    // pets boss
    public GameObject petsBoss;
    // vidas do boss
    private bool tomaDano = false;
    public int vidaCorpo = 40, vidaCabeca = 40;
    public bool bossIsDead = false;
    // Materiais
    private MeshRenderer[] renderers;
    private Material[] materiais;
    // Animator
    private Animator animator;
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
        // desativa box collider da cabeca
        cabecaPiramide.GetComponent<BoxCollider>().enabled = false;
        // busca animator
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentaBossPiramide();

        if (armasBoss[0] == null && armasBoss[1] == null && cabecaPiramide != null)
        {
            cabecaPiramide.GetComponent<BoxCollider>().enabled = true;
            tomaDano = true;
        }

        if(corpoPiramide == null)
        {
            animator.enabled = false;
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
                    Invoke(nameof(BuscaNovaPosicaoPlayer), 4.0f);
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
                    Invoke(nameof(BuscaNovaPosicaoPlayer), 4.0f);
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
                    Invoke(nameof(BuscaNovaPosicaoPlayer), 4.0f);
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
                    Invoke(nameof(BuscaNovaPosicaoPlayer), 4.0f);
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
                    Invoke(nameof(BuscaNovaPosicaoPlayer), 4.0f);
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
        if (tomaDano && corpoPiramide != null)
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
                    foreach (Material material in materiais)
                    {
                        StartCoroutine(Utilidades.PiscaCorRoutine(material));
                    }
                }
                if (vidaCorpo <= 0)
                {
                    Invoke(nameof(BuscaNovaPosicaoPlayer), 4.0f);
                    Destroy(corpoPiramide);
                }
            }
        }
        if (tomaDano && corpoPiramide == null)
        {
            float contadorCooldown, cooldown = 0.5f;
            contadorCooldown = cooldown;
            Utilidades.CalculaCooldown(contadorCooldown);
            contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
            if (colisor.gameObject.CompareTag("ProjetilSerra"))
            {
                int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;
                if (vidaCabeca > 0 && contadorCooldown == 0)
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
            Vector3 direcaoCabeca = alvo.transform.position - cabecaPiramide.transform.position;
            //cabecaPiramide.transform.up = Vector3.Slerp(cabecaPiramide.transform.up, -1 * direcao, 3 * velocidadeRotacao * Time.deltaTime);
            cabecaPiramide.transform.rotation = Quaternion.LookRotation(cabecaPiramide.transform.forward, -direcaoCabeca);
            if (corpoPiramide == null && posAlvo != null)
            {
                Vector3 posCabeca = cabecaPiramide.transform.position;
                if (Vector3.Distance(posAlvo, posCabeca) > 0.8)
                {
                    cabecaPiramide.transform.position = Vector3.Lerp(posCabeca, posAlvo, velocidadeCabeca * Time.deltaTime);
                }
                if (Vector3.Distance(posAlvo, posCabeca) < 0.8)
                {
                    Invoke(nameof(BuscaNovaPosicaoPlayer), tempoParado);
                }
            }
        }
    }
    private void BuscaNovaPosicaoPlayer()
    {
        posAlvo = alvo.transform.position;
        CancelInvoke(nameof(BuscaNovaPosicaoPlayer));
    }
}
