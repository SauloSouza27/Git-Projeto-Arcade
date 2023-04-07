using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ControlaPersonagem : MonoBehaviour
{
    // Controle movimento personagem e armas
    private float x, y;
    public float velocidadeMovimento = 1.0f;
    public GameObject personagem, armaPrincipal, armaPets, petEsq, petDir, pontaPetEsq, pontaPetDir;
    private GameObject alvoPet;
    public float velocidadeRotacaoPet = 2.0f, distanciaMinPetAtirar = 20.0f;
    // Pontos de vida
    public float pontosVida = 100.0f;
    public float danoContato = 20.0f;

    public ParticleSystem particulasDano;

    Material[] materiais;
    Color[] coresOriginais;

    void Start()
    {
        // Cursor Config
        //Cursor.lockState = CursorLockMode.Confined;

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        materiais = new Material[renderers.Length];
        coresOriginais = new Color[renderers.Length];
        for(int i = 0; i < renderers.Length; i++) 
        {
            materiais[i] = renderers[i].material;
            coresOriginais[i] = materiais[i].color;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        ControleMovimentoPersonagem();

        ControleArmaPrincipal();

        if (ControladorGame.instancia.armaPetAtivada == true)
        {
            MovimentoPets();
        }

        //mudança da cor do material
        for(int i = 0; i < materiais.Length; i++) 
        {
            materiais[i].color = Color.Lerp(materiais[i].color, coresOriginais[i], velocidadeCorMaterial * Time.deltaTime);
        }
    }

    // Controle movimento personagem
    public void ControleMovimentoPersonagem()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (x != 0 || y != 0)
        {
            personagem.transform.Translate(x * Time.deltaTime * velocidadeMovimento, y * Time.deltaTime * velocidadeMovimento, 0, Space.World);
            personagem.transform.position = Utilidades.TravaPosicao(personagem.transform.position);
        }
    }

    // Controle rotação arma principal
    private void ControleArmaPrincipal()
    {
        Vector3 position = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 30));
        Vector3 dirMouse = mousePos - armaPrincipal.transform.position;
        dirMouse = dirMouse.normalized;
        armaPrincipal.transform.rotation = Quaternion.LookRotation(armaPrincipal.transform.forward, dirMouse);
    }

    // Achar inimigo mais perto
    public GameObject AcharInimigoMaisPerto()
    {
        GameObject[] todosInimigos;
        todosInimigos = GameObject.FindGameObjectsWithTag("Inimigo");
        GameObject inimigoMaisProximo = null;
        float distancia = Mathf.Infinity;
        Vector3 posicao = armaPets.transform.position;
        foreach (GameObject inimigoPerto in todosInimigos)
        {
            Vector3 diferenca = inimigoPerto.transform.position - posicao;
            float testeDistancia = diferenca.sqrMagnitude;
            if (testeDistancia < distancia)
            {
                inimigoMaisProximo = inimigoPerto;
                distancia = testeDistancia;
            }
        }
        return inimigoMaisProximo;
    }

    // controle movimento do pet
    public void MovimentoPets()
    {
        alvoPet = AcharInimigoMaisPerto();
        Vector3 dirAlvoPet = alvoPet.transform.position - armaPets.transform.position;
        float distanciaAlvo = dirAlvoPet.magnitude;
        if (distanciaAlvo > distanciaMinPetAtirar)
        {
            petEsq.transform.rotation = Quaternion.Slerp(petEsq.transform.rotation, new Quaternion(0, 0, 0, 0), velocidadeRotacaoPet * Time.deltaTime);
            petDir.transform.rotation = Quaternion.Slerp(petDir.transform.rotation, new Quaternion(0, 0, 0, 0), velocidadeRotacaoPet * Time.deltaTime);
            pontaPetEsq.transform.rotation = Quaternion.Slerp(pontaPetEsq.transform.rotation, new Quaternion(0, 0, 0, 0), velocidadeRotacaoPet * Time.deltaTime);
            pontaPetDir.transform.rotation = Quaternion.Slerp(pontaPetDir.transform.rotation, new Quaternion(0, 0, 0, 0), velocidadeRotacaoPet * Time.deltaTime);
        }
        if (distanciaAlvo <= distanciaMinPetAtirar)
        {
            Vector3 dirAlvoPetEsq = alvoPet.transform.position - pontaPetEsq.transform.position;
            dirAlvoPetEsq = dirAlvoPetEsq.normalized;
            //petEsq.transform.rotation = Quaternion.LookRotation(petEsq.transform.forward, dirAlvoPetEsq);
            petEsq.transform.up = Vector3.Slerp(petEsq.transform.up, dirAlvoPetEsq, velocidadeRotacaoPet * Time.deltaTime);
            pontaPetEsq.transform.rotation = Quaternion.LookRotation(pontaPetEsq.transform.forward, dirAlvoPetEsq);
            Vector3 dirAlvoPetDir = alvoPet.transform.position - pontaPetDir.transform.position;
            dirAlvoPetDir = dirAlvoPetDir.normalized;
            //petDir.transform.rotation = Quaternion.LookRotation(petDir.transform.forward, dirAlvoPetDir);
            petDir.transform.up = Vector3.Slerp(petDir.transform.up, dirAlvoPetEsq, velocidadeRotacaoPet * Time.deltaTime);
            pontaPetDir.transform.rotation = Quaternion.LookRotation(pontaPetDir.transform.forward, dirAlvoPetDir);
        }
    }

    

    void ReceberDano()
    {
        if (particulasDano)
            particulasDano.Play();

        foreach(Material mat in materiais)
        {
            mat.color += Color.red;
        }
    }

    // Dano Inimigos
    private void OnCollisionEnter(Collision colisor)
    {
        GameObject inimigo = colisor.gameObject;
        if (inimigo.name == "Inimigo Pequeno(Clone)")
        {
            float dano = inimigo.GetComponent<MovimentoInimigoPequeno>().danoContato;
            pontosVida -= dano;

            ReceberDano();
        }
        if (inimigo.name == "Inimigo Grande(Clone)" || inimigo.name == "Inimigo Grande" || inimigo.name == "Inimigo Grande (1)")
        {
            float dano = inimigo.GetComponent<MovimentoInimigoGrande>().danoContato;
            pontosVida -= dano;

            ReceberDano();
        }
        if (pontosVida <= 0)
        {
            Destroy(personagem);
        }
    }
}
