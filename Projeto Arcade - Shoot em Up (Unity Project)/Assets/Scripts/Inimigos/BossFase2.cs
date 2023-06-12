using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFase2 : MonoBehaviour
{
    public Camera mainCamera;
    public Transform laserOrigin;
    public GameObject gemaEsq;
    public float gunRange = 55.0f;
    public float fireRate = 5.0f;
    public float laserDuration = 3.0f;
    // alvo laser esquerdo
    public GameObject alvoEsq, alvoDir, fxAvisoHit;
    public static float maxXEsq = 0, minXEsq = -28.0f, maxYEsq = 17.0f, minYEsq = 1.0f;

    private LineRenderer laserLineEsq, laserLineDir;
    private GameObject instaciaAvisoHit;
    private float avisoTimer;

    private void Awake()
    {
        laserLineEsq = gemaEsq.GetComponent<LineRenderer>();
        laserLineDir = gemaEsq.GetComponent<LineRenderer>();
    }
    private void Update()
    {
        avisoTimer += Time.deltaTime;
        if(Input.GetButtonDown("Fire3") && avisoTimer > fireRate)
        {
            StartCoroutine(AtiraDepoisAvisoEsq());

            avisoTimer = 0;
        }
    }

    IEnumerator ShootLaserEsq()
    {
        laserLineEsq.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLineEsq.enabled = false;
    }

    private Vector3 MudaPosicaoAlvoEsq()
    {
        Vector3 pos = alvoEsq.transform.position;
        float posX = Random.Range(minXEsq, maxXEsq);
        float posY = Random.Range(minYEsq, maxYEsq);

        alvoEsq.transform.position = new Vector3(posX, posY, 0.0f);

        return alvoEsq.transform.position;
    }

    private void AvisoPreHitEsq()
    {
        Vector3 pos = MudaPosicaoAlvoEsq();
        instaciaAvisoHit = Instantiate(fxAvisoHit, pos, fxAvisoHit.transform.rotation);
    }

    IEnumerator AtiraDepoisAvisoEsq()
    {
        AvisoPreHitEsq();
        
        yield return new WaitForSeconds(3.0f);

        laserLineEsq.SetPosition(0, laserOrigin.position);
        Vector3 rayOrigin = laserOrigin.position;
        RaycastHit hit;
        Vector3 dir = alvoEsq.transform.position - laserOrigin.position;
        dir = dir.normalized;
        if (Physics.Raycast(rayOrigin, dir, out hit, gunRange))
        {
            laserLineEsq.SetPosition(1, hit.point);
            alvoEsq.GetComponent<ControlaPersonagem>().ReceberDano();
        }
        else
        {
            laserLineEsq.SetPosition(1, alvoEsq.transform.position);
        }
        StartCoroutine(ShootLaserEsq());
        Destroy(instaciaAvisoHit);
    }
}
