using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform body, dick;
    public void ApplyCamera(float mx, float my, Vector3 gravityDir)
    {
        //cam.position = body.position; changed to 11
        transform.position = body.position;

        transform.eulerAngles = new Vector3(my, mx, 0f);
        dick.eulerAngles = new Vector3(my, mx, 0f);
        body.root.rotation = Quaternion.LookRotation(Vector3.forward, gravityDir) * Quaternion.Euler(new Vector3(0, mx, 0));
    }
}
