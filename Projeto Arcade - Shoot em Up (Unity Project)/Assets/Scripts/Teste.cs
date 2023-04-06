using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public float steps;
    public Vector3 p1, p2;
    public Vector3 delta, point;

    void LinhaEntreDoisPontos(Vector3 p1, Vector3 p2)
    {
        point = p1;
        Vector3 dir = p2 - p1;
        steps = Mathf.Max(dir.x, dir.y, dir.z);
        Debug.Log("Steps: " + steps);
        if (steps != 0)
        {
            delta = dir / steps;
            Debug.Log("Delta: " + delta);
            for (int k = 1; k <= steps; k++)
            {
                Debug.Log("Ponto pintado: " + point);
                point += delta;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        p1.Set(1, 1, 0);
        Debug.Log("Vetor p1: " + p1);
        p2.Set(6, 3, 0);
        Debug.Log("Vetor p2: " + p2);
        LinhaEntreDoisPontos(p1, p2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
