using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Tool : MonoBehaviour
{
    public GameObject item;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            item.SetActive(true);
            Destroy(this.gameObject);
        }


    }

}
