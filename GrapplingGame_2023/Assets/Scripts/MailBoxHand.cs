using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxHand : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("Reach");
    }

    void OnTriggerExit(Collider other)
    {
        anim.SetTrigger("Unreach");
    }

}
