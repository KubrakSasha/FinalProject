using UnityEngine;

public class ShootingEnemy : EnemyMain
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _reloadTime = 2.0f;
    private float _shootingDistance = 4;
    private float _shootingForce = 8;
    private float _reloadTimer;

    private void Update()
    {
        base.Update();
        if (_isAlive)
        {
            if (Vector2.Distance(transform.position, _player.position) < _shootingDistance)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        _reloadTimer += Time.deltaTime;
        if (_reloadTime < _reloadTimer)
        {
            _animator.SetTrigger("Shoot");
            var bullet = Instantiate(AssetManager.Instance.EnemyBulletPrefab, _shootPoint.position, _shootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(_shootPoint.right * _shootingForce, ForceMode2D.Impulse);
            _reloadTimer = 0;
            _animator.SetTrigger("Stop");

        }
    }
}