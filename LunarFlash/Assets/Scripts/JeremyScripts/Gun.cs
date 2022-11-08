using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GunState
{
    Idle,
    Shooting,
 
}

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float gunRange = 100f;
    public float laserDuration = 0.05f;

    public AudioSource shootSound;
    private GunState gunState;

    public ParticleSystem muzzleFlash;
    public GameObject impact;

    public Transform laserOrigin;

    public Camera fpsCam;

    LineRenderer laserLine;
    // Start is called before the first frame update
    
    void Start()
    {
        shootSound = GetComponent<AudioSource>();
        gunState = GunState.Idle;
    }

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        
        switch (gunState)
        {
            case GunState.Idle:
                if (Input.GetButtonDown("Fire1"))
                {
                    StartShoot();
                }
                break;
            case GunState.Shooting:
                if (Input.GetButtonUp("Fire1"))
                {
                    gunState = GunState.Idle;
                }
                break;
            default:
                break;
                
            

        }
    }

    public void StartShoot()
    {
        Shoot(); 
        gunState = GunState.Shooting;
    }

    void Shoot()
    {
        shootSound.Play();
        muzzleFlash.Play();

        laserLine.SetPosition(0, laserOrigin.position);
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gunRange))
        {
            Debug.Log(hit.transform.name);

            laserLine.SetPosition(1, hit.point);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 30f);
            }

            GameObject impactObj = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            
            Destroy(impactObj, 1f);
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * gunRange));
        }
        StartCoroutine(ShootLaser());
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
