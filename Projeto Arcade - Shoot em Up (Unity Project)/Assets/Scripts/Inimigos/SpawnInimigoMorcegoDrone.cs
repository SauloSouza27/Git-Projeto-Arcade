using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoMorcegoDrone : MonoBehaviour
{
    private GameObject controladorGame;
    private float contadorCooldown;
    private int nivelJogador;
    public float cooldownSpawnMorcegoDrone = 4.0f;
    public int quantidadeParaSpawnar = 3;
    private int contador;
    public GameObject morcegoDrone;
    public bool ativar = true, mudaDirecao = false;
    // Controle Movimento
    public float velocidade = 2.0f, rotacao = 1.0f, atrasoRotacao = 1.0f;
    public float anguloZ = 0.0f;

    private void Awake()
    {
        controladorGame = GameObject.Find("Controlador Game");
    }
    void Update()
    {
        if (Time.timeScale == 0) return;

        nivelJogador = controladorGame.GetComponent<ControladorGame>().nivel;
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0 && ativar == true && contador < quantidadeParaSpawnar)
        {
            GameObject instancia = Instantiate(morcegoDrone, transform.position, transform.rotation);
            MovimentoInimigoMorcegoDrone status = instancia.GetComponent<MovimentoInimigoMorcegoDrone>();
            status.isAutomatic = true;
            status.velocidadeMovimento = velocidade;
            status.velocidadeRotacao = rotacao;
            status.atrasoRotacao = atrasoRotacao;
            status.anguloZ = anguloZ;
            status.turn = mudaDirecao;
            contadorCooldown = cooldownSpawnMorcegoDrone;
            contador++;
        }
    }
}
