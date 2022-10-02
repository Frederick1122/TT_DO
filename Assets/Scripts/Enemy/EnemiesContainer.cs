using System;
using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    private int _remainingEnemies;
    
    public void UpdateQuantityRemainingEnemies(int changes)
    {
        _remainingEnemies += changes;

        if (_remainingEnemies != 0)
            return;

        UIManager.Instance.OpenWinnerMenu();
    }
}
