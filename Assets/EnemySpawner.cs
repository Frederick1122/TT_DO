using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs = new List<Enemy>();
    
    [Range(0,100)]
    [SerializeField] private int _percentageOfPossibleEnemies;

    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();

    private void Start() => Spawn();

    private void Spawn()
    {
        var enemyQuantity = (int) ((float) _spawnPoints.Count * ((float) _percentageOfPossibleEnemies / 100));
        
        for (var i = 0; i < enemyQuantity; i++)
        {
            var spawnPoint = _spawnPoints.GetRandom();
            var enemyPrefab = _enemyPrefabs.GetRandom();

            Instantiate(enemyPrefab, spawnPoint.transform.position, quaternion.identity);

            _spawnPoints.Remove(spawnPoint);
            _enemyPrefabs.Remove(enemyPrefab);
        }
        
        WinnerMenu.Instance.UpdateQuantityRemainingEnemies(enemyQuantity);
    }
}
