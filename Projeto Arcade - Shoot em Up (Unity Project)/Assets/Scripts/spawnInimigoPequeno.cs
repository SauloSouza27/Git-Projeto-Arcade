using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoPequeno : MonoBehaviour
{
    private float contadorCooldown;
    public float cooldownSpawnInimigoPequeno;
    public GameObject inimigoPequeno;
    public bool ativar = true;
    
    void Start()
    {
        cooldownSpawnInimigoPequeno = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        CalculaCooldown();
        if (contadorCooldown == 0 && ativar == true)
        {
            Instantiate(inimigoPequeno, transform.position, transform.rotation);
            cooldownSpawnInimigoPequeno = Random.Range(3, 8);
            contadorCooldown = cooldownSpawnInimigoPequeno;
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
}
