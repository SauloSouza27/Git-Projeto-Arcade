using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBesouro : MonoBehaviour
{
    private GameObject controladorGame;
    private float contadorCooldown;
    private int nivelJogador, quantidadeSpawn = 0;
    public float cooldownSpawnBesouro;
    public GameObject inimigoBesouro;
    public bool ativar = true;

    private void Awake()
    {
        controladorGame = GameObject.Find("Controlador Game");
    }


    void Update()
    {
        if (Time.timeScale == 0) return;

        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0 && ativar == true && quantidadeSpawn <3)
        {
            Instantiate(inimigoBesouro, transform.position, transform.rotation);
            contadorCooldown = cooldownSpawnBesouro;
            quantidadeSpawn++;
        }
    }
}
