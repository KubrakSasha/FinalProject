using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int _damage = 10;
    public int Damage => _damage;
    void Update()
    {
        Destroy(gameObject, 2f);
    }
    public int GetDamage()
    {
        return _damage;
    }
}
