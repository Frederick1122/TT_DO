using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KinematicObject : BaseMonoBehaviour
{
    [Header("KinematicObject Settings")]
    
    [SerializeField] protected float _minGroundNormalY = .65f;
    [SerializeField] protected float _gravityModifier = 1f;

    [Header("Autofill fields")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
   
    protected Vector2 _velocity; 

    protected Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];

    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;

    protected bool IsGrounded { get; private set; }

    protected override void OnEditorValidate()
    {
        if (_rigidbody2D.IsNullOrDefault())
        {
            _rigidbody2D = GetComponent<Rigidbody2D>(); 
            _rigidbody2D.isKinematic = true;
        }
    }

    protected virtual void Update()
    {
        _targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    protected virtual void FixedUpdate()
    {
        if (_velocity.y < 0)
            _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        else
            _velocity += Physics2D.gravity * Time.deltaTime;

        _velocity.x = _targetVelocity.x;

        IsGrounded = false;

        var deltaPosition = _velocity * Time.deltaTime;

        var moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);

        var move = moveAlongGround * deltaPosition.x;

        PerformMovement(move, false);

        move = Vector2.up * deltaPosition.y;

        PerformMovement(move, true);
    }

    void PerformMovement(Vector2 move, bool yMovement)
    {
        var distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            var count = _rigidbody2D.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            for (var i = 0; i < count; i++)
            {
                var currentNormal = _hitBuffer[i].normal;

                if (currentNormal.y > _minGroundNormalY)
                {
                    IsGrounded = true;

                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                if (IsGrounded)
                {
                    var projection = Vector2.Dot(_velocity, currentNormal);

                    if (projection < 0)
                        _velocity -= projection * currentNormal;
                }
                else
                {
                    _velocity.x *= 0;
                    _velocity.y = Mathf.Min(_velocity.y, 0);
                }

                var modifiedDistance = _hitBuffer[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidbody2D.position += move.normalized * distance;
    }

}
