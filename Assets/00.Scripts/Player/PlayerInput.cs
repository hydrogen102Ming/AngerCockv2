using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    //Actions
    public Action OnM1;
    public Action OnM1Hold;
    public Action OnM2;
    public Action OnJump;
    public Action OnGrapple;
    public Action<Vector3> OnMovement;
    public Action<float, float> OnMouse;

    private float horizontal, vertical;
    [SerializeField] private float _inputSpeed = 100f;

    [Header("Mouse")]
    public float rotspeedx = 2; //mouse sensX
    public float rotspeedy = -2;//mouse sensY
    public float mx, my;

    //public KeyCode horKey1, horKey2, vertKey1, verKey2;
    //private float _horizontal, _vertical;

    public void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) OnM1?.Invoke();
        if (Input.GetKey(KeyCode.Mouse0)) OnM1Hold?.Invoke();
        if (Input.GetKeyDown(KeyCode.Mouse1)) OnM2?.Invoke();
        if (Input.GetKeyDown(KeyCode.Space)) OnJump?.Invoke();

        if (Input.GetKeyDown(KeyCode.R)) OnGrapple?.Invoke();

        horizontal = Mathf.Lerp(horizontal, Input.GetAxisRaw("Horizontal"), _inputSpeed * Time.deltaTime);
        vertical = Mathf.Lerp(vertical, Input.GetAxisRaw("Vertical"), _inputSpeed * Time.deltaTime);
        Vector3 inputDir = new Vector3(horizontal, 0, vertical);
        OnMovement?.Invoke(inputDir);

        mx += Input.GetAxisRaw("Mouse X") * rotspeedx;
        my += Input.GetAxisRaw("Mouse Y") * rotspeedy;
        OnMouse?.Invoke(mx, my);
    }
}
