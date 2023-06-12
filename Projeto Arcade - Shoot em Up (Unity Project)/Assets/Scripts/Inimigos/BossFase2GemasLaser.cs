using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFase2GemasLaser : MonoBehaviour
{
    public Transform laserOrigin;
    public GameObject gema;
    public float gunRange = 55.0f;
    public float fireRate = 1.0f;
    public float laserDuration = 3.0f;
    // alvo laser esquerdo
    public GameObject alvo, fxAvisoHit;
    public float maxXEsq = 0, minXEsq = -28.0f, maxYEsq = 17.0f, minYEsq = 1.0f;

    private LineRenderer laserLine;
    private GameObject instaciaAvisoHitEsq;
    private float avisoTimer;

    private void Awake()
    {
        laserLine = gema.GetComponent<LineRenderer>();
    }
    private void Update()
    {
        avisoTimer += Time.deltaTime;
        if(avisoTimer > fireRate)
        {
            StartCoroutine(AtiraDepoisAviso());

            avisoTimer = 0;
        }
    }
    IEnumerator ShootLaserEsq()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
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
        Vector3 pos = MudaPosicaoAlvo();
        instaciaAvisoHitEsq = Instantiate(fxAvisoHit, pos, fxAvisoHit.transform.rotation);
    }

    IEnumerator AtiraDepoisAviso()
    {
        AvisoPreHit();
        
        yield return new WaitForSeconds(3.0f);

        laserLine.SetPosition(0, laserOrigin.position);
        Vector3 rayOrigin = laserOrigin.position;
        RaycastHit hit;
        Vector3 dir = alvo.transform.position - laserOrigin.position;
        dir = dir.normalized;
        if (Physics.Raycast(rayOrigin, dir, out hit, gunRange))
        {
            laserLine.SetPosition(1, hit.point);
        }
        else
        {
            laserLine.SetPosition(1, alvo.transform.position);
        }
        StartCoroutine(ShootLaserEsq());
        Destroy(instaciaAvisoHitEsq);
    }
}
