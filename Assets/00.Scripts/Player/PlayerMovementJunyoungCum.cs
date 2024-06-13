using UnityEngine;

public class PlayerMovementJunyoungCum : MonoBehaviour
{
    public Rigidbody rigidBody;
    //public Collider col;
    //public Animator animator;

    [Header("Speed")]
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private float _paintSpeedMulti;
    [SerializeField] private float _paintMaxspeedMulti;

    [SerializeField] private float _jumpPower;

    [Space(10)]
    [SerializeField] private float _gravity;
    public Vector3 gravityDir,addMoveVector,hookdir;
    public bool hooked = false;

    private bool _isGrounded = false;
    private bool _isPainted = false;

    [Header("Detection Settings / Layer Masks")]
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _groundCheckDistance;
    private readonly Collider[] _arrPaints = new Collider[1]; //legnth number 1 array

    [SerializeField] private LayerMask _groundLa;
    [SerializeField] private LayerMask _paintLa;
    Vector3 right2 = new Vector3(0.99f, 0, 0.01f);
    //void Awake()
    //{
    //    _groundCheckDistance = transform.localScale.y * 0.901f;
    //    _groundCheckRadius = _groundCheckDistance * 0.2f;
    //    _height = col.localScale.y;
    //}
    public void IHateBaeRemasteredMordenWarfare4(float hor, float ver)
    {
        gravityDir = Vector3.up;
        float _speedMulti = 1;
        float _maxSpeedMulti = 1;
        void SetSpeed()
        {
            bool isOnPaint = Physics.OverlapSphereNonAlloc(transform.position + transform.up * 0.3f, 1f, _arrPaints, _paintLa) > 0;
            if (isOnPaint)
            {

                _isPainted = true;
                //_isGrounded = true;
                for (int i = 0; i < _arrPaints.Length; i++)
                {
                    gravityDir = (_arrPaints[i].transform.up).normalized;
                }
                _speedMulti = _paintSpeedMulti;
                _maxSpeedMulti = _paintMaxspeedMulti;
            }
            else
            {
                _isPainted = false;
                gravityDir = Vector3.up;
                _speedMulti = 1;
                _maxSpeedMulti = 1f;
            }
        }
        SetSpeed();
        Vector3 _localVelocity = transform.InverseTransformVector(rigidBody.velocity);
        Vector3 _moveVector = Quaternion.Euler(0, Player.Instance.mx, 0) *new Vector3(hor, 0, ver).normalized ;
        Vector3 a;
        bool isOnGround = Physics.SphereCast(transform.position, _groundCheckRadius, -gravityDir, out RaycastHit _hit, _groundCheckDistance, _groundLa);
        float speed = _speed * _speedMulti;
        float maxSpeed = _maxSpeed * _maxSpeedMulti;

        if (isOnGround)
        {
            _isGrounded = true;
                if(_hit.normal != Vector3.up)
                {
                _moveVector = Vector3.ProjectOnPlane(_moveVector, _hit.normal);
                }
            a = (rigidBody.velocity / maxSpeed);
        }
        else
        {
            _isGrounded = false;
            
            a= Vector3.Project((rigidBody.velocity / maxSpeed),_moveVector);
        }
        //rigidBody.AddForce(gravityDir.normalized * _gravity,ForceMode.Acceleration);
        _moveVector -= a.normalized * Mathf.Clamp(a.magnitude, 0, 1);
        rigidBody.AddForce((_moveVector) * speed + addMoveVector + gravityDir.normalized * _gravity);

        //animation should not be setting in fixed update
        //animator.SetBool("Walk", _isGrounded && (Input.GetButton("Horizontal") || Input.GetButton("Vertical")));
        //animator.SetBool("Slide", _isPainted && (Input.GetButton("Horizontal") || Input.GetButton("Vertical")));
        //animator.SetFloat("X", _localVelocity.x / 25);
        //animator.SetFloat("Y", _localVelocity.y / 25 + 0.5f);
        //animator.SetFloat("Z", _localVelocity.z / 25);
    }

    public void TryJump()
    {
        if (_isGrounded)
        {
            Jump();//ri.AddForce((Vector3.up + gravityDir * 5).normalized * _jumpPower * 1.5f, ForceMode.Impulse);
        }
    }
    private void Jump()
    {
        rigidBody.AddForce((Vector3.up + gravityDir * 5).normalized * _jumpPower * 1.5f, ForceMode.Impulse);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -gravityDir * _groundCheckDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position - gravityDir * _groundCheckDistance, Vector3.down * _groundCheckRadius);
        Gizmos.DrawRay(transform.position - gravityDir * _groundCheckDistance, Vector3.forward * _groundCheckRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.up * 0.3f, 1);
    }
}
