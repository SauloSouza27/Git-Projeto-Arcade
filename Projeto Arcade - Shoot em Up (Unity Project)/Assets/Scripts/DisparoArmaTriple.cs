using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoArmaTriple : MonoBehaviour
{
    // Disparo arma
    public GameObject pontaArmaEsq, pontaArmaDir, pontaArmaCentro, bala;
    // Tiro
    [Range(0, 1)] public float cooldown = 0.9f;
    private float contadorCooldown;
    // Alterar tiro altomatico
    public bool tiroAutomatico = true;
    // som tiro
    public AudioSource somTiro;


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
        Instantiate(bala, pontaArmaEsq.transform.position, pontaArmaEsq.transform.rotation);
        Instantiate(bala, pontaArmaDir.transform.position, pontaArmaDir.transform.rotation);
        Instantiate(bala, pontaArmaCentro.transform.position, pontaArmaCentro.transform.rotation);
        somTiro.Play();
    }
}
