using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody ri;
    public static PlayerMovement plmv;
    public Transform Cam;
    private CameraManager camMana;

    [SerializeField]
    private float _speed = 5f, _maxSpeed = 8,distance=0.60001f,jumpPower=55f,radius=0.2f,paintMaxspeed=2f,paintSpeed=0.5f,_airspeed;
    [SerializeField]
    private LayerMask la,paintLayer;
    public Transform col;
    private PlayerInput input;
    private Vector3 localVelocity,plup;
    bool _isGrounded = false,_isPainted = false;
    private RaycastHit _hit;
    private float height,speedMulti=1f,maxSpeedMulti=1f;
    private Collider[] Paints = new Collider[1];
    public Vector3 gravityDir;

    // Start is called before the first frame update
    void Awake()
    {

        plmv = this;
        distance = transform.localScale.y*0.901f;
        radius = distance * 0.2f;
        height = col.localScale.y;
    }

    public void InputInit(PlayerInput input1)
    {
        input = input1;
    }

    public void CamInit(CameraManager cc)
    {
        camMana = cc;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Physics.OverlapSphereNonAlloc(transform.position-transform.up*-0.5f,1f,Paints,paintLayer) > 0)
        {
            _isPainted = true;
            _isGrounded = true;
            for(int i=0; i<Paints.Length; i++)
            {
                gravityDir = (gravityDir + Paints[i].transform.up)/2;
            }
            speedMulti = paintSpeed;
            maxSpeedMulti = paintMaxspeed;
            //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _airspeed);

        }
        else
        {
            _isPainted = false;
            gravityDir = Vector3.up;
            speedMulti = 1;
            maxSpeedMulti = 1f;
        }

        localVelocity = transform.InverseTransformVector(ri.velocity);
        //ri.velocity.magnitude < _maxSpeed
        //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical) - localVelocity / _maxSpeed)) * _speed);
        if (Physics.SphereCast(transform.position, radius, -transform.up, out _hit, distance, la) || _isPainted)
        {
            _isGrounded = true;
            //ri.velocity = ri.velocity - Vector3.up * ri.velocity.y;
            //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _speed);
            ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal - localVelocity.x / 1.3f / _maxSpeed/maxSpeedMulti, 0, input.Vertical - localVelocity.z / _maxSpeed/maxSpeedMulti))) * _speed*speedMulti);
            print(ri.velocity.magnitude);
        }
        else
        {
            _isGrounded = false;
            ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal - localVelocity.x / 1.8f / _maxSpeed, 0, input.Vertical - localVelocity.z /1.2f/ _maxSpeed))) * _speed);
            //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _airspeed);
            ri.AddForce(gravityDir * -9);
        }
    }

    private void Update()
    {
        //transform.Rotate(new Vector3(0,Input.GetAxisRaw("Mouse X"), 0));
        if (Input.GetKeyDown(KeyCode.Space)&&(_isGrounded||_isPainted))
        {
            ri.AddForce(transform.up * jumpPower);
        }
    }

}
