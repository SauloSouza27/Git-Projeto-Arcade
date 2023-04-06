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

    // Update is called once per frame
    void Update()
    {
        // Cooldown e controle tiro
        CalculaCooldown();
        if ((Input.GetButton("Fire1") == true || tiroAutomatico == true) && contadorCooldown == 0)
        {
            Tiro();
            contadorCooldown = cooldown;
        }
    }
    private void CalculaCooldown()
    {
        if (contadorCooldown > 0)
        {
            contadorCooldown -= Time.deltaTime;
        }
        if (contadorCooldown < 0)
        {
            contadorCooldown = 0;
        }

    }

    // Tiro
    public void Tiro()
    {
        Instantiate(bala, pontaArma.transform.position, pontaArma.transform.rotation);
    }
    void Start()
    {

    }
}
