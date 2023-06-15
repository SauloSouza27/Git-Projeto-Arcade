using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoTornado : MonoBehaviour
{
    public float velocidadeMovimento = 4.0f, anguloRotacao = 25.0f;
    private bool mudaDirecao = true, primeiroGiro = true;
    private float contadorCooldown;
    public float cooldownMudaDirecao = 2.0f;
    public GameObject tornado;

    void Start()
    {
        cooldownMudaDirecao /= 2;
        contadorCooldown = cooldownMudaDirecao;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        Movimento();

        Utilidades.DestroyOutOfScreen(tornado.transform.position, gameObject);
    }
    private void Movimento()
    {
        tornado.transform.Translate(0, velocidadeMovimento * Time.deltaTime, 0, Space.Self);
        // rotacao
        Utilidades.CalculaCooldown(contadorCooldown);
        contadorCooldown = Utilidades.CalculaCooldown(contadorCooldown);

        if (mudaDirecao)
        {
            tornado.transform.Rotate(0, 0, anguloRotacao * Time.deltaTime, Space.Self);
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
            tornado.transform.Rotate(0, 0, -anguloRotacao * Time.deltaTime, Space.Self);
        }
        if (contadorCooldown == 0 && mudaDirecao == false)
        {
            mudaDirecao = true;
            contadorCooldown = cooldownMudaDirecao;
        }
    }
}
