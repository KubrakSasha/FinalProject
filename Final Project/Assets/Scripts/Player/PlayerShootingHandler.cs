using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingHandler : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject muzlePrefab;
    [SerializeField] float shotForce;
    [SerializeField] float timeBetweenShoot = 3f;
    [SerializeField] private float _reloadtime = 2.0f;
    [SerializeField] private int _maxAmmo = 30;
    private int _currentAmmo;
    private bool _isReloading = false;


    float timer;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    void FixedUpdate()
    {
        Shoot();
    }
    public IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadtime);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
    }
    private void Shoot()
    {        
        if (Input.GetButton("Fire1"))
        {
            if (_currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
            timer += Time.fixedDeltaTime;
            if (timer > timeBetweenShoot && _isReloading == false)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);//◊≈√Œ ¡Œ ŒÃ
                GameObject muzle = Instantiate(muzlePrefab, shootPoint.position, shootPoint.rotation);
                Destroy(muzle, 0.1f);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(shootPoint.up * shotForce, ForceMode2D.Impulse);
                GameManager.Instance.CameraShake.Shake(0.1f, 0.1f);
                timer = 0;
                _currentAmmo--;
            }         
            
        }
    }
    

}
