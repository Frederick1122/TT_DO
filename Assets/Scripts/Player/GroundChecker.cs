using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GroundChecker : BaseMonoBehaviour
{
    public Action CheckingPossibilityOfJumpAction;

    private void OnCollisionEnter2D(Collision2D other) => CheckingPossibilityOfJumpAction?.Invoke();
}
