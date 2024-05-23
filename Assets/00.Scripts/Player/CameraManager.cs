using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cam, body,dick;
    public float rotspeedx=2,rotspeedy=-2;
    public float mx, my;
    // Update is called once per frame
    private void Awake()
    {
        PlayerMovement.plmv.Cam = transform;
        PlayerMovement.plmv.CamInit(this);
    }
    void Update()
    {
        cam.position = body.position;

        mx +=Input.GetAxisRaw("Mouse X")*rotspeedx;
        my += Input.GetAxisRaw("Mouse Y") * rotspeedy;
        transform.eulerAngles = new Vector3(my,mx,0f);
        dick.eulerAngles = new Vector3(my, mx, 0f);
        body.root.rotation = Quaternion.LookRotation(Vector3.forward,PlayerMovement.plmv.gravityDir) * Quaternion.Euler(new Vector3(0, mx, 0));
       
    }
}
