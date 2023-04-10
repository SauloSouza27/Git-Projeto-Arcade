using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoPiramide : MonoBehaviour
{
    public GameObject inimigoPiramide, spawnPoint, controladorGame;
    private int nivelJogador;
    public bool ativar = true;

    private void Awake()
    {
        controladorGame = GameObject.Find("Controlador Game");
    }

    void Update()
    {
        nivelJogador = controladorGame.GetComponent<ControladorGame>().nivel;

        if (ativar && nivelJogador == 3)
        {
            for (int i = 0; i < 1; i++)
            {
                AtivaSpawnInimigoPiramide();
                ativar = false;
            }
        }
        
    }

    public void AtivaSpawnInimigoPiramide()
    {
        Instantiate(inimigoPiramide, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
