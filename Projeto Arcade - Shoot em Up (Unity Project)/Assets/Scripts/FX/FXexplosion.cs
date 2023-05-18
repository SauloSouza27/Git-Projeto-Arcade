using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXexplosion : MonoBehaviour
{
    public float tempoParaDestruir = 1.6f;
    void Update()
    {
        Destroy(gameObject, tempoParaDestruir);
    }
}
