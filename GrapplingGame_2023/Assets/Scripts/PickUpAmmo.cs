using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{

    public RayCastGun gunScript;
    public GameObject gun;

    public int amountOfAmmo;

    // Start is called before the first frame update
    void Start()
    {
        gunScript = gun.GetComponent<RayCastGun>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            gunScript.currentMaxHeldAmmo += amountOfAmmo;
            Destroy(other.gameObject);
        }

    }
}
