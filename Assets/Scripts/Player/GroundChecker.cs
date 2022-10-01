using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class GroundChecker : BaseMonoBehaviour
{
    private List<Ground> _contacts = new List<Ground>();

    public Action CheckingPossibilityOfJumpAction;
    public Action DropCheckAction;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var ground = other.gameObject.GetComponent<Ground>();
        
        if(ground.IsNullOrDefault())
            return;
        
        _contacts.Add(ground);
        
        CheckingPossibilityOfJumpAction?.Invoke();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var ground = other.gameObject.GetComponent<Ground>();

        if (ground.IsNullOrDefault())
            return;

        _contacts.Remove(ground);

        if (_contacts.Count == 0)
            DropCheckAction?.Invoke();
    }
}
