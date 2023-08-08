using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    private float nextTimeToHit = 0f;
    public float hitRate = 15f;
    public Camera fpsCam;

    public float damage = 10f;
    public float range = 100f;

    public GameObject impactEffect;
    public float impactForce = 30f;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("Fire2") && Time.time >= nextTimeToHit)
        {
            nextTimeToHit = Time.time + 1f / hitRate;
            shoveling();

        }
    }

    void shoveling()
    {
        anim.Play("Shovel");

        RaycastHit hit;

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
