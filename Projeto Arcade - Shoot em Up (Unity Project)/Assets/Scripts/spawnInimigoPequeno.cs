using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoPequeno : MonoBehaviour
{
    private float contadorCooldown;
    public float cooldownSpawnInimigoPequeno, minTempo = 6.0f, maxTempo = 15.0f;
    public GameObject inimigoPequeno;
    public bool ativar = true;
    
    void Start()
    {
        cooldownSpawnInimigoPequeno = Random.Range(minTempo, maxTempo);
    }

    void Update()
    {
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
