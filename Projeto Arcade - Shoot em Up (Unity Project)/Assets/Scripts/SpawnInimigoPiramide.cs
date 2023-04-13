using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigoPiramide : MonoBehaviour
{
    public GameObject inimigoPiramide, spawnPoint;
    public bool ativar = true;

    public void AtivaSpawnInimigoPiramide()
    {
        Instantiate(inimigoPiramide, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
