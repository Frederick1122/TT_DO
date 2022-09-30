using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInputSystem))] 
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : BaseMonoBehaviour
{
    [Header("PlayerController settings")]
    [SerializeField] private float _maxSpeed = 7;
    [SerializeField] private float _jumpTakeOffSpeed = 7;

    [Header("Autofill fields")]
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInputSystem _playerInputSystem;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GroundChecker _groundChecker;

    public bool _controlEnabled = true;
    private Vector2 _movement;

    private bool _isGrounded;

    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();

        if (_boxCollider2D.IsNullOrDefault())
            _boxCollider2D = GetComponent<BoxCollider2D>();

        if (_spriteRenderer.IsNullOrDefault())
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_animator.IsNullOrDefault())
            _animator = GetComponent<Animator>();

        if (_rigidbody2D.IsNullOrDefault())
            _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_playerInputSystem.IsNullOrDefault())
            _playerInputSystem = GetComponent<PlayerInputSystem>();
        
        if (_groundChecker.IsNullOrDefault())
            _groundChecker = GetComponentInChildren<GroundChecker>();
    }

    private void Start() => _groundChecker.CheckingPossibilityOfJumpAction += UpdatePossibilityOfJump;

    protected void Update()
    {
        if (_controlEnabled)
        {
            _movement.x = Mathf.Clamp(_movement.x + _playerInputSystem.MovingHorizontal(), -_maxSpeed, _maxSpeed);

            if (_movement.x > _rigidbody2D.angularDrag)
                _movement.x -= _rigidbody2D.angularDrag;
            else if (_movement.x < -_rigidbody2D.angularDrag)
                _movement.x += _rigidbody2D.angularDrag;
            else
                _movement.x = 0;
        }
        else
            _movement.x = 0;
        
        ComputeVelocity();
    }
    

    private void ComputeVelocity()
    {
        if (_isGrounded && _playerInputSystem.IsPressJump())
            _rigidbody2D.velocity = Vector2.up * _jumpTakeOffSpeed;

        if (_movement.x > 0.01f)
            transform.rotation = Quaternion.Euler(0,0,0);
        else if (_movement.x < -0.01f)
            transform.rotation = Quaternion.Euler(0,180,0);

        _rigidbody2D.velocity = new Vector2(_movement.x, _rigidbody2D.velocity.y);
    }

    private void UpdatePossibilityOfJump()
    {
        _isGrounded = true;
    }
}