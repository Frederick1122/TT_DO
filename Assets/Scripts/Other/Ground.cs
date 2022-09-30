using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlatformEffector2D))]
public class Ground : BaseMonoBehaviour
{
    [Header("Autofill fields")]
    [SerializeField] private BoxCollider2D _boxCollider2D;
    
    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();
        
        FillInField(ref _boxCollider2D);
    }

    private void Start() => _boxCollider2D.usedByComposite = true;
}
