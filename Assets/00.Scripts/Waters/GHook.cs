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

    private List<Transform> _transformList = new List<Transform>();
    private bool isWebSHooted = false;
    private RaycastHit _hit;
    float _distance;
    private bool b_canShot => true;
    private void Update()
    {
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
        }
        if (isWebSHooted)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _hit.point);
            _distance = Vector3.Distance(transform.position, _hit.point);
            if (Vector3.Distance(transform.position, _hit.point) > _hit.distance - 2)
            {
                rb.velocity += -(Vector3.Project(rb.velocity, _hit.point - transform.position));
                print("cock");
                rb.AddForce((_hit.point - transform.position));
            }

            //if (Vector3.Distance(transform.position, _hit.point) < 1)
            //{
            //    isWebSHooted = false;
            //    lineRenderer.enabled = false;
            //}
        }
    }

}