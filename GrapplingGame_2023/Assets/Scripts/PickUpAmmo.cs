using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{

    public NewGunScript gunScript;
    public GameObject gun;

    public int amountOfAmmo;

    // Start is called before the first frame update
    void Start()
    {
        gunScript = gun.GetComponent<NewGunScript>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            gunScript.maxAmmo += amountOfAmmo;
            Destroy(other.gameObject);
        }

    }
}
