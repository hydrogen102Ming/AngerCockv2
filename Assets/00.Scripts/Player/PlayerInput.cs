using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal, Vertical;

    [SerializeField] private float _inputSpeed=100f;
    void Start()
    {
        PlayerMovement.plmv.InputInit(this);
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Mathf.Lerp(Horizontal, Input.GetAxisRaw("Horizontal"), _inputSpeed * Time.deltaTime);
        Vertical = Mathf.Lerp(Vertical, Input.GetAxisRaw("Vertical"), _inputSpeed * Time.deltaTime);

    }
}
