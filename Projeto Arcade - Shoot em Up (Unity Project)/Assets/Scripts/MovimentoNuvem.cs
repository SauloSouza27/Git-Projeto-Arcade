using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoNuvem : MonoBehaviour
{
    public GameObject clouds;
    public float velocidadeRotacao = 1.0f;
    void Update()
    {
        clouds.transform.Rotate(0, 0, velocidadeRotacao * Time.deltaTime);
    }
}
