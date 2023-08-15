using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class NewGunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    private object hit;
    public GameObject impactEffect;
    public float impactForce = 30f;
    private float nextTimeToFire = 0f;

    public AudioSource source;

    public bool readytofire;
    public bool aiming;
    public bool isReloading;
    public bool shooting;
    //public Transform BulletSpawnPoint;

    //public TrailRenderer BulletTrail;

    public Rigidbody player;

    public Animator cameraAnim;
    public Animator gunAnim;
    public Animator tommyAnim;

    [SerializeField]
    private float BulletSpeed = 100;

    // AMMO

    public int maxAmmo;
    public int currentAmmo;

    public TMP_Text ammoText;

    //GUN Types
    [SerializeField]
    bool pistol;
    [SerializeField]
    bool tommy;

    void Start()
    {
        shooting = false;
        readytofire = true;

        if (currentAmmo == 0)
            currentAmmo += 1;
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = currentAmmo.ToString() + "/" + maxAmmo;
        
        if(pistol)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && readytofire && currentAmmo == 1)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                shoot();


            }
        }

        if(tommy)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && readytofire && currentAmmo >= 1)
            {
                source.Play();
                nextTimeToFire = Time.time + 1f / fireRate;
                shootTommy();


            }
        }


    }

    void shoot()
    {
        shooting = true;

        muzzleFlash.Play();

        currentAmmo--;


        source.Play();

        readytofire = false;

        

        CameraRecoil();

        RaycastHit hit;

        gunAnim.SetTrigger("Shoot");

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }


            if (hit.rigidbody != null)
            {

                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }
        
        }

        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 2f);
    }


    public void Reload()
    {
        if(maxAmmo >= 1)
        {
            maxAmmo -= 1;
            currentAmmo += 1;
            readytofire = true;
            shooting = false;
            Debug.Log("readytoswitch");
        }


    }


    void CameraRecoil()
    {
        //player.AddForce(fpsCam.transform.forward * -450);
        cameraAnim.SetTrigger("Shoot");
    }

    void shootTommy()
    {
        currentAmmo--;

        RaycastHit hit;
        muzzleFlash.Play();

        tommyAnim.SetTrigger("Shoot");
        //gunAnim.SetTrigger("Shoot");

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }


            if (hit.rigidbody != null)
            {

                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }

        }

        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 2f);
    }

}
