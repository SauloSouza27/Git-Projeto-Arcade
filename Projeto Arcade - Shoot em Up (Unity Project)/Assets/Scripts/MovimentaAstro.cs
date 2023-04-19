using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaPlaneta2 : MonoBehaviour
{
    public float velocidade = 1.0f;
    public GameObject astro, spawnPoint;
    void Start()
    {
        spawnPoint = GameObject.Find("Spawn Point Planeta 2");
    }

    void Update()
    {
        velocidade = spawnPoint.GetComponent<SpawnAstros>().velocidade;
        astro.transform.Translate(0, 0, -velocidade * Time.deltaTime, Space.World);
    }
}
