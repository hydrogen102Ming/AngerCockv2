using System;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoSingleton<Player>
{
    #region Components
    public CameraManager cameraManager;
    public PlayerMovement playerMovement;
    #endregion

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 moveDir;

    [Header("Mouse")]
    public float rotspeedx = 2; //mouse sensX
    public float rotspeedy = -2;//mouse sensY
    private float horizontal, vertical;
    [SerializeField] private float _inputSpeed = 100f;
    private float mx, my;
    protected override void Awake()
    {
        base.Awake();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        GetInput();
        cameraManager.ApplyCamera(mx, my, playerMovement.gravityDir);
    }
    private void FixedUpdate()
    {
        moveDir.Normalize();
        playerMovement.IHateBaeRemasteredMordenWarfare4(moveDir.x, moveDir.z);
    }
    private void GetInput()
    {
        horizontal = Mathf.Lerp(horizontal, Input.GetAxisRaw("Horizontal"), _inputSpeed * Time.deltaTime);
        vertical = Mathf.Lerp(vertical, Input.GetAxisRaw("Vertical"), _inputSpeed * Time.deltaTime);
        moveDir = new Vector3(horizontal, 0, vertical);

        mx += Input.GetAxisRaw("Mouse X") * rotspeedx;
        my += Input.GetAxisRaw("Mouse Y") * rotspeedy;
        //my = Mathf.Clamp(my, -90, 90);

        if (Input.GetKeyDown(KeyCode.Space)) playerMovement.TryJump();
    }
}
