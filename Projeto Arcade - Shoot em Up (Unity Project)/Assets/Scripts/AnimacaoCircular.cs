using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoCircular : MonoBehaviour
{
    public Transform centro;
    public float raio;
    private float tempo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // x = c + r * cos(t)
        // y = c + r * sen(t)
        tempo += Time.deltaTime; 
        Debug.Log("Fixed Time: "+Time.fixedTime);
        Debug.Log("Delta Time: "+Time.deltaTime);
        float x = centro.position.x + raio * Mathf.Cos(tempo);
        float z = centro.position.z + raio * Mathf.Sin(tempo);
        transform.position = new Vector3(x, 0, z);
    }
}
