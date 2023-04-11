using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoArma : MonoBehaviour
{
    // Disparo arma
    public GameObject pontaArma, bala;
    // Tiro
    [Range(0, 1)] public float cooldown = 1.0f;
    private float contadorCooldown;
    // Alterar tiro altomatico
    public bool tiroAutomatico = true;
    // Dano arma
    public float danoArmaPrincipal = 10.0f;


    private void Awake()
    {
        contadorCooldown = cooldown;
    }
  
    void Update()
    {
        if (Time.timeScale == 0) return;

        // Cooldown e controle tiro
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if ((Input.GetButton("Fire1") == true || tiroAutomatico == true) && contadorCooldown == 0)
        {
            Tiro();
            contadorCooldown = cooldown;
        }
    }

    // Tiro
    private void Tiro()
    {
        Instantiate(bala, pontaArma.transform.position, pontaArma.transform.rotation);
    }
}
