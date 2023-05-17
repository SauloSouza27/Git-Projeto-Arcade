using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXexplosion : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 1.6f);
    }
}
