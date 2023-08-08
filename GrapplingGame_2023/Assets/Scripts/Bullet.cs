using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{

    private Rigidbody rB;

    private bool targetHit;

  

  
    private void Start()
    {
        rB = GetComponent<Rigidbody>();

    }

    public void OnCollisionEnter(Collision collision)
    {
        //enemyAI.TakeDamage(50);

        Destroy(gameObject, 2f);

        if (targetHit)
            return;
        else
            targetHit = true;

        rB.isKinematic = true;
      
    }
}
