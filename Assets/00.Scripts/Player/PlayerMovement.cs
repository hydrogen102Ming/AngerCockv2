
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody ri;
    public static PlayerMovement plmv;
    public Transform Cam;
    public Transform col;
    private CameraManager _camMana;

    [SerializeField]
    private float _speed = 5f, _maxSpeed = 8,_distance=0.60001f,_jumpPower=55f,_radius=0.2f,_paintMaxspeed=2f,_paintSpeed=0.5f,_airspeed,_customGravity;
    [SerializeField]
    private LayerMask _groundLa,_paintLa;
    private PlayerInput _input;
    private Vector3 _localVelocity,_plup,_moveVector;
    bool _isGrounded = false,_isPainted = false;
    private RaycastHit _hit;
    private float _height,_speedMulti=1f,_maxSpeedMulti=1f;
    private Collider[] _Paints = new Collider[1];
    public Vector3 gravityDir;
    public Animator anime;

    // Start is called before the first frame update
    void Awake()
    {

        //일단 여기에 둠 ㅎㅎ   

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


        if (Physics.OverlapSphereNonAlloc(transform.position-transform.up*-0.5f,1f,_Paints,_paintLa) > 0)
        {
            _isPainted = true;
            //_isGrounded = true;
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
        _moveVector = transform.TransformVector(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized;

        if (Physics.SphereCast(transform.position, _radius, -gravityDir, out _hit, _distance, _groundLa))
        {
            _isGrounded = true;
            _moveVector = _moveVector - (ri.velocity/_maxSpeed/_maxSpeedMulti);
            _moveVector = Vector3.ProjectOnPlane(_moveVector.normalized, _hit.normal) * _speed*_speedMulti;
           // ri.AddForce((transform.TransformVector(new Vector3(_input.Horizontal - _localVelocity.x / 1.3f / _maxSpeed/_maxSpeedMulti, 0, _input.Vertical - _localVelocity.z / _maxSpeed/_maxSpeedMulti))) * _speed*_speedMulti);
           // print(ri.velocity.magnitude);
        }
        else
        {
            _isGrounded = false;
            _moveVector = (_moveVector - (ri.velocity / _maxSpeed/4/_maxSpeedMulti))*_speed*_speedMulti/2;

            //_moveVector =((transform.TransformVector(new Vector3(_input.Horizontal - _localVelocity.x / 1.8f / _maxSpeed, 0, _input.Vertical - _localVelocity.z /1.2f/ _maxSpeed))) * _speed);
            //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _airspeed);
            ri.AddForce(gravityDir.normalized * _customGravity);
        }

        ri.AddForce(_moveVector);

        anime.SetBool("Walk", _isGrounded && (Input.GetButton("Horizontal") || Input.GetButton("Vertical")));
        anime.SetBool("Slide", _isPainted&&(Input.GetButton("Horizontal") || Input.GetButton("Vertical")));
        anime.SetFloat("X",_localVelocity.x / 25);
        anime.SetFloat("Y", _localVelocity.y / 25+0.5f);
        anime.SetFloat("Z", _localVelocity.z / 25);
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //일단 여기둠 ㅈㅅ ㅎㅎ
        //transform.Rotate(new Vector3(0,Input.GetAxisRaw("Mouse X"), 0));
        if (Input.GetKeyDown(KeyCode.Space)&&(_isGrounded))
        {
                ri.AddForce((Vector3.up+gravityDir*5).normalized * _jumpPower*1.5f,ForceMode.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            //ri.velocity = transform.TransformVector(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized * 15;
        }
    }

}
