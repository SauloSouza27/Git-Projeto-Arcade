using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoMorcegoDrone : MonoBehaviour
{
    public GameObject inimigoMorcegoDrone, spawnPoint, controladorGame;
    public bool ativar = true;

    private void Awake()
    {
        controladorGame = GameObject.Find("Controlador Game");
    }

    public void AtivaSpawnInimigoMorcegoDrone()
    {
        Instantiate(inimigoMorcegoDrone, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
