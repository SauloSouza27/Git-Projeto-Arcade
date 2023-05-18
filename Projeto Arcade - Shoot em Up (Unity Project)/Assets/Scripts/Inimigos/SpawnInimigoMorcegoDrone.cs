using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoMorcegoDrone : MonoBehaviour
{
    private float contadorCooldown;
    public float atrasaSpawn = 0.0f, cooldownSpawnMorcegoDrone = 4.0f;
    public int quantidadeParaSpawnar = 3;
    private int contador;
    public GameObject morcegoDronePrefab;
    public bool ativar = true, mudaDirecao = false;
    // Controle Movimento
    public float velocidade = 2.0f, rotacao = 1.0f, atrasoRotacao = 1.0f;
    public float anguloZ = 0.0f;

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (atrasaSpawn > 0)
        {
            atrasaSpawn -= Time.deltaTime;
            return;
        }

        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0 && ativar == true && contador < quantidadeParaSpawnar)
        {
            GameObject instancia = Instantiate(morcegoDronePrefab, transform.position, transform.rotation);
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
