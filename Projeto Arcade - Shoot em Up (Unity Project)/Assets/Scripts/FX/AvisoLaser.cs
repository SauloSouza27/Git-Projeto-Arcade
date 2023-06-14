using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvisoLaser : MonoBehaviour
{
    private GameObject bossFase2;

    public float tempoPisca = 1.0f;
    public float intensidadeCor = 1.1f, velocidadeMudaCor = 1.0f;

    private bool aumentaBrilho = true;
    private float count;
    private MeshRenderer render;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        // contador pisca cor
        count = tempoPisca;
    }

    private void Update()
    {
        AlternaCorEmission();
    }

    private void AlternaCorEmission()
    {

        if (aumentaBrilho)
        {
            if (count > 0)
            {
                AumentaIntensidadeEmissao();
                count -= Time.deltaTime;
            }
            if (count <= 0)
            {
                count = tempoPisca;
                aumentaBrilho = false;
            }
        }
        if (!aumentaBrilho)
        {
            if (count > 0)
            {
                DiminuiIntensidadeEmissao();
                count -= Time.deltaTime;
            }
            if (count <= 0)
            {
                count = tempoPisca;
                aumentaBrilho = true;
            }
        }
    }

    private void AumentaIntensidadeEmissao()
    {
        Color brilhoForte = new Color(0.2358f, 0.0f, 0.0f) * intensidadeCor;
        render.material.SetColor("_Color", brilhoForte);
        intensidadeCor += velocidadeMudaCor * Time.deltaTime;
    }
    private void DiminuiIntensidadeEmissao()
    {
        Color brilhoFraco = new Color(0.2358f, 0.0f, 0.0f) * intensidadeCor;
        render.material.SetColor("_Color", brilhoFraco);
        intensidadeCor -= velocidadeMudaCor * Time.deltaTime;
    }
}
