using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isWebSHooted = !isWebSHooted;

            if (isWebSHooted)
                if (Physics.Raycast(Player.Instance.playerMovement.cam.position, transform.forward, out _hit, 1024, la))
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

            if (Vector3.Distance(transform.position, _hit.point) > _hit.distance - 2f)
            {

                rb.AddForce(-(Vector3.Project(rb.velocity, _hit.point - transform.position).normalized), ForceMode.Impulse);
            }

            //if (Vector3.Distance(transform.position, _hit.point) < 1)
            //{
            //    isWebSHooted = false;
            //    lineRenderer.enabled = false;
            //}
        }
    }

}