using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update

    public float health = 50f;
    public GameObject destroyEffect;
    public bool destroy;
    public bool deactivate;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            if(destroy)
            {
                StartCoroutine("Die");
            }
            if(deactivate)
            {
                this.gameObject.SetActive(false);
            }

        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        GameObject impactGO = Instantiate(destroyEffect, transform.position, transform.rotation);
        Destroy(impactGO, 2f);
    }

}
