using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Fireball : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    [SerializeField] private float _speed;
    private void Start() => StartCoroutine(LifeRoutine());
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var potentialEnemy = other.gameObject.GetComponent<Enemy>();
        var potentialWall = other.gameObject.GetComponent<Ground>();

        if (!potentialEnemy.IsNullOrDefault())
        {
            potentialEnemy.Death();
            WinnerMenu.Instance.UpdateQuantityRemainingEnemies(-1);
        }
        
        if(!potentialEnemy.IsNullOrDefault() || !potentialWall.IsNullOrDefault())
            Death();
    }

    private void Update()
    {
        var target = transform.position + transform.right * 10f;
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        Death();
    }
    
    private void Death()
    {
        Destroy(gameObject);
    }
}
