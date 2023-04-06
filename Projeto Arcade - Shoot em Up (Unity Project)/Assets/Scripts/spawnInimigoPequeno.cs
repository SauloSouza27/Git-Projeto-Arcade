using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnInimigoPequeno : MonoBehaviour
{
    private float contadorCooldown;
    public float cooldownSpawnInimigoPequeno = 3.0f;
    public GameObject inimigoPequeno;
    public bool ativar = true;
    
    void Start()
    {

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
