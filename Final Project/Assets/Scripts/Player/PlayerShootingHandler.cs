using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShootingHandler : MonoBehaviour
{
    public event Action OnAmmoChanged;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _muzlePrefab;
    //private float _shotForce = 50;
    //private float _timeBetweenShoot = 0.3f;
    //private float _reloadtime = 2.0f;
    //private int _maxAmmo = 10;
    private float _currentAmmo;
    public float CurrentAmmo => _currentAmmo;

    private bool _isReloading = false;
    public Weapon weapon;

    private float _timer;

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        weapon.WeaponType = Weapon.WeaponTypes.Pistol;
        _currentAmmo = weapon.GetMaxAmmo();
    }

    void Update()
    {
        Shoot();
    }
    public IEnumerator Reload()
    {
        _isReloading = true;

        yield return new WaitForSeconds(weapon.GetTimeReloadTime());
        //SoundManager.Instance.PlaySound(SoundManager.Sound.PistolReloading);

        _currentAmmo = weapon.GetMaxAmmo();
        _isReloading = false;
    }
    public void WeaponChange(Weapon.WeaponTypes types) 
    { 
        weapon.WeaponType = types;
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
            _timer += Time.deltaTime;
            if (_timer > weapon.GetTimeBetweenShoot() && _isReloading == false)
            {
                if (weapon.WeaponType == Weapon.WeaponTypes.Shotgun) 
                {
                    int shotgunShells = 4;
                    for (int i = 0; i < shotgunShells; i++)
                    {
                        GameObject bulletS = Instantiate(_bulletPrefab, _shootPoint.position +
                        new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(-3f, 3f), _shootPoint.rotation);
                        Rigidbody2D rb1 = bulletS.GetComponent<Rigidbody2D>();
                        rb1.AddForce(_shootPoint.up * weapon.GetShootForce(), ForceMode2D.Impulse);
                    }     
                    
                }
                else
                {
                    GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);//◊≈√Œ ¡Œ ŒÃ
                    GameObject muzle = Instantiate(_muzlePrefab, _shootPoint.position, _shootPoint.rotation);
                    Destroy(muzle, 0.1f);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(_shootPoint.up * weapon.GetShootForce(), ForceMode2D.Impulse);
                    SoundManager.Instance.PlaySound(SoundManager.Sound.PistolShot);
                    GameManager.Instance.CameraShake.Shake(0.1f, 0.1f);
                }
                
                _timer = 0;
                _currentAmmo--;
                OnAmmoChanged?.Invoke();
            }

        }
    }
    public IEnumerator SetUnlimiteAmmo() 
    {
        float temp = _currentAmmo;
        _currentAmmo = 999;
        yield return new WaitForSeconds(5);
        _currentAmmo = temp;
    }
    //public float GetCurrentAmmo() 
    //{
    //    return _currentAmmo;
    //} 



}
