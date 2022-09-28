using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerInputSystem))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : BaseMonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [Space]
    [Header("Autofill fields")]
    [SerializeField] private PlayerInputSystem _playerInputSystem;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private bool _isGrounded = true;
    
    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();
        
        if (_playerInputSystem == null)
            _playerInputSystem = GetComponent<PlayerInputSystem>();

        if (_boxCollider2D == null)
            _boxCollider2D = GetComponent<BoxCollider2D>();

        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var movement = _playerInputSystem.UpdateMovement();
        
        MovementLogic(movement.x);
        JumpLogic(movement.z);
    }


    private void MovementLogic(float xMovement)
    {
        _rigidbody2D.AddForce(Vector2.right * xMovement * _speed);
    }

    private void JumpLogic(float zMovement)
    {
        if (!_isGrounded && zMovement <= 0)
            return;

        zMovement = 1;
        
        _rigidbody2D.AddForce(Vector2.up * zMovement * _jumpSpeed, ForceMode2D.Impulse);

        _isGrounded = false;
    }
}
