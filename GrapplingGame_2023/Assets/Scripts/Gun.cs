using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    private float lastShootTime = 0f;
    public float fireRate;
    public Camera cam;
    //public GameObject clip;

    //public ParticleSystem smoke;

    //public Animator anim;


    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastShootTime + fireRate)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                lastShootTime = Time.time;
                //anim.Play("Recoil");
                //anim.Play("Pump");
                //smoke.Play();


            }
        }

    }
}
