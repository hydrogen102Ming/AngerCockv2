using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal, Vertical;
    public KeyCode horKey1,horKey2, vertKey1,verKey2;

    private float _horizontal, _vertical;
    [SerializeField] private float _inputSpeed=100f;
    void Start()
    {
        PlayerMovement.plmv.InputInit(this);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(horKey1))
            _horizontal++;
        if (Input.GetKeyUp(horKey1))
            _horizontal--;
        if(Input.GetKeyDown(vertKey1))
            _vertical++;
        if (Input.GetKeyUp(vertKey1))
            _vertical--;



        Horizontal = Mathf.Lerp(Horizontal, Input.GetAxisRaw("Horizontal"), _inputSpeed * Time.deltaTime);
        Vertical = Mathf.Lerp(Vertical, Input.GetAxisRaw("Vertical"), _inputSpeed * Time.deltaTime);

    }
}
