using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody ri;
    public static PlayerMovement plmv;
    public Transform Cam;
    public Transform col;
    private CameraManager _camMana;

    [SerializeField]
    private float _speed = 5f, _maxSpeed = 8,_distance=0.60001f,_jumpPower=55f,_radius=0.2f,_paintMaxspeed=2f,_paintSpeed=0.5f,_airspeed;
    [SerializeField]
    private LayerMask _groundLa,_paintLa;
    private PlayerInput _input;
    private Vector3 _localVelocity,_plup;
    bool _isGrounded = false,_isPainted = false;
    private RaycastHit _hit;
    private float _height,_speedMulti=1f,_maxSpeedMulti=1f;
    private Collider[] _Paints = new Collider[1];
    public Vector3 gravityDir;

    // Start is called before the first frame update
    void Awake()
    {
        plmv = this;
        _distance = transform.localScale.y*0.901f;
        _radius = _distance * 0.2f;
        _height = col.localScale.y;
    }

    public void InputInit(PlayerInput input1)
    {
        _input = input1;
    }

    public void CamInit(CameraManager cc)
    {
        _camMana = cc;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Physics.OverlapSphereNonAlloc(transform.position-transform.up*-0.5f,1f,_Paints,_paintLa) > 0)
        {
            _isPainted = true;
            _isGrounded = true;
            for(int i=0; i<_Paints.Length; i++)
            {
                gravityDir = (gravityDir + _Paints[i].transform.up)/2;
            }
            _speedMulti = _paintSpeed;
            _maxSpeedMulti = _paintMaxspeed;
            //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _airspeed);

        }
        else
        {
            _isPainted = false;
            gravityDir = Vector3.up;
            _speedMulti = 1;
            _maxSpeedMulti = 1f;
        }

        _localVelocity = transform.InverseTransformVector(ri.velocity);
        //ri.velocity.magnitude < _maxSpeed
        //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical) - localVelocity / _maxSpeed)) * _speed);
        if (Physics.SphereCast(transform.position, _radius, -transform.up, out _hit, _distance, _groundLa) || _isPainted)
        {
            _isGrounded = true;
            //ri.velocity = ri.velocity - Vector3.up * ri.velocity.y;
            //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _speed);
            ri.AddForce((transform.TransformVector(new Vector3(_input.Horizontal - _localVelocity.x / 1.3f / _maxSpeed/_maxSpeedMulti, 0, _input.Vertical - _localVelocity.z / _maxSpeed/_maxSpeedMulti))) * _speed*_speedMulti);
            print(ri.velocity.magnitude);
        }
        else
        {
            _isGrounded = false;
            ri.AddForce((transform.TransformVector(new Vector3(_input.Horizontal - _localVelocity.x / 1.8f / _maxSpeed, 0, _input.Vertical - _localVelocity.z /1.2f/ _maxSpeed))) * _speed);
            //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _airspeed);
            ri.AddForce(gravityDir * -9);
        }
    }

    private void Update()
    {
        //transform.Rotate(new Vector3(0,Input.GetAxisRaw("Mouse X"), 0));
        if (Input.GetKeyDown(KeyCode.Space)&&(_isGrounded||_isPainted))
        {
            ri.AddForce(transform.up * _jumpPower);
        }
    }

}
