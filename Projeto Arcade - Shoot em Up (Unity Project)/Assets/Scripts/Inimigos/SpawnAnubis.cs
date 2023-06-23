using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnubis : MonoBehaviour
{
    private GameObject controladorGame, buttonNivel;
    private float contadorCooldown;
    public float atrasaSpawn = 0.0f, cooldownSpawnAnubis = 2.0f, velocidadeMovimento = 4.0f;
    public int quantidadeParaSpawnar = 2, pontosVida = 20, xpInimigo = 100, numeroDisparos = 3;
    public float minY = 25.0f, cooldownTiro = 0.3f, tempoDisparo = 2.0f, atrasaDisparos = 0.0f, velocidadeProjetil = 40.0f;
    private int contador;
    public GameObject anubisPrefab;
    public bool ativar = true;
    private GameObject instancia;

    private void Awake()
    {
        controladorGame = GameObject.FindGameObjectWithTag("ControladorGame");
        buttonNivel = controladorGame.GetComponent<ControladorGame>().buttonSubirNivel;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (buttonNivel.activeSelf)
        {
            return;
        }

        if (atrasaSpawn > 0)
        {
            atrasaSpawn -= Time.deltaTime;
            return;
        }
        if (instancia != null)
        {
            return;
        }

        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0 && ativar == true && contador < quantidadeParaSpawnar)
        {
            instancia = Instantiate(anubisPrefab, transform.position, transform.rotation);
            MovimentoAnubis status = instancia.GetComponent<MovimentoAnubis>();
            status.atrasaDisparos = atrasaDisparos;
            status.velocidadeProjetil = velocidadeProjetil;
            status.velocidadeMovimento = velocidadeMovimento;
            status.pontosVida = pontosVida;
            status.xpInimigo = xpInimigo;
            status.cooldown = cooldownTiro;
            status.tempoDisparo = tempoDisparo;
            status.numeroDisparos = numeroDisparos;
            status.minY = minY;
            contadorCooldown = cooldownSpawnAnubis;
            contador++;
        }
    }
}
