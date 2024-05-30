using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody ri;
    public Transform col;
    public Animator anime;

    [SerializeField]
    private float _speed = 5f, _maxSpeed = 8, _distance = 0.60001f, _jumpPower = 55f, _radius = 0.2f, _paintMaxspeed = 2f, _paintSpeed = 0.5f, _airspeed, _customGravity;
    private Vector3 _localVelocity, _plup, _moveVector;
    public Vector3 gravityDir;

    [Header("Detection Settings / Layer Masks")]
    [SerializeField] private LayerMask _groundLa;
    [SerializeField] private LayerMask _paintLa;
    private Collider[] _Paints = new Collider[1]; //legnth number 1 array

    private float _height, _speedMulti = 1f, _maxSpeedMulti = 1f;

    private bool _isGrounded = false, _isPainted = false;

    void Awake()
    {
        _distance = transform.localScale.y * 0.901f;
        _radius = _distance * 0.2f;
        _height = col.localScale.y;
    }
    public void IHateBaeRemasteredMordenWarfare4(float hor, float ver)
    {
        if (Physics.OverlapSphereNonAlloc(transform.position - transform.up * -0.3f, 1f, _Paints, _paintLa) > 0)
        {
            _isPainted = true;
            //_isGrounded = true;
            for (int i = 0; i < _Paints.Length; i++)
            {
                gravityDir = (_Paints[i].transform.up).normalized;
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
        _moveVector = transform.TransformVector(new Vector3(hor, 0, ver)).normalized;

        if (Physics.SphereCast(transform.position, _radius, -gravityDir, out RaycastHit _hit, _distance, _groundLa))
        {
            _isGrounded = true;
            _moveVector = _moveVector - (ri.velocity / _maxSpeed / _maxSpeedMulti);
            _moveVector = Vector3.ProjectOnPlane(_moveVector, _hit.normal) * _speed * _speedMulti;
            // ri.AddForce((transform.TransformVector(new Vector3(_input.Horizontal - _localVelocity.x / 1.3f / _maxSpeed/_maxSpeedMulti, 0, _input.Vertical - _localVelocity.z / _maxSpeed/_maxSpeedMulti))) * _speed*_speedMulti);
            // print(ri.velocity.magnitude);
        }
        else
        {
            _isGrounded = false;
            _moveVector = (_moveVector - (ri.velocity / _maxSpeed / 4 / _maxSpeedMulti)) * _speed * _speedMulti / 2;

            //_moveVector =((transform.TransformVector(new Vector3(_input.Horizontal - _localVelocity.x / 1.8f / _maxSpeed, 0, _input.Vertical - _localVelocity.z /1.2f/ _maxSpeed))) * _speed);
            //ri.AddForce((transform.TransformVector(new Vec    tor3(input.Horizontal, 0, input.Vertical))) * _airspeed);
            ri.AddForce(gravityDir.normalized * _customGravity);
        }

        ri.AddForce(_moveVector);

        //animation should not be setting in fixed update
        anime.SetBool("Walk", _isGrounded && (Input.GetButton("Horizontal") || Input.GetButton("Vertical")));
        anime.SetBool("Slide", _isPainted && (Input.GetButton("Horizontal") || Input.GetButton("Vertical")));
        anime.SetFloat("X", _localVelocity.x / 25);
        anime.SetFloat("Y", _localVelocity.y / 25 + 0.5f);
        anime.SetFloat("Z", _localVelocity.z / 25);
    }

    public void TryJump()
    {
        if(_isGrounded)
        {
            Jump();//ri.AddForce((Vector3.up + gravityDir * 5).normalized * _jumpPower * 1.5f, ForceMode.Impulse);
        }
    }
    private void Jump()
    {
        ri.AddForce((Vector3.up + gravityDir * 5).normalized * _jumpPower * 1.5f, ForceMode.Impulse);
    }
    #region fuck
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
        //if (Physics.OverlapSphereNonAlloc(transform.position - transform.up * -0.5f, 1f, _paints, _paintLa) > 0)
        //{
        //    _isPainted = true;
        //    _isGrounded = true;
        //    for (int i = 0; i < _paints.Length; i++)
        //    {
        //        gravityDir = (gravityDir + _paints[i].transform.up) / 2;
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
        //if (Physics.SphereCast(transform.position, _radius, -transform.up, out RaycastHit _hit, _distance, _groundLa) || _isPainted)
        //{
        //    _isGrounded = true;
        //    //ri.velocity = ri.velocity - Vector3.up * ri.velocity.y;
        //    //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _speed);
        //    rigidBody.AddForce((transform.TransformVector(new Vector3(horizontal - _localVelocity.x / 1.3f / _maxSpeed / _maxSpeedMulti, 0, vertical - _localVelocity.z / _maxSpeed / _maxSpeedMulti))) * _speed * _speedMulti);
        //    print(rigidBody.velocity.magnitude);
        //}
        //else
        //{
        //    _isGrounded = false;
        //    rigidBody.AddForce((transform.TransformVector(new Vector3(horizontal - _localVelocity.x / 1.8f / _maxSpeed, 0, vertical - _localVelocity.z / 1.2f / _maxSpeed))) * _speed);
        //    //ri.AddForce((transform.TransformVector(new Vector3(input.Horizontal, 0, input.Vertical))) * _airspeed);
        //    rigidBody.AddForce(gravityDir * -9);
        //}
    }
    //private void Update()
    //{
    //    //transform.Rotate(new Vector3(0,Input.GetAxisRaw("Mouse X"), 0));
    //    if (Input.GetKeyDown(KeyCode.Space) && (_isGrounded))
    //    {
    //        ri.AddForce((Vector3.up + gravityDir * 5).normalized * _jumpPower * 1.5f, ForceMode.Impulse);
    //    }
    //    
    //    if (Input.GetKeyDown(KeyCode.LeftShift))
    //    {
    //        //ri.velocity = transform.TransformVector(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized * 15;
    //    }
    //}
    #endregion
}
