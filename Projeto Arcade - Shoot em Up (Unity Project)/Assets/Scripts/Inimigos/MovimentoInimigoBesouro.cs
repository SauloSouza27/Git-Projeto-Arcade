using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoBesouro : MonoBehaviour
{
    public GameObject besouro, bosta;
    // movimento
    public float velocidadeMovimento = 2.0f, velocidadeRotacao = 2.5f, velocidadeRotacaoBosta = 2.0f;
    private bool mudaDirecao = true, primeiroGiro = true;
    private float contadorCooldown;
    [Range(0.0f, 8.0f)] public float cooldownMudaDirecao = 2.0f;

    private void Start()
    {
        cooldownMudaDirecao /= 2;
        contadorCooldown = cooldownMudaDirecao;
    }
    void Update()
    {
        Movimento();
    }
     private void Movimento()
    {
        //  rotacao bosta
        bosta.transform.Rotate(velocidadeRotacaoBosta * Time.deltaTime, 0, 0, Space.Self);
        // direcao
        besouro.transform.Translate(0, velocidadeMovimento * Time.deltaTime, 0, Space.Self);
        // rotacao
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);
        
        if (mudaDirecao)
        {
            besouro.transform.Rotate(0, 0, velocidadeRotacao * Time.deltaTime, Space.Self);
        }
        if (contadorCooldown == 0 && mudaDirecao == true)
        {
            mudaDirecao = false;
            if (primeiroGiro)
            {
                primeiroGiro = false;
                cooldownMudaDirecao *= 2;
            }
            contadorCooldown = cooldownMudaDirecao;
        }
        if (!mudaDirecao)
        {
            besouro.transform.Rotate(0, 0, - velocidadeRotacao * Time.deltaTime, Space.Self);
        }
        if (contadorCooldown == 0 && mudaDirecao == false)
        {
            mudaDirecao = true;
            contadorCooldown = cooldownMudaDirecao;
        }
    }
}
