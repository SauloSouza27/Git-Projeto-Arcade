using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBesouro : MonoBehaviour
{
    private float contadorCooldown;
    public int quantidadeSpawn = 0;
    public float atrasaSpawn, cooldownSpawnBesouro;
    private int count;
    public GameObject inimigoBesouroPrefab;
    public bool ativar = true;
    // controles do movimento da instacia
    public float velocidadeMov = 4.0f, anguloRot = 25.0f;
    public float tempoMudaDirecao = 2.5f;



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
        if (contadorCooldown == 0 && ativar == true && count < quantidadeSpawn)
        {
            GameObject instancia = Instantiate(inimigoBesouroPrefab, transform.position, transform.rotation);
            MovimentoInimigoBesouro status = instancia.GetComponent<MovimentoInimigoBesouro>();
            status.velocidadeMovimento = velocidadeMov;
            status.anguloRotacao = anguloRot;
            status.cooldownMudaDirecao = tempoMudaDirecao;
            contadorCooldown = cooldownSpawnBesouro;
            count++;
        }
    }
}
