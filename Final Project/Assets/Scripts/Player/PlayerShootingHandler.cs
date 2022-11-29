using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingHandler : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shotForce;
    [SerializeField] float timeBetweenShoot = 3f;
    float timer;


    void FixedUpdate()
    {
        Shoot();
    }
    private void Shoot()
    {
        
        if (Input.GetButton("Fire1"))
        {
            timer += Time.fixedDeltaTime;// Чет хз не работает
            if (timer > timeBetweenShoot)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(shootPoint.up * shotForce, ForceMode2D.Impulse);
                timer = 0;
            }
            
            
        }
    }

}
