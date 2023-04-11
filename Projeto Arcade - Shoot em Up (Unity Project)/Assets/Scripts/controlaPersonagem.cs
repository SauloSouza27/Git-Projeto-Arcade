//using OpenCover.Framework.Model; (deu erro no build)
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView; (deu erro no build)
using UnityEngine;

public class ControlaPersonagem : MonoBehaviour
{
    // Controle movimento personagem e armas
    private float x, y;
    public float velocidadeMovimento = 1.0f, velocidadeCorMaterial = 2.0f;
    public GameObject personagem, armaPrincipal, armaPets, petEsq, petDir, pontaPetEsq, pontaPetDir;
    private GameObject alvoPet;
    public float velocidadeRotacaoPet = 2.0f, distanciaMinPetAtirar = 20.0f;
    // Pontos de vida
    public float pontosVida = 100.0f;
    public float danoContato = 20.0f;
    // Particulas
    public ParticleSystem particulasDano;

    Material[] materiais;
    Color[] coresOriginais;

    void Start()
    {
        // Cursor Config
        //Cursor.lockState = CursorLockMode.Confined;

        // Busca materiais do personagem
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

        if (ControladorGame.instancia.armaPetAtivada)
        {
            ControleArmaPets();
        }

        //mudança da cor do material
        RetornaCorOriginal();
    }

    // Dano Inimigos
    private void OnCollisionEnter(Collision colisor)
    {
        GameObject inimigo = colisor.gameObject;
        if (pontosVida > 0)
        {
            ReceberDano(inimigo);
        }
    }

    // Morte Personagem
    public void MorteJogador()
    {
        EfeitoTomaDano();
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    // Controle movimento personagem
    public void ControleMovimentoPersonagem()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
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
    public void ControleArmaPets()
    {
        alvoPet = AcharInimigoMaisPerto();
        if (alvoPet != null)
        {
            Vector3 dirAlvoPet = alvoPet.transform.position - armaPets.transform.position;
            float distanciaAlvo = dirAlvoPet.magnitude;
            if (distanciaAlvo > distanciaMinPetAtirar)
            {
                petEsq.transform.rotation = Quaternion.Slerp(petEsq.transform.rotation, new Quaternion(0, 0, 0, 1), velocidadeRotacaoPet * Time.deltaTime);
                petDir.transform.rotation = Quaternion.Slerp(petDir.transform.rotation, new Quaternion(0, 0, 0, 1), velocidadeRotacaoPet * Time.deltaTime);

                pontaPetEsq.transform.rotation = Quaternion.Slerp(pontaPetEsq.transform.rotation, new Quaternion(0, 0, 0, 1), velocidadeRotacaoPet * Time.deltaTime);
                pontaPetDir.transform.rotation = Quaternion.Slerp(pontaPetDir.transform.rotation, new Quaternion(0, 0, 0, 1), velocidadeRotacaoPet * Time.deltaTime);
            }
            if (distanciaAlvo <= distanciaMinPetAtirar)
            {

                // (opcao de rotacao instantanea) gameObject.transform.rotation = Quaternion.LookRotation(pontaPetDir.transform.forward, dirAlvoPetDir);

                Vector3 dirAlvoPetEsq = alvoPet.transform.position - pontaPetEsq.transform.position;
                dirAlvoPetEsq = dirAlvoPetEsq.normalized;
                petEsq.transform.up = Vector3.Slerp(petEsq.transform.up, dirAlvoPetEsq, velocidadeRotacaoPet * Time.deltaTime);
                pontaPetEsq.transform.up = Vector3.Slerp(pontaPetEsq.transform.up, dirAlvoPetEsq, velocidadeRotacaoPet * Time.deltaTime);

                Vector3 dirAlvoPetDir = alvoPet.transform.position - pontaPetDir.transform.position;
                dirAlvoPetDir = dirAlvoPetDir.normalized;
                petDir.transform.up = Vector3.Slerp(petDir.transform.up, dirAlvoPetDir, velocidadeRotacaoPet * Time.deltaTime);
                pontaPetDir.transform.up = Vector3.Slerp(pontaPetDir.transform.up, dirAlvoPetDir, velocidadeRotacaoPet * Time.deltaTime);
            }
        }
    }
    // Muda cor para vermelho
    public void EfeitoTomaDano()
    {
        if (particulasDano)
            particulasDano.Play();

        foreach (Material mat in materiais)
        {
            mat.color += Color.red;
        }
    }
    // Calcula dano, mudar cor e partícula de dano
    private void ReceberDano(GameObject inimigo)
    {

        if (inimigo.name == "Inimigo Pequeno(Clone)")
        {
            EfeitoTomaDano();
            float dano = inimigo.GetComponent<MovimentoInimigoPequeno>().danoContato;
            pontosVida -= dano;
        }
        if (inimigo.name == "Inimigo Piramide Esq" || inimigo.name == "Inimigo Piramide Esq(Clone)" || inimigo.name == "Inimigo Piramide Dir" || inimigo.name == "Inimigo Piramide Dir(Clone)")
        {
            EfeitoTomaDano();
            float dano = inimigo.GetComponent<MovimentoInimigoPiramide>().danoContato;
            pontosVida -= dano;
        }
        if (inimigo.CompareTag("BalaPiramide"))
        {
            EfeitoTomaDano();
            float dano = inimigo.GetComponent<BalaPersonagem>().danoProjetil;
            pontosVida -= dano;
            Destroy(inimigo);
        }
    }

    private void RetornaCorOriginal()
    {
        for (int i = 0; i < materiais.Length; i++)
        {
            materiais[i].color = Color.Lerp(materiais[i].color, coresOriginais[i], velocidadeCorMaterial * Time.deltaTime);
        }
    }
}
