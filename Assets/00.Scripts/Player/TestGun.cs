using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour
{
    public BulletTypeMing ming;
    public ParticleSystem par;
    float time = 0;
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0) && time <=0)
        {
            time = 0.02f;
            //BulletPool.Instance.GiveBullets(transform,ming);
            //par.Play();
            Fire();
        }
        time -= Time.fixedDeltaTime;
    }
    public void Fire()
    {
        BulletPool.Instance.GiveBullets(transform,ming);
        par.Play();
    }
}
