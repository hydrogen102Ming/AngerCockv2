using System;
using System.Collections.Generic;
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
    public List<Item> playerItems;
    protected override void Awake()
    {
        base.Awake();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInput.OnMovement += HandleOnMovement;
        playerInput.OnMouse += HandleOnMouse;
        playerInput.OnJump += HandleOnJump;

        for(int i = 0; i < playerItems.Count; i++)
        {
            playerItems[i].Initialize(this);
        }
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
        playerInput.GetInput();
    }
    #region Handles
    private void HandleOnMovement(Vector3 obj)
    {
        moveDir = obj;
    }
    private void HandleOnMouse(float arg1, float arg2)
    {
        mx = arg1;
        my = arg2;
    }
    private void HandleOnJump()
    {
        playerMovement.TryJump();
    }
    #endregion
}
