using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.TryGetComponent<EnemyMain>(out EnemyMain enemy);
        if (enemy != null)
        {
            enemy._healthSystem.ApplyDamgage(100);
            Destroy(gameObject);
        }
        
    }
}
