using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHook : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject anchor;
    public LineRenderer lineRenderer;
    public LayerMask la;
    public float power =5;

    private List<Transform> _transformList = new List<Transform>();
    private bool isWebSHooted = false;
    private RaycastHit _hit;
    float _distance;
    private bool b_canShot => true;
    
    Vector3 right2 = new Vector3(0.99f, 0, 0.01f);
    private void Update()
    {
        Player.Instance.playerMovement.addMoveVector = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.R) && b_canShot)
        {
            isWebSHooted = !isWebSHooted;

            if (isWebSHooted)
                if (Physics.Raycast(Player.Instance.cameraManager.body.position, transform.forward, out _hit, 1024, la))
                {

                }
                else
                {
                    isWebSHooted = false;
                }
            lineRenderer.enabled = isWebSHooted;
            Player.Instance.playerMovement.hooked = isWebSHooted;
        }
        if (isWebSHooted)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _hit.point);
            _distance = Vector3.Distance(transform.position, _hit.point);
            if (_distance > _hit.distance)
            {
                Player.Instance.playerMovement.addMoveVector = (_hit.point - transform.position).normalized * (_distance - _hit.distance+ Vector3.Project(rb.velocity, (transform.position-_hit.point).normalized).magnitude);
                //rb.AddForce((_hit.point - transform.position)*2,ForceMode.Acceleration);
            }
            Player.Instance.playerMovement.hookdir = (_hit.point - Player.Instance.playerMovement.transform.position).normalized;
            //if (Vector3.Distance(transform.position, _hit.point) < 1)
            //{
            //    isWebSHooted = false;
            //    lineRenderer.enabled = false;
            //}
        }
    }

}