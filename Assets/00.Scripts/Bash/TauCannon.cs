using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauCannon : MonoBehaviour
{
    public Animator animator;
    int _chargeCoundt = 0;

    private void FixedUpdate()
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
        Player.Instance.playerMovement.ri.AddForce(-transform.forward*(float) _chargeCoundt*6 ,ForceMode.Impulse);
    }
}
