using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triangulo : MonoBehaviour
{
    Mesh malha;
    Vector3[] vertices;
    int[] triangulos;
    void Start()
    {
        malha = GetComponent<MeshFilter>().mesh;

        Vector3 p0 = new Vector3(0, 0, 0);
        Vector3 p1 = new Vector3(0, 0, 1);
        Vector3 p2 = new Vector3(1, 0, 0);

        vertices = new Vector3[] {p0, p1, p2};

        triangulos = new int[] { 0, 1, 2 };

        malha.Clear();
        malha.vertices = vertices;
        malha.triangles = triangulos;

    }

    void Update()
    {
        
    }
}
