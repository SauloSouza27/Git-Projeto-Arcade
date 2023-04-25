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
    public GameObject armaEsq, armaDir, bala;
    // Tiro
    [Range(0, 5)] public float cooldown = 0.3f, tempoDisparo = 3.0f;
    private float contadorCooldown;
    private float contadorDisparos;
    public int numeroDisparos = 10;
    private bool ativaArma = false;
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
    }
    private void Start()
    {
        ativaArma = true;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentaBossPiramide();

        Debug.Log(ativaArma);
        Debug.Log("Contagem tiros: " + contadorDisparos);
        if (ativaArma)
        {
            // Cooldown e controle tiro
            Utilidades.CalculaCooldown(contadorCooldown);
            contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
            if (contadorCooldown == 0)
            {
                Instantiate(bala, armaEsq.transform.position, armaEsq.transform.rotation);
                Instantiate(bala, armaDir.transform.position, armaDir.transform.rotation);
                contadorCooldown = cooldown;
                contadorDisparos++;
                if(contadorDisparos < numeroDisparos) return;
                else
                {
                    contadorDisparos = 0;
                    ativaArma = false;
                    StartCoroutine(IntervaloDisparo());
                }
            }
        }
    }
    private void MovimentaBossPiramide()
    {
        // Rotacao corpo
        Vector3 direcao = alvo.transform.position - transform.position;
        direcao = direcao.normalized;
        corpoPiramide.transform.up = Vector3.Slerp(corpoPiramide.transform.up, - direcao, velocidadeRotacao * Time.deltaTime);
        // Mira cabeca
        //cabecaPiramide.transform.up = Vector3.Slerp(cabecaPiramide.transform.up, -1 * direcao, 3 * velocidadeRotacao * Time.deltaTime);
        cabecaPiramide.transform.rotation = Quaternion.LookRotation(cabecaPiramide.transform.forward, - direcao);
    }
    private IEnumerator IntervaloDisparo()
    {
        yield return new WaitForSeconds(tempoDisparo);
        ativaArma = true;
    }
}
