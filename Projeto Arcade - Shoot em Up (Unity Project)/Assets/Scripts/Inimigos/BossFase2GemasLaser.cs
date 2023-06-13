using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFase2GemasLaser : MonoBehaviour
{
    public Transform laserOrigin;
    public GameObject gema;
    public float gunRange = 55.0f;
    public float fireRate = 1.0f, duracaoAviso = 1.0f;
    public float laserDuration = 3.0f;
    // alvo laser
    public GameObject alvo, fxAvisoHit;
    public float maxXEsq = 0, minXEsq = -28.0f, maxYEsq = 17.0f, minYEsq = 1.0f;

    private LineRenderer laserLine;
    public GameObject instaciaAvisoHitEsq;
    public float avisoTimer, fireTimer;
    public Vector3 posAlvo;
    public bool achouAlvo = false;

    private void Awake()
    {
        laserLine = gema.GetComponent<LineRenderer>();
    }
    private void Update()
    {
        avisoTimer += Time.deltaTime;
        if(avisoTimer > fireRate)
        {
            AtiraDepoisAviso();
        }
    }
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
        fireTimer = 0;
        achouAlvo = false;
        avisoTimer = 0;
        StopAllCoroutines();
    }

    private Vector3 MudaPosicaoAlvo()
    {
        Vector3 pos = alvo.transform.position;
        float posX = Random.Range(minXEsq, maxXEsq);
        float posY = Random.Range(minYEsq, maxYEsq);

        alvo.transform.position = new Vector3(posX, posY, 0.0f);

        return alvo.transform.position;
    }

    private void AvisoPreHit()
    {
        posAlvo = MudaPosicaoAlvo();
        instaciaAvisoHitEsq = Instantiate(fxAvisoHit, posAlvo, fxAvisoHit.transform.rotation);
        achouAlvo = true;
    }

    private void AtiraDepoisAviso()
    {
        if (!achouAlvo)
        {
            AvisoPreHit();
        }
        
        if (achouAlvo)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer > duracaoAviso)
            {
                laserLine.SetPosition(0, laserOrigin.position);
                Vector3 rayOrigin = laserOrigin.position;
                RaycastHit hit;
                Vector3 dir = posAlvo - laserOrigin.position;
                dir = dir.normalized;
                if (Physics.Raycast(rayOrigin, dir, out hit, gunRange))
                {
                    laserLine.SetPosition(1, hit.point);
                }
                else
                {
                    laserLine.SetPosition(1, posAlvo);
                }
                StartCoroutine(ShootLaser());
                Destroy(instaciaAvisoHitEsq);
            }
        }
    }
}
