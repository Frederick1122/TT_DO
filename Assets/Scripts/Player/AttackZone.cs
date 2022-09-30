using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AttackZone : MonoBehaviour
{
   private List<Enemy> _enemies = new List<Enemy>();

   private void OnTriggerEnter2D(Collider2D other)
   {
      var potentialEnemy = other.gameObject.GetComponent<Enemy>(); 
      
      if(!potentialEnemy.IsNullOrDefault())
         _enemies.Add(potentialEnemy);
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      var potentialEnemy = other.gameObject.GetComponent<Enemy>(); 
      
      if(!potentialEnemy.IsNullOrDefault())
         _enemies.Remove(potentialEnemy);
   }

   public List<Enemy> GetEnemies() => _enemies;
}
