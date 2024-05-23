using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHook : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject anchor;
    public LineRenderer lineRenderer;
    public LayerMask la;
    List<Transform> _transformList = new List<Transform>();
    bool isWebSHooted = false;
    RaycastHit _hit;
    void Start()
    {//if(_rb == null)
        //_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            isWebSHooted = !isWebSHooted;

            if(isWebSHooted)
                if(Physics.Raycast(PlayerMovement.plmv.Cam.position, transform.forward,out _hit,1024,la))
                {
                    
                }else
                {
                    isWebSHooted = false;
                }
            lineRenderer.enabled = isWebSHooted;
        }
    }

    void FixedUpdate()
    {
        if(isWebSHooted)
        {
            lineRenderer.SetPosition(0,transform.position);
            lineRenderer.SetPosition(1, _hit.point);
            rb.AddForce((_hit.point - transform.position).normalized*15,ForceMode.VelocityChange);
            if (Vector3.Distance(transform.position, _hit.point) < 1)
            {
                isWebSHooted = false;
                lineRenderer.enabled = false;
            }
        }
    }
}
