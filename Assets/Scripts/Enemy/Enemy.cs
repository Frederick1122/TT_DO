using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : BaseMonoBehaviour
{
    [HideInInspector] public Action OnEnemyDestroyedAction;
    
    [Header("Autofill fields")]
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();
        
        FillInField(ref _boxCollider2D);
        FillInField(ref _spriteRenderer);
        FillInField(ref _rigidbody2D);
    }

    private void Start()
    {
        _rigidbody2D.mass = 1000;
    }

    public void Death()
    {
        _boxCollider2D.enabled = false;
        _spriteRenderer.enabled = false;
        _rigidbody2D.isKinematic = true;
        
        OnEnemyDestroyedAction?.Invoke();
    }
}
