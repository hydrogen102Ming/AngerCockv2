using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauCannon : MonoBehaviour
{
    public Rigidbody ri;
    public Animator animator;
    private int _chargeCoundt = 0;
    private void Start()
    {
        ri = Player.Instance.playerMovement.rigidBody;

    }
    private void Update()
    {
        animator.SetBool("TauCharge", Input.GetKey(KeyCode.Mouse1));
    }
    public void ChargeCount()
    {
        _chargeCoundt++;
        if( _chargeCoundt > 10)
        {
            _chargeCoundt--;
        }
    }
    public void Shoot()
    {
        ri.AddForce(-transform.forward*(float) _chargeCoundt*6 ,ForceMode.Impulse);
    }

}
