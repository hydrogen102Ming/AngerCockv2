using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour
{
    public BulletTypeMing ming;
    public ParticleSystem par;
    private float time = 0;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && time <= 0)
        {
            time = 0.02f;
            //BulletPool.Instance.GiveBullets(transform,ming);
            //par.Play();
            Fire();
        }
        time -= Time.deltaTime;
    }
    public void Fire()
    {
        BulletPool.Instance.GiveBullets(transform,ming);
        par.Play();
    }


}
