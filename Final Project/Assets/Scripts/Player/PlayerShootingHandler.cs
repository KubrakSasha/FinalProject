using System.Collections;
using UnityEngine;

public class PlayerShootingHandler : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _muzlePrefab;
     private float _shotForce = 50;
     private float _timeBetweenShoot = 0.2f;
     private float _reloadtime = 2.0f;
     private int _maxAmmo = 30;
    private int _currentAmmo;
    private bool _isReloading = false;

    private float _timer;

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
            _timer += Time.fixedDeltaTime;
            if (_timer > _timeBetweenShoot && _isReloading == false)
            {
                GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);//◊≈√Œ ¡Œ ŒÃ
                GameObject muzle = Instantiate(_muzlePrefab, _shootPoint.position, _shootPoint.rotation);
                Destroy(muzle, 0.1f);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(_shootPoint.up * _shotForce, ForceMode2D.Impulse);
                GameManager.Instance.CameraShake.Shake(0.1f, 0.1f);
                _timer = 0;
                _currentAmmo--;
            }         
            
        }
    }
    

}
