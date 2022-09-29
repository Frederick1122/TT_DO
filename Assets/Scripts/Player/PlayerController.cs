using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInputSystem))]
public class PlayerController : KinematicObject
{
    [Header("PlayerController settings")]
    [SerializeField] private float _maxSpeed = 7;
    [SerializeField] private float _jumpTakeOffSpeed = 7;
    [SerializeField] private JumpState _jumpState = JumpState.Grounded;

    [Header("Autofill fields")]
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInputSystem _playerInputSystem;

    public bool _controlEnabled = true;
    private bool _stopJump;
    private bool _jump;
    private Vector2 _move;

    public Bounds Bounds => _boxCollider2D.bounds;

    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();

        if (_boxCollider2D.IsNullOrDefault())
            _boxCollider2D = GetComponent<BoxCollider2D>();

        if (_spriteRenderer.IsNullOrDefault())
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_animator.IsNullOrDefault())
            _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (_controlEnabled)
        {
            _move.x = Input.GetAxis("Horizontal");
            
            if (_jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                _jumpState = JumpState.PrepareToJump;
            else if (Input.GetButtonUp("Jump")) 
                _stopJump = true;
        }
        else
            _move.x = 0;

        UpdateJumpState();
        base.Update();
    }

    void UpdateJumpState()
    {
        _jump = false;
        switch (_jumpState)
        {
            case JumpState.PrepareToJump:
                _jumpState = JumpState.Jumping;
                _jump = true;
                _stopJump = false;
                break;
            case JumpState.Jumping:
                if (!IsGrounded)
                    _jumpState = JumpState.InFlight;
                break;
            case JumpState.InFlight:
                if (IsGrounded)
                    _jumpState = JumpState.Landed;
                break;
            case JumpState.Landed:
                _jumpState = JumpState.Grounded;
                break;
        }
    }

    protected override void ComputeVelocity()
    {
        if (_jump && IsGrounded)
        {
            _velocity.y = _jumpTakeOffSpeed * 1.5f;
            _jump = false;
        }
        else if (_stopJump)
        {
            _stopJump = false;
            if (_velocity.y > 0) 
                _velocity.y *= 0.5f;
        }

        if (_move.x > 0.01f)
            _spriteRenderer.flipX = false;
        else if (_move.x < -0.01f)
            _spriteRenderer.flipX = true;

        //_animator.SetBool("grounded", IsGrounded);
        //_animator.SetFloat("velocityX", Mathf.Abs(_velocity.x) / _maxSpeed);

        _targetVelocity = _move * _maxSpeed;
    }

    public enum JumpState
    {
        Grounded,
        PrepareToJump,
        Jumping,
        InFlight,
        Landed
    }
}