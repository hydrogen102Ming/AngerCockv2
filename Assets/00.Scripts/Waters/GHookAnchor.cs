using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHookAnchor : MonoBehaviour
{
    //not finished and unused
    public Transform next;
    public LayerMask layerMask;
    RaycastHit _hit;
    private void OnEnable()
    {
        
    }

    public void Init(Transform t)
    {
        next = t;
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position,next.position - transform.position,out _hit, Vector3.Distance(transform.position, next.position) - 0.01f))
        {

        }
    }
}
