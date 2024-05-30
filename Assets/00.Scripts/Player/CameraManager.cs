using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform body, dick;
    Vector3 right2 = new Vector3(0.99f, 0,0.01f);
    public void ApplyCamera(float mx, float my, Vector3 gravityDir)
    {
        //cam.position = body.position; changed to 11
        transform.position = body.position;
        my = Mathf.Clamp(my,-89,89);
        transform.eulerAngles = new Vector3(my, mx, 0f);
        dick.eulerAngles = new Vector3(my, mx, 0f);
        body.root.rotation = Quaternion.LookRotation(Vector3.Cross(((gravityDir == Vector3.right)|| (gravityDir == Vector3.left)) ? right2:Vector3.right, gravityDir), gravityDir) * Quaternion.Euler(0, mx, 0);
    }
}
