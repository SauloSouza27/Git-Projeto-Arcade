using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBossFase2 : MonoBehaviour
{
    private GameObject alvo;
    //movimento
    public GameObject cabeca;
    public float velocidadeRotacaoCabeca = 1.0f, velocidadeRotacaoCorpo = 0.5f;
    //vida
    public int primeiraVida = 300, segundaVida = 300, terceiraVida = 300, quartaVida = 300;
    private bool stage1 = true, stage2 = false, stage3 = false, stage4 = false;
    //tiros
    public GameObject balaRa, balaOlhos, olhoRa, olhoEsq, olhoDir;
        // Ra
        [Range(0, 5)] public float cooldownRa = 0.3f, tempoDisparoRa = 3.0f;
        public int numeroDisparosRa = 10;
        private float contadorCooldownRa, contadorDisparosRa;
        private bool ativaArmaRa = false;
        // Olhos
        [Range(0, 5)] public float cooldownOlhos = 0.3f, tempoDisparoOlhos = 3.0f;
        public int numeroDisparosOlhos = 10;
        private float contadorCooldownOlhos, contadorDisparosOlhos;
        private bool ativaArmaOlhos = false;
    //lasers
    public GameObject laserEsq, laserDir;
    // tornado
    public GameObject tornadoPrefab, spawnTornEsq, SpawnTornDir;
    [Range(0, 5)] public float cooldownTornado = 0.3f, tempoDisparoTornado = 3.0f;
    public int numeroDisparosTornado = 4;
    private float contadorCooldownTornado, contadorDisparosTornado;
    private bool ativaArmaTornado = false;
    // Materiais
    private MeshRenderer[] renderers;
    private Material[] materiais;
    // FX
    public GameObject fxExplosionPrefab, fxExpHit, fxExpHitPet;
    // ui VItoria
    public GameObject uiVitoria;
    // partes corpo boss fase 2
    private Transform[] partesCorpo;
    // SFX
    public AudioSource[] somTiros = new AudioSource[3];
    
    private void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("Player");
        // Busca materiais do inimigo
        renderers = GetComponentsInChildren<MeshRenderer>();
        materiais = new Material[renderers.Length - 2];
        for (int i = 0; i < renderers.Length - 2; i++)
        {
            materiais[i] = renderers[i].material;
        }
        // busca partes Corpo
        partesCorpo = GetComponentsInChildren<Transform>();

        ativaArmaRa = false;
        StartCoroutine(IntervaloDisparoRa(2.0f));
    }
    private void Update()
    {
        if (Time.timeScale == 0) return;

        MovimentoCabeca();
        MovimentoCorpo();

        if (stage1)
        {
            ArmaRa();
        }
        
        if (stage2)
        {
            ArmaOlhos();
        }

        if (stage3)
        {
            ArmaRa();
            ArmaTornado();
        }

        if (stage4)
        {
            ArmaOlhos();
            ArmaTornado();
        }
    }
    //Movimento
    private void MovimentoCorpo()
    {
        // rotacao corpo
        Vector3 direcao = alvo.transform.position - transform.position;
        direcao.y = -17.20f;
        if (direcao.x <= -3.5f)
        {
            direcao.x = -3.5f;
        }
        if (direcao.x >= 3.5f)
        {
            direcao.x = 3.5f;
        }
        direcao = direcao.normalized;
        transform.up = Vector3.Slerp(transform.up, - direcao, velocidadeRotacaoCorpo * Time.deltaTime);
    }
    private void MovimentoCabeca()
    {
        // rotacao cabeca
        Vector3 direcao = alvo.transform.position - cabeca.transform.position;
        direcao = direcao.normalized;
        cabeca.transform.up = Vector3.Slerp(cabeca.transform.up, - direcao, velocidadeRotacaoCabeca * Time.deltaTime);
    }
    // ativa/desativa lasers
    private void AtivaLasers()
    {
        laserDir.SetActive(true);
        laserEsq.SetActive(true);
    }
    private void DesativaLasers()
    {
        laserDir.SetActive(false);
        laserEsq.SetActive(false);
    }

    // arma olho de Ra
    private void ArmaRa()
    {
        // disparo arma Ra
        if (ativaArmaRa)
        {
            // Cooldown e controle tiro Ra
            Utilidades.CalculaCooldown(contadorCooldownRa);
            contadorCooldownRa = Utilidades.CalculaCooldown(contadorCooldownRa);
            if (contadorCooldownRa == 0)
            {
                TiroBalaRa();
                contadorCooldownRa = cooldownRa;
                contadorDisparosRa++;
                if (contadorDisparosRa < numeroDisparosRa) return;
                else
                {
                    contadorDisparosRa = 0;
                    ativaArmaRa = false;
                    StartCoroutine(IntervaloDisparoRa(tempoDisparoRa));
                }
            }
        }
    }
    private void TiroBalaRa()
    {
        Instantiate(balaRa, olhoRa.transform.position, olhoRa.transform.rotation);
        somTiros[0].Play();
    }
    private IEnumerator IntervaloDisparoRa(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        ativaArmaRa = true;
    }

    // arma olhos
    private void ArmaOlhos()
    {
        // disparo arma Olhos
        if (ativaArmaOlhos)
        {
            // Cooldown e controle tiro Olhos
            Utilidades.CalculaCooldown(contadorCooldownOlhos);
            contadorCooldownOlhos = Utilidades.CalculaCooldown(contadorCooldownOlhos);
            if (contadorCooldownOlhos == 0)
            {
                TiroBalaOlhos();
                contadorCooldownOlhos = cooldownOlhos;
                contadorDisparosOlhos++;
                if (contadorDisparosOlhos < numeroDisparosOlhos) return;
                else
                {
                    contadorDisparosOlhos = 0;
                    ativaArmaOlhos = false;
                    StartCoroutine(IntervaloDisparoOlhos(tempoDisparoOlhos));
                }
            }
        }
    }

    private void TiroBalaOlhos()
    {
        Instantiate(balaOlhos, olhoEsq.transform.position, olhoEsq.transform.rotation);
        Instantiate(balaOlhos, olhoDir.transform.position, olhoDir.transform.rotation);
        somTiros[1].Play();
    }
    private IEnumerator IntervaloDisparoOlhos(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        ativaArmaOlhos = true;
    }

    // Disparo Tornados
    private void ArmaTornado()
    {
        // disparo arma Tornado
        if (ativaArmaTornado)
        {
            // Cooldown e controle tiro Tornado
            Utilidades.CalculaCooldown(contadorCooldownTornado);
            contadorCooldownTornado = Utilidades.CalculaCooldown(contadorCooldownTornado);
            if (contadorCooldownTornado == 0)
            {
                TiroTornados();
                contadorCooldownTornado = cooldownTornado;
                contadorDisparosTornado++;
                if (contadorDisparosTornado < numeroDisparosTornado) return;
                else
                {
                    contadorDisparosTornado = 0;
                    ativaArmaTornado = false;
                    StartCoroutine(IntervaloDisparoTornados(tempoDisparoTornado));
                }
            }
        }
    }
    private void TiroTornados()
    {
        Instantiate(tornadoPrefab, spawnTornEsq.transform.position, tornadoPrefab.transform.rotation);
        Instantiate(tornadoPrefab, SpawnTornDir.transform.position, tornadoPrefab.transform.rotation);
        somTiros[2].Play();
    }
    private IEnumerator IntervaloDisparoTornados(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        ativaArmaTornado = true;
    }

    // Dano e Stágios
    private void CaluclaDanoInimigo(int dano)
    {
        if (stage1)
        {
            if (primeiraVida > 0)
            {
                primeiraVida -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (primeiraVida <= 0)
            {
                stage1 = false;
                stage2 = true;
                AtivaLasers();
                StartCoroutine(IntervaloDisparoOlhos(tempoDisparoOlhos));
            }
        }
        
        if (stage2)
        {
            if (segundaVida > 0)
            {
                segundaVida -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (segundaVida <= 0)
            {
                stage2 = false;
                stage3 = true;
                cooldownRa = 0.5f;
                DesativaLasers();
                StartCoroutine(IntervaloDisparoTornados(tempoDisparoTornado));
            }
        }

        if (stage3)
        {
            if (terceiraVida > 0)
            {
                terceiraVida -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (terceiraVida <= 0)
            {
                stage3 = false;
                stage4 = true;
                AtivaLasers();
            }
        }

        if (stage4)
        {
            if (quartaVida > 0)
            {
                quartaVida -= dano;

                foreach (Material material in materiais)
                {
                    StartCoroutine(Utilidades.PiscaCorRoutine(material));
                }
            }
            if (quartaVida <= 0)
            {
                stage4 = false;
                DesativaLasers();
                Instantiate(fxExplosionPrefab, transform.position, transform.rotation);
                foreach(Transform ts in partesCorpo)
                {
                    ts.gameObject.SetActive(false);
                }
                partesCorpo[0].gameObject.SetActive(true);
                StartCoroutine(CarregaUIVItoria(uiVitoria, 2.0f));
            }
        }
    }
    private IEnumerator CarregaUIVItoria(GameObject uiVitoria, float delay)
    {
        yield return new WaitForSeconds(delay);
        uiVitoria.SetActive(true);
        Time.timeScale = 0;
        StopAllCoroutines();
        yield break;
    }
    private void FXExplosionHit(GameObject fxExpHit, Collision colisor)
    {
        ContactPoint point = colisor.GetContact(0);
        Vector3 pos = point.point;
        Instantiate(fxExpHit, pos, colisor.transform.rotation);
    }
    private void OnCollisionEnter(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("BalaPersonagem"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<ControlaPersonagem>().danoArmaPrincipal;

            FXExplosionHit(fxExpHit, colisor);
            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("BalaPet"))
        {
            Destroy(colisor.gameObject);
            int dano = alvo.GetComponent<DisparoArmaPet>().danoArmaPet;

            FXExplosionHit(fxExpHitPet, colisor);
            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("OrbeGiratorio"))
        {
            int dano = alvo.GetComponent<RespostaOrbeGiratorio>().danoOrbeGiratorio;

            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("ProjetilSerra"))
        {
            int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;

            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("Player"))
        {
            int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;

            CaluclaDanoInimigo(dano);
        }
        if (colisor.gameObject.CompareTag("Escudo"))
        {
            int dano = alvo.GetComponent<ControlaPersonagem>().danoContato;

            CaluclaDanoInimigo(dano);
        }
    }
    private void OnCollisionExit(Collision colisor)
    {
        if (colisor.gameObject.CompareTag("ProjetilSerra"))
        {
            int dano = alvo.GetComponent<DisparoArmaSerra>().danoSerra;

            CaluclaDanoInimigo(dano);
        }
    }
}
