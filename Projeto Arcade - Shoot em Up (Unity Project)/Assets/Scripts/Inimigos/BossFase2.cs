using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFase2 : MonoBehaviour
{
    public Camera mainCamera;
    public Transform laserOrigin;
    public GameObject gemaEsq;
    public float gunRange = 50.0f;
    public float fireRate = 0.2f;
    public float laserDuration = 1.0f;
    // alvo laser
    public GameObject alvoEsq, alvoDir, fxAvisoHit;
    private static float maxXEsq = 0, minXEsq = -28.0f, maxYEsq = 17.0f, minYEsq = 1.0f;

    private LineRenderer laserLine;
    private float fireTimer;

    private void Awake()
    {
        laserLine = gemaEsq.GetComponent<LineRenderer>();
    }
    private void Update()
    {
        fireTimer += Time.deltaTime;
        if(Input.GetButtonDown("Fire3") && fireTimer > fireRate)
        {
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = laserOrigin.position;
            RaycastHit hit;
            Vector3 dir = alvoEsq.transform.position - laserOrigin.position;
            dir = dir.normalized;
            if (Physics.Raycast(rayOrigin, dir, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                alvoEsq.GetComponent<ControlaPersonagem>().ReceberDano();
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (dir * gunRange));
            }
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }

    private Vector3 MudaPosicaoAlvoEsq()
    {
        Vector3 pos = alvoEsq.transform.position;
        float posX = Random.Range(minXEsq, maxXEsq);
        float posY = Random.Range(minYEsq, maxYEsq);

        pos = new Vector3(posX, posY);

        return pos;
    }

    private void AvisoPreHitEsq()
    {
        Vector3 pos = MudaPosicaoAlvoEsq();
        Instantiate(fxAvisoHit, pos, alvoEsq.transform.rotation);
    }
}
