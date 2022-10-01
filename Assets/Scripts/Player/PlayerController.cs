using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : BaseMonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float _maxSpeed = 7;
    [SerializeField] private float _jumpTakeOffSpeed = 7;
    [SerializeField] private float _dragModifier = 3f;
    
    [Header("Combat settings")] [SerializeField]
    private Fireball _fireballPrefab;

    [SerializeField] private GameObject _spawnPoint;
    
    [Header("Autofill fields")]
    [SerializeField] private PolygonCollider2D _polygonCollider2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private AttackZone _attackZone;

    private PlayerInputSystem _playerInputSystem;
    public bool _controlEnabled = true;
    private Vector2 _movement;

    private bool _isGrounded;
    private static readonly int Attacking1 = Animator.StringToHash("Attacking1");
    private static readonly int Attacking2 = Animator.StringToHash("Attacking2");
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Falling = Animator.StringToHash("Falling");

    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();

        FillInField(ref _polygonCollider2D);
        FillInField(ref _animator);
        FillInField(ref _rigidbody2D);

        if (_groundChecker.IsNullOrDefault())
            _groundChecker = GetComponentInChildren<GroundChecker>();
        
        if (_attackZone.IsNullOrDefault())
            _attackZone = GetComponentInChildren<AttackZone>();
    }

    private void Start()
    {
        _playerInputSystem = PlayerInputSystem.Instance;
        _groundChecker.CheckingPossibilityOfJumpAction += UpdatePossibilityOfJump;
        _groundChecker.DropCheckAction += HandleFalls;
    }

    protected void Update()
    {
        if (_controlEnabled)
        {
            
            var movingHorizontal = _playerInputSystem.GetPIS().MovingHorizontal();
            
            _movement.x = Mathf.Clamp(_movement.x + movingHorizontal, -_maxSpeed, _maxSpeed);

            var dragModifier = 1f;

            if (movingHorizontal == 0) 
                dragModifier = _dragModifier;

            if (_isGrounded)
            {
                CheckCombatSkills();
            
                if (_movement.x > _rigidbody2D.drag)
                    _movement.x = Mathf.Clamp(_movement.x - _rigidbody2D.drag * dragModifier, 0, _maxSpeed);
                else if (_movement.x < -_rigidbody2D.drag)
                    _movement.x = Mathf.Clamp(_movement.x + _rigidbody2D.drag * dragModifier, -_maxSpeed, 0);
                else
                    _movement.x = 0;
            }
            
            _animator.SetBool(Running,movingHorizontal != 0);
        }
        else
            _movement.x = 0;
        
        ComputeVelocity();
    }
    

    private void ComputeVelocity()
    {
        if (_isGrounded && _playerInputSystem.GetPIS().IsPressJump())
        {
            _rigidbody2D.velocity = Vector2.up * _jumpTakeOffSpeed;
            _isGrounded = false;
            _animator.SetTrigger(Jumping);
        }

        if (_movement.x > 0.01f)
            transform.rotation = Quaternion.Euler(0,0,0);
        else if (_movement.x < -0.01f)
            transform.rotation = Quaternion.Euler(0,180,0);

        _rigidbody2D.velocity = new Vector2(_movement.x, _rigidbody2D.velocity.y);
    }

    private void UpdatePossibilityOfJump()
    {
        _isGrounded = true;
        _animator.SetBool(Grounded, true);
        _animator.SetBool(Falling, false);
    }

    private void HandleFalls()
    {
        _isGrounded = false;
        _animator.SetBool(Falling, true);
        _animator.SetBool(Grounded, false);
    }

    private void CheckCombatSkills()
    {
        if (_playerInputSystem.GetPIS().IsPressPunch()) 
            StartCoroutine(AttackRoutine(Attacking1,StrikeType.Punch));
        else if(_playerInputSystem.GetPIS().IsPressShot())
            StartCoroutine(AttackRoutine(Attacking2,StrikeType.Shot));
    }

    private IEnumerator AttackRoutine(int animationId, StrikeType strikeType)
    {
        _controlEnabled = false;
        _animator.SetTrigger(animationId);
        
        yield return new WaitForSeconds(0.4f);

        switch (strikeType)
        {
            case StrikeType.Punch:
                PunchHandler();
                break;
            case StrikeType.Shot:
                ShotHandler();
                break;
        }
      
        yield return new WaitForSeconds(0.4f);

        _controlEnabled = true;
    }

    private void PunchHandler()
    {
        var enemies = _attackZone.GetEnemies();
        var enemiesCount = enemies.Count;

        for (var index = 0; index < enemies.Count; index++)
            enemies[index].Death();

        WinnerMenu.Instance.UpdateQuantityRemainingEnemies(-enemiesCount);
    }

    private void ShotHandler()
    {
        var fireball = Instantiate(_fireballPrefab, _spawnPoint.transform, false);
        fireball.transform.parent = null;
    }

    private enum StrikeType
    {
        Punch,
        Shot
    }
}