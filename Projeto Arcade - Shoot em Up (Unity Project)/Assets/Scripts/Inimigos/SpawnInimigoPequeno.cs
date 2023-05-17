using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoPequeno : MonoBehaviour
{
    private GameObject controladorGame;
    private float contadorCooldown;
    private int nivelJogador;
    public float cooldownSpawnInimigoPequeno, minTempo = 6.0f, maxTempo = 15.0f;
    private float atrasaSpawn;
    public GameObject inimigoPequeno;
    public bool ativar = true;

    private void Awake()
    {
        controladorGame = GameObject.Find("Controlador Game");
        cooldownSpawnInimigoPequeno = Random.Range(minTempo, maxTempo);
        contadorCooldown = cooldownSpawnInimigoPequeno - minTempo + 1;
    }

    private void OnEnable()
    {
        nivelJogador = controladorGame.GetComponent<ControladorGame>().nivel;

        if (controladorGame.GetComponent<ControladorGame>().nivel == 2)
        {
            atrasaSpawn = 4.0f;
        }
        if (controladorGame.GetComponent<ControladorGame>().nivel == 5)
        {
            atrasaSpawn = 6.0f;
        }
    }

    void Update()
    {

        if (Time.timeScale == 0) return;

        if (atrasaSpawn > 0)
        {
            atrasaSpawn -= Time.deltaTime;
            return;
        }

        nivelJogador = controladorGame.GetComponent<ControladorGame>().nivel;

        if (nivelJogador >= 3)
        {
            minTempo = 4.0f;
            maxTempo = 10.0f;
        }

        if (nivelJogador == 7)
        {
            minTempo = 7.0f;
            maxTempo = 9.0f;
        }

        if (nivelJogador >= 8)
        {
            minTempo = 3.0f;
            maxTempo = 9.0f;
        }

        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0 && ativar == true)
        {
            Instantiate(inimigoPequeno, transform.position, transform.rotation);
            cooldownSpawnInimigoPequeno = Random.Range(minTempo, maxTempo);
            contadorCooldown = cooldownSpawnInimigoPequeno;
        }
    }
}
