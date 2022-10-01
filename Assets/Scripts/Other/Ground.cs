using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
[RequireComponent(typeof(CompositeCollider2D))]
public class Ground : BaseMonoBehaviour
{
    [Header("Autofill fields")]
    [SerializeField] private TilemapCollider2D _tilemapCollider2D;
    
    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();
        
        FillInField(ref _tilemapCollider2D);
    }

    private void Start() => _tilemapCollider2D.usedByComposite = true;
}
