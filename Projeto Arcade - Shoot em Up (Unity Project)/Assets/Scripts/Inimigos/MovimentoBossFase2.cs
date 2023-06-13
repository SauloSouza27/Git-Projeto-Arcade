using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBossFase2 : MonoBehaviour
{
    //tiro olho
    public GameObject bala, olhoEsq, olhoDir;
    [Range(0, 5)] public float cooldown = 0.3f, tempoDisparo = 3.0f;
    public int numeroDisparos = 10;

    private float contadorCooldown, contadorDisparos;
    private bool ativaArma = false;

    private void Start()
    {
        ativaArma = false;
        StartCoroutine(IntervaloDisparo(2.0f));
    }
    private void Update()
    {
        // disparo armas boss
        if (ativaArma)
        {
            // Cooldown e controle tiro
            Utilidades.CalculaCooldown(contadorCooldown);
            contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
            if (contadorCooldown == 0)
            {
                Tiro();
                contadorCooldown = cooldown;
                contadorDisparos++;
                if (contadorDisparos < numeroDisparos) return;
                else
                {
                    contadorDisparos = 0;
                    ativaArma = false;
                    StartCoroutine(IntervaloDisparo(tempoDisparo));
                }
            }
        }
    }
    private void Tiro()
    {
        Instantiate(bala, olhoEsq.transform.position, olhoEsq.transform.rotation);
        Instantiate(bala, olhoDir.transform.position, olhoDir.transform.rotation);
    }
    private IEnumerator IntervaloDisparo(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        ativaArma = true;
    }
}
