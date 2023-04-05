using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class controlaPersonagem : MonoBehaviour
{
    // Canhoes
    public GameObject pontaPetEsq, pontaPetDir;
    [Range(0,1)] public float bonusAttackSpeed = 1.0f;
    // Controle movimento personagem
    private float x, y;
    public float velocidadeMovimento = 1.0f;
    public GameObject personagem, armaPrincipal, pet, alvoPet;
    // Pontos de vida
    public float pontosVida = 100.0f;
    public float danoContato = 20.0f;

    void Start()
    {
        //Lock cursor
        //Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        ControleMovimentoPersonagem();

        ControleArmaPrincipal();

        MovimentoPets();
    }

    // Controle movimento personagem
    public void ControleMovimentoPersonagem()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (x != 0 || y != 0)
        {
            personagem.transform.Translate(x * Time.deltaTime * velocidadeMovimento, y * Time.deltaTime * velocidadeMovimento, 0, Space.World);
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

    // controle movimento do pet
    private void MovimentoPets()
    {
        alvoPet = GameObject.FindWithTag("Inimigo");
        Vector3 dirAlvoPet = alvoPet.transform.position - pet.transform.position;
        if (dirAlvoPet.magnitude <= 15.0f)
        {
            dirAlvoPet = dirAlvoPet.normalized;
            pet.transform.rotation = Quaternion.LookRotation(pet.transform.forward, dirAlvoPet);
            Vector3 dirAlvoPetEsq = alvoPet.transform.position - pontaPetEsq.transform.position;
            dirAlvoPetEsq = dirAlvoPetEsq.normalized;
            pontaPetEsq.transform.rotation = Quaternion.LookRotation(pontaPetEsq.transform.forward, dirAlvoPetEsq);
            Vector3 dirAlvoPetDir = alvoPet.transform.position - pontaPetDir.transform.position;
            dirAlvoPetDir = dirAlvoPetDir.normalized;
            pontaPetDir.transform.rotation = Quaternion.LookRotation(pontaPetDir.transform.forward, dirAlvoPetDir);
        }
        if (dirAlvoPet.magnitude > 15.0f)
        {
            pet.transform.rotation = new Quaternion(0, 0, 0, 0);
            pontaPetEsq.transform.rotation = new Quaternion(0, 0, 0, 0);
            pontaPetDir.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    // Dano Inimigos
    private void OnCollisionEnter(Collision colisor)
    {
        GameObject inimigo = colisor.gameObject;
        if (inimigo.name == "Inimigo Pequeno(Clone)")
        {
            float dano = inimigo.GetComponent<movimentoInimigoPequeno>().danoContato;
            pontosVida -= dano;
            Debug.Log(dano);
        }
        if (inimigo.name == "Inimigo Grande(Clone)" || inimigo.name == "Inimigo Grande" || inimigo.name == "Inimigo Grande (1)")
        {
            float dano = inimigo.GetComponent<movimentoInimigoGrande>().danoContato;
            pontosVida -= dano;
            Debug.Log("Dano: " + dano);
        }
        if (pontosVida <= 0)
        {
            Destroy(personagem);
        }
    }
}
