using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaPersonagem : MonoBehaviour
{
    public float velocidade = 1.0f, duracaoBala = 5.0f;
    public GameObject projetil;

    void DestroiBala()
    {
        Destroy(projetil, duracaoBala * Time.deltaTime);
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        // Movimento bala
        projetil.transform.Translate(0, velocidade * Time.deltaTime, 0, Space.Self);
        DestroiBala();
    }
    
}
