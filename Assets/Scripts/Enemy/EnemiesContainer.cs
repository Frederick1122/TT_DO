using System;
using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    public Action OnEnemyDestroyedAction;

    private int _remainingEnemies;

    private void Start() => OnEnemyDestroyedAction += () => UpdateQuantityRemainingEnemies(-1);

    public void UpdateQuantityRemainingEnemies(int changes)
    {
        _remainingEnemies += changes;

        if (_remainingEnemies != 0)
            return;

        UIManager.Instance.OpenWinnerMenu();
    }
}
