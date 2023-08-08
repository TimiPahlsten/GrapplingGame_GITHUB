using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAtPlayer : MonoBehaviour
{

    public Transform target;
    public float speed;

    void Update()
    {
        transform.LookAt(target);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

}
