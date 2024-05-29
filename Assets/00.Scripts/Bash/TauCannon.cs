using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauCannon : Item
{
    public Animator animator;
    private int _chargeCoundt = 0;
    public override void Initialize(Player player)
    {
         //player.playerInput.OnM2 += HandleOnM2;

    }
    private void HandleOnM2()
    {
        animator.SetBool("TauCharge", true);
    }
    private void Update()
    {
        animator.SetBool("TauCharge", Input.GetKey(KeyCode.Mouse1));
    }
    //private void FixedUpdate()
    //{
    //    animator.SetBool("TauCharge", Input.GetKey(KeyCode.Mouse1));
    //}
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
        Player.Instance.playerMovement.ri.AddForce(-transform.forward*(float) _chargeCoundt*6 ,ForceMode.Impulse);
    }

}
