using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbHookshot : MonoBehaviour
{
    private const float NORMAL_FOV = 60f;
    private const float HOOKVINE_FOV = 80f;

    private LineRenderer lr;
    private Vector3 hookPoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, player;
    public Transform Playercamera;
    public float maxDistance = 20f;
    private SpringJoint joint;

    public float spring;
    public float damper;
    public float scale;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(StartHook());
        }

    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {

        RaycastHit hit;
        if (Physics.Raycast(Playercamera.position, Playercamera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            hookPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = hookPoint;

            float distanceFromPoint = Vector3.Distance(player.position, hookPoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = scale;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;


        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);

    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, hookPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return hookPoint;
    }

    IEnumerator StartHook()
    {
        StartGrapple();
        yield return new WaitForSeconds(0.2f);
        StopGrapple();
    }
}

