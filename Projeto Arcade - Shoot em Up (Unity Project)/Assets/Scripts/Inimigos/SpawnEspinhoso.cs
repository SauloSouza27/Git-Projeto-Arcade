using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEspinhoso : MonoBehaviour
{
    private float contadorCooldown;
    public float atrasaSpawn = 0.0f, cooldownSpawnEspinhoso = 2.0f, velocidadeMovimento = 6.0f;
    public int quantidadeParaSpawnar = 3, xpInimigo = 10;
    private int contador;
    public GameObject espinhosoPrefab;
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
            GameObject instancia = Instantiate(espinhosoPrefab, transform.position, transform.rotation);
            MovimentoInimigoEspinhoso status = instancia.GetComponent<MovimentoInimigoEspinhoso>();
            status.velocidadeMovimento = velocidadeMovimento;
            status.xpInimigo = xpInimigo;
            contadorCooldown = cooldownSpawnEspinhoso;
            contador++;
        }
    }
}
