using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Escudo : MonoBehaviour
{
    private bool isCollided = false;
    private float respawnTime = 10f;
    private float respawnTimer = 0f;
    public GameObject escudo;
    public Image escudoCDIM;
    public float velocidadeRotacao = 10f;

    void Start()
    {
        escudoCDIM.fillAmount = 0;
    }


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
            escudoCDIM.fillAmount -= 1 / respawnTime * Time.deltaTime;
            if (escudoCDIM.fillAmount <= 0)
            {
                escudoCDIM.fillAmount = 0;
            }
        }

    }
    private void OnCollisionEnter(Collision colisor)
    {

        if (colisor.gameObject.CompareTag("Inimigo") || colisor.gameObject.CompareTag("BalaPiramide") || colisor.gameObject.CompareTag("BalaBossPiramide") || colisor.gameObject.CompareTag("BalaAnubis")
             || colisor.gameObject.CompareTag("BalaBossFase2"))
        {
            escudo.GetComponent<MeshRenderer>().enabled = false;
            escudo.GetComponent<Collider>().enabled = false;
            isCollided = true;
            escudoCDIM.fillAmount = 1;
        }
    }
    private void Rotacao()
    {
        escudo.transform.Rotate(Vector3.up, velocidadeRotacao * Time.deltaTime);
        escudo.transform.Rotate(Vector3.right, velocidadeRotacao * Time.deltaTime);
    }
}
