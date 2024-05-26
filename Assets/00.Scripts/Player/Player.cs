using System;
using UnityEngine;
public class Player : MonoSingleton<Player>
{
    #region Components
    public CameraManager cameraManager;
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    #endregion
    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 moveDir;
    private float mx, my;
    protected override void Awake()
    {
        base.Awake();
        playerInput.OnMovement += HandleOnMovement;
        playerInput.OnMouse += HandleOnMouse;
    }
    private void Update()
    {
        GetInput();
        cameraManager.ApplyCamera(mx, my, playerMovement.gravityDir);
    }
    private void FixedUpdate()
    {
        moveDir.Normalize();
        playerMovement.IhateBaeRemake(moveDir.x, moveDir.z);
    }
    private void GetInput()
    {
        playerInput.GetInput();
    }
    private void HandleOnMouse(float arg1, float arg2)
    {
        mx = arg1;
        my = arg2;
    }
    private void HandleOnMovement(Vector3 obj)
    {
        moveDir = obj;
    }
}
