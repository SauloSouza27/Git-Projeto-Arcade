using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMumia : MonoBehaviour
{
    private GameObject controladorGame;
    private float contadorCooldown, cooldownSpawnMumia;
    public float atrasaSpawn = 0.0f, minTempo = 6.0f, maxTempo = 15.0f;
    public int quantidadeParaSpawnar = 3;
    private int contador;
    public GameObject mumiaPrefab;
    public bool ativar = true;

    private void Awake()
    {
        controladorGame = GameObject.FindGameObjectWithTag("ControladorGame");
    }
    private void Start()
    {
        cooldownSpawnMumia = Random.Range(minTempo, maxTempo);
    }
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
            Instantiate(mumiaPrefab, transform.position, transform.rotation);
            cooldownSpawnMumia = Random.Range(minTempo, maxTempo);
            contadorCooldown = cooldownSpawnMumia;
            contador++;
        }
    }
}
