using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    private bool isCollided = false;
    private float respawnTime = 10f;
    private float respawnTimer = 0f;
    public GameObject escudo;
    public float velocidadeRotacao = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotacao();
        if (isCollided)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= respawnTime)
            {
                // Re-enable the object
                escudo.GetComponent<MeshRenderer>().enabled = true;
                escudo.GetComponent<Collider>().enabled = true;
                isCollided = false;
                respawnTimer = 0f;
            }
        }

    }
    private void OnCollisionEnter(Collision colisor)
    {

        if (colisor.gameObject.CompareTag("Inimigo") || colisor.gameObject.CompareTag("BalaPiramide") || colisor.gameObject.CompareTag("BalaBossPiramide") || colisor.gameObject.CompareTag("BalaAnubis"))
        {
            escudo.GetComponent<MeshRenderer>().enabled = false;
            escudo.GetComponent<Collider>().enabled = false;
            isCollided = true;
        }
    }
    private void Rotacao()
    {
        transform.Rotate(Vector3.up, velocidadeRotacao * Time.deltaTime);
        transform.Rotate(Vector3.right, velocidadeRotacao * Time.deltaTime);
    }
}
