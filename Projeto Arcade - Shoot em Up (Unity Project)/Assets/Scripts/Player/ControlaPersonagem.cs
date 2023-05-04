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
    public GameObject personagem, armaPrincipal, armaPets, petEsq, petDir, pontaPetEsq, pontaPetDir, armaOrbeGiratorio, armaSerra;
    private GameObject alvoPet;
    public float velocidadeRotacaoPet = 2.0f, distanciaMinPetAtirar = 20.0f, velocidadeRotacaoOrbeGiratorio = 15.0f;
    // Pontos de vida
    public int pontosVida = 3;
    public int danoContato = 5;
    // Dano arma
    public int danoArmaPrincipal = 1;
    // Particulas
    public ParticleSystem particulasDano;
    // Materias player
    private Material[] materiais;
    private Color[] coresOriginais;
    // sons player
    public AudioSource tomaDano;

    void Start()
    {
        // Cursor Config
        // Cursor.lockState = CursorLockMode.Confined;

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

        armaPrincipal = GameObject.FindWithTag("ArmaPrincipal");
        ControleArmaPrincipal(armaPrincipal);

        if(ControladorGame.instancia == null)
        {
            return;
        }

        if (ControladorGame.instancia.armaPetAtivada)
        {
            ControleArmaPets();
        }

        if (ControladorGame.instancia.armaOrbeGiratorioAtivada)
        {
            ControleOrbeGiratorio();
        }

        //mudança da cor do material
        RetornaCorOriginal();
    }

    // Dano Inimigos
    private void OnCollisionEnter(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("Inimigo") || colisor.gameObject.CompareTag("BalaPiramide") || colisor.gameObject.CompareTag("BalaBossPiramide"))
        {
            if (pontosVida > 0)
            {
                ReceberDano();
                if (colisor.gameObject.CompareTag("BalaPiramide"))
                {
                    Destroy(colisor.gameObject);
                }
            }
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
    private void ControleMovimentoPersonagem()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (x != 0 || y != 0)
        {
            personagem.transform.Translate(x * Time.deltaTime * velocidadeMovimento, y * Time.deltaTime * velocidadeMovimento, 0, Space.World);
            personagem.transform.position = Utilidades.TravaPosicao(personagem.transform.position);
        }
    }

    // Controle rotacao arma principal
    private void ControleArmaPrincipal(GameObject armaPrincipalAtiva)
    {
        Vector3 position = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 30));
        Vector3 dirMouse = mousePos - armaPrincipalAtiva.transform.position;
        dirMouse = dirMouse.normalized;
        armaPrincipalAtiva.transform.rotation = Quaternion.LookRotation(armaPrincipalAtiva.transform.forward, dirMouse);
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
    private void ControleArmaPets()
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
        if (alvoPet == null)
        {
            petEsq.transform.rotation = Quaternion.Slerp(petEsq.transform.rotation, new Quaternion(0, 0, 0, 1), velocidadeRotacaoPet * Time.deltaTime);
            petDir.transform.rotation = Quaternion.Slerp(petDir.transform.rotation, new Quaternion(0, 0, 0, 1), velocidadeRotacaoPet * Time.deltaTime);

            pontaPetEsq.transform.rotation = Quaternion.Slerp(pontaPetEsq.transform.rotation, new Quaternion(0, 0, 0, 1), velocidadeRotacaoPet * Time.deltaTime);
            pontaPetDir.transform.rotation = Quaternion.Slerp(pontaPetDir.transform.rotation, new Quaternion(0, 0, 0, 1), velocidadeRotacaoPet * Time.deltaTime);
        }
    }

    // Arma orbe giratorio
    private void ControleOrbeGiratorio()
    {
        armaOrbeGiratorio.transform.RotateAround(transform.position, transform.forward, velocidadeRotacaoOrbeGiratorio * Time.deltaTime);
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
    

    // Calcula dano, mudar cor e particula de dano
    private void ReceberDano()
    {
        EfeitoTomaDano();
        tomaDano.Play();
        int dano = 1;
        pontosVida -= dano;
    }

    private void RetornaCorOriginal()
    {
        for (int i = 0; i < materiais.Length; i++)
        {
            materiais[i].color = Color.Lerp(materiais[i].color, coresOriginais[i], velocidadeCorMaterial * Time.deltaTime);
        }
    }
}
