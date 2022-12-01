using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum GunState
{
    Idle,
    Shooting,
    //States for shooting the gun
}

public class Gun : MonoBehaviour
{

    public TMP_Text currnent;
    public TMP_Text totalAmmo;

    public float damage = 10f;
    public float gunRange = 100f;
    public float laserDuration = 0.05f;
    public int maxAmmo = 25;
    private int currentAmmo;
    public float reloadTime = 2.5f;
    private bool isReloading = false;

    public Animator reloadAnimator;

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
        currentAmmo = maxAmmo;

        currnent.text = currentAmmo.ToString();
        totalAmmo.text = maxAmmo.ToString();
    }

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {

        /*switch (gunState)
        {
            case GunState.Idle:
                if (Input.GetButtonDown("Fire1"))
                {
                    StartShoot();
                    //If "Fire1" input pressed, start shooting
                }
                break;
            case GunState.Shooting:
                if (Input.GetButtonUp("Fire1"))
                {
                    gunState = GunState.Idle;
                    //If "Fire1" input not pressed, back to idle
                }
                break;
            default:
                break;
                
            

        }*/

        if (isReloading)
        {
            return;
        }

        if(currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            currnent.text = maxAmmo.ToString();
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            shootSound.Play();
            muzzleFlash.Play();
            Shoot();
            currnent.text = currentAmmo.ToString();
        }
    }

    public void StartShoot()
    {
        Shoot(); 
        gunState = GunState.Shooting;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        reloadAnimator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        reloadAnimator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    void Shoot()
    {
       
        currentAmmo--;

        laserLine.SetPosition(0, laserOrigin.position);
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gunRange))
        {
            Debug.Log(hit.transform.name);
            //This is used for damaging enemy when detected by raycast
            TakeDamage take = hit.transform.GetComponent<TakeDamage>();
            if (take != null)
            {
                take.hitDamage(damage);
            }
            //line renderer stuff
            laserLine.SetPosition(1, hit.point);

            

            

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

    public void GunDamageUpgradeExecution(float percent, int howlong)
    {
        StartCoroutine(GunDamageUpgrade(percent, howlong));
    }

    IEnumerator GunDamageUpgrade(float percent, int howLong) // percent being from 0 (0% to 1(100%) 
    {

        var defaultDamage = damage;
        damage = damage * (1 + percent);

        yield return new WaitForSeconds(howLong);

        damage = defaultDamage;
    }
}
