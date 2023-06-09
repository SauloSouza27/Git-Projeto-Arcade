using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXexplosion : MonoBehaviour
{
    public bool espinhoso = false;
    public float tempoParaDestruir = 1.6f;
    void Update()
    {
        Destroy(gameObject, tempoParaDestruir);
    }
    private void OnCollisionEnter(Collision colidido)
    {

        if (colidido.gameObject.CompareTag("Escudo") && espinhoso)
        {
            Destroy(gameObject);
        }
    }
}
