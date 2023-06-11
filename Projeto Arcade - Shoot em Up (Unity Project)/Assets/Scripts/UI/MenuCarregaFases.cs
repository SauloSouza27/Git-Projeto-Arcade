using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCarregaFases : MonoBehaviour
{
    public Button fase2;

    private void OnEnable()
    {
        if (!ProgressoPlayer.instancia.concluiuFase1)
        {
            fase2.interactable = false;
        }

        if (ProgressoPlayer.instancia.concluiuFase1)
        {
            fase2.interactable = true;
        }
    }
}
