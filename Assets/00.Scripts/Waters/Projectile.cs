using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Projectile : Bullet
{
    //public AnimationCurve rotataionCurvv;
    public float rotatespeed = 0.01f;
    public GameObject dieEffect;
    public Color color;
    public LimitBullet blType;

    protected override void OnEnable()
    {
        transform.Rotate(new Vector3(Random.Range(-4, 4f), Random.Range(-4, 4f), Random.Range(-4, 4f)));
        base.OnEnable();
    }
    protected override void FixedUpdate()
    {
        //transform.Rotate(new Vector3(rotataionCurvv.Evaluate(), 0, 0));
        transform.Rotate(new Vector3(rotatespeed, 0, 0));
        base.FixedUpdate();
    }

    public override void Collision()
    {
        //if (_hit.transform.CompareTag("YelloWater"))
        //    return;
        if (_hit.collider.TryGetComponent<Paintable>(out Paintable pp))
        {

        PaintManager.instance.paint(pp, _hit.point,2, 0.2f, 0.5f, color);
        }
        transform.position = _hit.point;
        transform.up = _hit.normal;
        BulletLmitPool.Instance.GiveBullets(transform, blType);
        BulletPool.Instance.CollectBullets(gameObject, bulletType);
        //Instantiate(dieEffect,_hit.point,Quaternion.FromToRotation(Vector3.up,_hit.normal));
    }
}
