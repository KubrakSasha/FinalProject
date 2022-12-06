using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyMain
{
    private float _shootingDistance = 10;
    private float _shootingForce = 10;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] protected float reloadTime = 2.0f;    
    protected float reloadTimer;

    private void Update() 
    {
        base.Update();
        if (Vector2.Distance(transform.position, _player.position) < _shootingDistance)
            Shoot();
    }

    private void Shoot()
    {
        //if (Vector2.Distance(transform.position, _player.position) < _shootingDistance)
        {
            //_movementSpeed = 0;
            reloadTimer += Time.deltaTime;
            if (reloadTime < reloadTimer)
            {
                var bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(_shootPoint.right * _shootingForce, ForceMode2D.Impulse);
                reloadTimer = 0;
            }
        }
    }
}
