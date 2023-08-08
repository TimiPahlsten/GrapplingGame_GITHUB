using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbCamera : MonoBehaviour
{

    public Transform player;

    void Update()
    {
        transform.position = player.transform.position;
    }
}
