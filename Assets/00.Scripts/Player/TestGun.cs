using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : Item
{
    public BulletTypeMing ming;
    public ParticleSystem par;
    private float time = 0;
    public override void Initialize(Player player)
    {
        player.playerInput.OnM1Hold += HandleOnM1;
    }
    private void HandleOnM1()
    {
        if (time <= 0)
        {
            time = 0.02f;
            //BulletPool.Instance.GiveBullets(transform,ming);
            //par.Play();
            Fire();
        }
    }
    private void Update()
    {
        time -= Time.deltaTime;
    }
    //void FixedUpdate()
    //{
    //    //if(Input.GetKey(KeyCode.Mouse0) && time <=0)
    //    //{
    //    //    time = 0.02f;
    //    //    //BulletPool.Instance.GiveBullets(transform,ming);
    //    //    //par.Play();
    //    //    Fire();
    //    //}
    //    //time -= Time.fixedDeltaTime;
    //}
    public void Fire()
    {
        BulletPool.Instance.GiveBullets(transform,ming);
        par.Play();
    }


}
