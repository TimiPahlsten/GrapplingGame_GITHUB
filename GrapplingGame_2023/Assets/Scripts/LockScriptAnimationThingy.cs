using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScriptAnimationThingy : MonoBehaviour
{

    public GameObject lockItem;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(!lockItem.activeInHierarchy)
        {
            anim.Play("GateOpen");
        }
    }
}
