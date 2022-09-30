using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
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
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInputSystem _playerInputSystem;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private AttackZone _attackZone;

    public bool _controlEnabled = true;
    private Vector2 _movement;

    private bool _isGrounded;

    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();

        FillInField(ref _boxCollider2D);
        FillInField(ref _animator);
        FillInField(ref _rigidbody2D);
        FillInField(ref _playerInputSystem);

        if (_groundChecker.IsNullOrDefault())
            _groundChecker = GetComponentInChildren<GroundChecker>();
        
        if (_attackZone.IsNullOrDefault())
            _attackZone = GetComponentInChildren<AttackZone>();
    }

    private void Start() => _groundChecker.CheckingPossibilityOfJumpAction += UpdatePossibilityOfJump;

    protected void Update()
    {
        if (_controlEnabled)
        {
            CheckCombatSkills();
            _movement.x = Mathf.Clamp(_movement.x + _playerInputSystem.GetPIS().MovingHorizontal(), -_maxSpeed, _maxSpeed);

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
        if (_isGrounded && _playerInputSystem.GetPIS().IsPressJump())
        {
            _rigidbody2D.velocity = Vector2.up * _jumpTakeOffSpeed;
            _isGrounded = false;
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
    }

    public void CheckCombatSkills()
    {
        if (_playerInputSystem.GetPIS().IsPressPunch()) 
            StartCoroutine(Punch());
    }

    IEnumerator Punch()
    {
        _controlEnabled = false;

        yield return new WaitForSeconds(0.5f);

        var enemies = _attackZone.GetEnemies();
        var enemiesCount = enemies.Count;

        for (var index = 0; index < enemies.Count; index++)
            enemies[index].Death();

        WinnerMenu.Instance.UpdateQuantityRemainingEnemies(-enemiesCount);
        
        _controlEnabled = true;
    }
}