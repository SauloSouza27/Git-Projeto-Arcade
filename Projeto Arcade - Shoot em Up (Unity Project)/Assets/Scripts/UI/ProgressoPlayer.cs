using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressoPlayer : MonoBehaviour
{
    public static ProgressoPlayer instancia;
    public bool concluiuFase1 = false;


    void Start()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
