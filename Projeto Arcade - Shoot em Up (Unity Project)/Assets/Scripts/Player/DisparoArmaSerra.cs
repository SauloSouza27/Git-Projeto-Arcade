using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisparoArmaSerra : MonoBehaviour
{
    public GameObject projetilSerra, pontaSaidaSerra;
    // Cooldown
    [Range(0, 6)] public float cooldown = 6.0f;
    private float contadorCooldown;
    // Dano
    public int danoSerra = 3;
    public Image serraCDIM;

    private void Start()
    {
        serraCDIM.fillAmount = 0;
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
            serraCDIM.fillAmount = 1;
        }
        if(contadorCooldown != 0)
        {
            serraCDIM.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (serraCDIM.fillAmount <= 0)
            {
                serraCDIM.fillAmount = 0;
            }
        }
    }

    private void DiparaSerra()
    {
        Instantiate(projetilSerra, pontaSaidaSerra.transform.position, pontaSaidaSerra.transform.rotation);
    }
    

}
