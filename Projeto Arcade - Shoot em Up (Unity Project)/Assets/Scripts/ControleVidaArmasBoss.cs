using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleVidaArmasBoss : MonoBehaviour
{
    private GameObject alvo, controladorGame;
    public GameObject boss;
    // vida
    public int vidaArma = 20;
    // Tiro
    public GameObject arma, pontArma, bala;
    [Range(0, 5)] public float cooldown = 0.3f, tempoDisparo = 3.0f;
    private float contadorCooldown;
    private float contadorDisparos;
    public int numeroDisparos = 10;
    private bool ativaArma = false;
    // Materiais
    MeshRenderer[] renderers;
    Material[] materiais;
    // box collider
    CapsuleCollider colisor;
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
        // busca colisor
        colisor = alvo.GetComponent<CapsuleCollider>();
        colisor.enabled = false;
    }
    private void Start()
    {
        ativaArma = false;
        StartCoroutine(IntervaloDisparo(12.0f));
        StartCoroutine(AtrasaColisorArma(colisor, 10.0f));
    }
    private void Update()
    {
        MiraArmaBoss();

        // disparo armas boss
        if (ativaArma)
        {
            // Cooldown e controle tiro
            Utilidades.CalculaCooldown(contadorCooldown);
            contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
            if (contadorCooldown == 0)
            {
                Instantiate(bala, pontArma.transform.position, pontArma.transform.rotation);
                contadorCooldown = cooldown;
                contadorDisparos++;
                if (contadorDisparos < numeroDisparos) return;
                else
                {
                    contadorDisparos = 0;
                    ativaArma = false;
                    StartCoroutine(IntervaloDisparo(tempoDisparo));
                }
            }
        }
    }
    private void MiraArmaBoss()
    {
        // Rotacao armas
        Vector3 dir = alvo.transform.position - arma.transform.position;
        dir = dir.normalized;
        arma.transform.rotation = Quaternion.LookRotation(arma.transform.forward, dir);
    }
    private IEnumerator IntervaloDisparo(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        ativaArma = true;
    }

    private IEnumerator AtrasaColisorArma(CapsuleCollider colisor, float delay)
    {
        yield return new WaitForSeconds(delay);
        colisor.enabled = true;
    }
    private void OnCollisionEnter(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("BalaPersonagem"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<ControlaPersonagem>().danoArmaPrincipal;
            if (vidaArma > 0)
            {
                vidaArma -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (vidaArma <= 0)
            {
                boss.GetComponent<MovimentoBoss>().controleArmasDestruidas += 1;
                Destroy(gameObject);
            }
        }
        if (colisor.gameObject.CompareTag("BalaPet"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;
            if (vidaArma > 0)
            {
                vidaArma -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (vidaArma <= 0)
            {
                boss.GetComponent<MovimentoBoss>().controleArmasDestruidas += 1;
                Destroy(gameObject);
            }
        }
        if (colisor.gameObject.CompareTag("OrbeGiratorio"))
        {
            int dano = alvo.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;
            if (vidaArma > 0)
            {
                vidaArma -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (vidaArma <= 0)
            {
                boss.GetComponent<MovimentoBoss>().controleArmasDestruidas += 1;
                Destroy(gameObject);
            }
        }
        if (colisor.gameObject.CompareTag("ProjetilSerra"))
        {
            int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;
            if (vidaArma > 0)
            {
                vidaArma -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (vidaArma <= 0)
            {
                boss.GetComponent<MovimentoBoss>().controleArmasDestruidas += 1;
                Destroy(gameObject);
            }
        }
        if (colisor.gameObject.CompareTag("Player"))
        {
            int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;
            if (vidaArma > 0)
            {
                vidaArma -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (vidaArma <= 0)
            {
                boss.GetComponent<MovimentoBoss>().controleArmasDestruidas += 1;
                Destroy(gameObject);
            }
        }
    }
}
