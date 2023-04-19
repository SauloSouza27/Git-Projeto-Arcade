using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoSubirNivel : MonoBehaviour
{
    public float amplitude = 0.5f, velocidadePisca = 3.0f;
    Vector3 scale;
    void Update()
    {
        for (int i = 0; i < 3; ++i) scale[i] = amplitude * Mathf.Sin(velocidadePisca * Time.time) + 1;
        transform.localScale = scale;
    }

}
