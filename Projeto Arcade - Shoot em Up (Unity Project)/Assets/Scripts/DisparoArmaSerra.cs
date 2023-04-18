using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoArmaSerra : MonoBehaviour
{
    public GameObject projetilSerra, pontaSaidaSerra;
    // Cooldown
    [Range(0, 6)] public float cooldown = 6.0f;
    private float contadorCooldown;
    // Dano
    public int danoSerra = 1, danoSerraDPS = 1;

    private void Awake()
    {
        contadorCooldown = 0;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        // Cooldown e controle tiro
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        if (Input.GetKeyDown(KeyCode.Space) && contadorCooldown == 0)
        {
            DiparaSerra();
            contadorCooldown = cooldown;
        }
    }

    private void DiparaSerra()
    {
        Instantiate(projetilSerra, pontaSaidaSerra.transform.position, pontaSaidaSerra.transform.rotation);
    }
    

}
