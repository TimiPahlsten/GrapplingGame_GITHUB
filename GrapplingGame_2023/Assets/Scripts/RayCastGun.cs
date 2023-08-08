using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayCastGun : MonoBehaviour
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
    //public Transform BulletSpawnPoint;

    //public TrailRenderer BulletTrail;

    public Rigidbody player;

    public Animator anim;
    public Animator cameraAnim;

    [SerializeField]
    private float BulletSpeed = 100;

    void Start()
    {
        readytofire = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Prepare gun
        if(Input.GetButton("Fire2"))
        {
            anim.SetBool("Aiming", true);
            cameraAnim.SetBool("FOV_AIMING", true);
            aiming = true;

        }
        else
        {
            anim.SetBool("Aiming", false);
            cameraAnim.SetBool("FOV_AIMING", false);
            aiming = false;

        }
        

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && readytofire && aiming)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            shoot();

        }

        if(!readytofire && aiming && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }


    }

    void shoot()
    {

        muzzleFlash.Play();
        anim.Play("Recoil");


        RaycastHit hit;

        source.Play();

        readytofire = false;

       


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


    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }

        trail.transform.position = HitPoint;

        Destroy(trail.gameObject, trail.time);
    }


    void Reload()
    {
        anim.Play("Reload");
    }

    void ResetGun()
    {
        Debug.Log("Reseted");
        readytofire = true;
    }

    void CameraRecoil()
    {
        player.AddForce(fpsCam.transform.forward * -450);
        cameraAnim.Play("CameraRecoilTest");
    }

}
