using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAstros : MonoBehaviour
{
    public GameObject astro;
    public float velocidade = 2.0f;
    private float contadorCooldown;
    public float cooldownSpawnAstro = 1.0f;
    public bool ativar = true;

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
    void Start()
    {
        
    }

    void Update()
    {

        CalculaCooldown();
        if(contadorCooldown == 0 && ativar == true)
        {
            Instantiate(astro, transform.position, astro.transform.rotation);
            contadorCooldown = cooldownSpawnAstro;
        }
    }
}
