using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilidades : MonoBehaviour
{
    public static Vector3 maxPersonagem = new Vector3(26, 24, 0);
    public static Vector3 minPersonagem = new Vector3(-26, -5, 0);

    public static Vector3 TravaPosicao(Vector3 pos)
    {
        if (pos.x >= maxPersonagem.x)
        {
            pos = new Vector3(maxPersonagem.x, pos.y, pos.z);
        }
        else if (pos.x <= minPersonagem.x)
        {
            pos = new Vector3(minPersonagem.x, pos.y, pos.z);
        }

        if (pos.x >= maxPersonagem.y)
        {
            pos = new Vector3(pos.x, maxPersonagem.y, pos.z);
        }
        else if (pos.x <= minPersonagem.y)
        {
            pos = new Vector3(pos.x, minPersonagem.y, pos.z);
        }

        return pos;
    }
}
