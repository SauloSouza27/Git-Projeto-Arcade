using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoMorcegoDrone : MonoBehaviour
{
    private GameObject controladorGame;
    private float contadorCooldown;
    private int nivelJogador;
    public float cooldownSpawnMorcegoDrone = 4.0f;
    public GameObject morcegoDrone;
    public bool ativar = true;
    // Controle Movimento
    public float velocidade;

    private void Awake()
    {
        controladorGame = GameObject.Find("Controlador Game");
    }
    void Update()
    {
        if (Time.timeScale == 0) return;

        nivelJogador = controladorGame.GetComponent<ControladorGame>().nivel;

        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (contadorCooldown == 0 && ativar == true)
        {
            GameObject instancia = Instantiate(morcegoDrone, transform.position, transform.rotation);
            MovimentoInimigoMorcegoDrone status = instancia.GetComponent<MovimentoInimigoMorcegoDrone>();
            status.isAutomatic = true;
            contadorCooldown = cooldownSpawnMorcegoDrone;
        }
    }
}
