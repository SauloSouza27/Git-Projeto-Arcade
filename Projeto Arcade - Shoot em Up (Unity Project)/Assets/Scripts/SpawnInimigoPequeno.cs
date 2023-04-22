using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoPequeno : MonoBehaviour
{
    private GameObject controladorGame;
    private float contadorCooldown;
    private int nivelJogador;
    public float cooldownSpawnInimigoPequeno, minTempo = 6.0f, maxTempo = 15.0f;
    public GameObject inimigoPequeno;
    public bool ativar = true;

    private void Awake()
    {
        controladorGame = GameObject.Find("Controlador Game");
        cooldownSpawnInimigoPequeno = Random.Range(minTempo, maxTempo);
        contadorCooldown = cooldownSpawnInimigoPequeno - minTempo + 1;
    }


    void Update()
    {
        if (Time.timeScale == 0) return;

        nivelJogador = controladorGame.GetComponent<ControladorGame>().nivel;

        if(nivelJogador >= 3)
        {
            minTempo = 4.0f;
            maxTempo = 8.0f;
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
