using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour
{
    public BulletTypeMing ming;
    public ParticleSystem par;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.Mouse0) && time <=0)
        {
            time = 0.01f;
            BulletPool.Instance.GiveBullets(transform,ming);
            par.Play();
        }
        time -= Time.deltaTime;
    }

}
