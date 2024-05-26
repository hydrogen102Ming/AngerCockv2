using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Transform Cam;
    public Transform col;

    [SerializeField] private float _speed = 5f, _maxSpeed = 8, _jumpPower = 55f, _paintMaxspeed = 2f, _paintSpeed = 0.5f, _airspeed;
    private float _height, _speedMulti = 1f, _maxSpeedMulti = 1f;

    [Space(10)]
    [SerializeField] private float _distance = 0.6f, _radius = 0.2f;
    [SerializeField] private LayerMask _groundLa, _paintLa;      

    private Vector3 _localVelocity, _plup;
    private bool _isGrounded = false, _isPainted = false;
    private Collider[] _paints = new Collider[1];
    public Vector3 gravityDir;
    void Awake()
    {
        _distance = transform.localScale.y * 0.901f;
        _radius = _distance * 0.2f;
        _height = col.localScale.y;
    }
    public void IhateBae()
    {
        //if (Physics.OverlapSphereNonAlloc(transform.position - transform.up * -0.5f, 1f, _Paints, _paintLa) > 0)
        //{
        //    _isPainted = true;
        //    _isGrounded = true;
        //    for (int i = 0; i < _Paints.Length; i++)
        //    {
        //        gravityDir = (gravityDir + _Paints[i].transform.up) / 2;
        //    }
        //    _speedMulti = _paintSpeed;
        //    _maxSpeedMulti = _paintMaxspeed;
        //    //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _airspeed);
        //}
        //else
        //{
        //    _isPainted = false;
        //    gravityDir = Vector3.up;
        //    _speedMulti = 1;
        //    _maxSpeedMulti = 1f;
        //}

        //_localVelocity = transform.InverseTransformVector(rigidBody.velocity);
        ////ri.velocity.magnitude < _maxSpeed
        ////ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical) - localVelocity / _maxSpeed)) * _speed);
        //if (Physics.SphereCast(transform.position, _radius, -transform.up, out _hit, _distance, _groundLa) || _isPainted)
        //{
        //    _isGrounded = true;
        //    //ri.velocity = ri.velocity - Vector3.up * ri.velocity.y;
        //    //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _speed);
        //    rigidBody.AddForce((transform.TransformVector(new Vector3(_input.Horizontal - _localVelocity.x / 1.3f / _maxSpeed / _maxSpeedMulti, 0, _input.Vertical - _localVelocity.z / _maxSpeed / _maxSpeedMulti))) * _speed * _speedMulti);
        //    print(rigidBody.velocity.magnitude);
        //}
        //else
        //{
        //    _isGrounded = false;
        //    rigidBody.AddForce((transform.TransformVector(new Vector3(_input.Horizontal - _localVelocity.x / 1.8f / _maxSpeed, 0, _input.Vertical - _localVelocity.z / 1.2f / _maxSpeed))) * _speed);
        //    //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _airspeed);
        //    rigidBody.AddForce(gravityDir * -9);
        //}
    }
    public void IhateBaeRemake(float horizontal, float vertical)
    {
        if (Physics.OverlapSphereNonAlloc(transform.position - transform.up * -0.5f, 1f, _paints, _paintLa) > 0)
        {
            _isPainted = true;
            _isGrounded = true;
            for (int i = 0; i < _paints.Length; i++)
            {
                gravityDir = (gravityDir + _paints[i].transform.up) / 2;
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

        _localVelocity = transform.InverseTransformVector(rigidBody.velocity);
        //ri.velocity.magnitude < _maxSpeed
        //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical) - localVelocity / _maxSpeed)) * _speed);
        if (Physics.SphereCast(transform.position, _radius, -transform.up, out RaycastHit _hit, _distance, _groundLa) || _isPainted)
        {
            _isGrounded = true;
            //ri.velocity = ri.velocity - Vector3.up * ri.velocity.y;
            //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _speed);
            rigidBody.AddForce((transform.TransformVector(new Vector3(horizontal - _localVelocity.x / 1.3f / _maxSpeed / _maxSpeedMulti, 0, vertical - _localVelocity.z / _maxSpeed / _maxSpeedMulti))) * _speed * _speedMulti);
            print(rigidBody.velocity.magnitude);
        }
        else
        {
            _isGrounded = false;
            rigidBody.AddForce((transform.TransformVector(new Vector3(horizontal - _localVelocity.x / 1.8f / _maxSpeed, 0, vertical - _localVelocity.z / 1.2f / _maxSpeed))) * _speed);
            //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _airspeed);
            rigidBody.AddForce(gravityDir * -9);
        }
    }
    private void Jump()
    {
        rigidBody.AddForce(transform.up * _jumpPower);
    }
    private void Update()
    {
        //transform.Rotate(new Vector3(0,Input.GetAxisRaw("Mouse X"), 0));
        if (Input.GetKeyDown(KeyCode.Space)&&(_isGrounded||_isPainted))
        {
            rigidBody.AddForce(transform.up * _jumpPower);
        }
    }


}
