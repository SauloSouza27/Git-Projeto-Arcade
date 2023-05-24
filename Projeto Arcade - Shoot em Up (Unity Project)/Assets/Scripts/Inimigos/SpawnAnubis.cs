using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnubis : MonoBehaviour
{
    private float contadorCooldown;
    public float atrasaSpawn = 0.0f, cooldownSpawnAnubis = 2.0f, velocidadeMovimento = 6.0f;
    public int quantidadeParaSpawnar = 3;
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
            MovimentoInimigoEspinhoso status = instancia.GetComponent<MovimentoInimigoEspinhoso>();
            status.velocidadeMovimento = velocidadeMovimento;
            contadorCooldown = cooldownSpawnAnubis;
            contador++;
        }
    }
}
