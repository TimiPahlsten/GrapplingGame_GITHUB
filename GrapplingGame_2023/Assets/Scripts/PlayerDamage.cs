using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public Rigidbody player;

    public Animator hurtEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hazard"))
        {
            hurtEffect.Play("Hurt");
            player.AddForce(other.transform.forward * 450);
        }
    }
}
