using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBoss : MonoBehaviour
{
    private GameObject alvo, controladorGame;
    public GameObject cabecaPiramide, corpoPiramide;
    // Controle rotaçao
    public float velocidadeRotacao = 2.0f;
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
    private void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentaBossPiramide();
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
}
