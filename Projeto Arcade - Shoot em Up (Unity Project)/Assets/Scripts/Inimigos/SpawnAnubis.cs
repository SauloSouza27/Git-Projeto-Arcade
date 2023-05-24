using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnubis : MonoBehaviour
{
    private float contadorCooldown;
    public float atrasaSpawn = 0.0f, cooldownSpawnAnubis = 2.0f, velocidadeMovimento = 4.0f;
    public int quantidadeParaSpawnar = 2, pontosVida = 20;
    public float atrasaDisparos = 0.0f, velocidadeProjetil = 40.0f;
    private int contador;
    public GameObject anubisPrefab;
    public bool ativar = true;

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
            GameObject instancia = Instantiate(anubisPrefab, transform.position, transform.rotation);
            MovimentoAnubis status = instancia.GetComponent<MovimentoAnubis>();
            status.atrasaDisparos = atrasaDisparos;
            status.velocidadeProjetil = velocidadeProjetil;
            status.velocidadeMovimento = velocidadeMovimento;
            status.pontosVida = pontosVida;
            if (instancia != null) return;
            contadorCooldown = cooldownSpawnAnubis;
            contador++;
        }
    }
}
