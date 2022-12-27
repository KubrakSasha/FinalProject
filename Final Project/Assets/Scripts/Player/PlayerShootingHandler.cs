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
    private Animator _animator;   
    private float _currentAmmo;
    private bool _isReloading = false;
    private Weapon _weapon;
    private float _timer;

    public float CurrentAmmo => _currentAmmo;
    public Weapon Weapon => _weapon;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponent<Weapon>();
        _weapon.WeaponType = Weapon.WeaponTypes.Pistol;
        _currentAmmo = _weapon.GetMaxAmmo();
    }

    void Update()
    {
        Shoot();
    }
    public IEnumerator Reload()
    {
        _isReloading = true;
        _animator.SetBool("Reloading", true);
        if (!SoundManager.Instance.Get().isPlaying)
        {
            SoundManager.Instance.PlaySound(_weapon.GetReloadSound());
        }
        yield return new WaitForSeconds(_weapon.GetTimeReloadTime());        
        _currentAmmo = _weapon.GetMaxAmmo();
        OnAmmoChanged?.Invoke();
        _isReloading = false;
        _animator.SetBool("Reloading", false);
    }
    public void WeaponChange(Weapon.WeaponTypes types) 
    {
        _weapon.WeaponType = types;
        if (_weapon.WeaponType == Weapon.WeaponTypes.Pistol)
        {            
            _animator.SetBool("Pistol", true);
            _animator.SetBool("Rifle", false);
            _animator.SetBool("Shotgun", false);
        }
        
        if (_weapon.WeaponType == Weapon.WeaponTypes.Rifle)
        {
            _animator.SetBool("Pistol", false);
            _animator.SetBool("Rifle", true);
            _animator.SetBool("Shotgun", false);
        }
        if (_weapon.WeaponType == Weapon.WeaponTypes.Shotgun)
        {
            _animator.SetBool("Pistol", false);
            _animator.SetBool("Rifle", false);
            _animator.SetBool("Shotgun", true);
        }
        _currentAmmo = _weapon.GetMaxAmmo();
        OnAmmoChanged?.Invoke();
    }
    private void Shoot()
    {
        if (_currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1"))
        {            
            _timer += Time.deltaTime;
            if (_timer > _weapon.GetTimeBetweenShoot() && _isReloading == false)
            {
                if (_weapon.WeaponType == Weapon.WeaponTypes.Shotgun) 
                {
                    int shotgunShells = 4;
                    for (int i = 0; i < shotgunShells; i++)
                    {                        
                        GameObject bulletS = Instantiate(_bulletPrefab, _shootPoint.position +
                        new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(-2f, 2f), _shootPoint.rotation);
                        Rigidbody2D rb1 = bulletS.GetComponent<Rigidbody2D>();
                        rb1.AddForce(_shootPoint.up * _weapon.GetShootForce(), ForceMode2D.Impulse);
                        SoundManager.Instance.PlaySound(_weapon.GetShotSound());                        
                    }           
                }
                else
                {                    
                    GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
                    GameObject muzle = Instantiate(_muzlePrefab, _shootPoint.position, _shootPoint.rotation);
                    Destroy(muzle, 0.1f);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(_shootPoint.up * _weapon.GetShootForce(), ForceMode2D.Impulse);
                    SoundManager.Instance.PlaySound(_weapon.GetShotSound());                                      
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
        OnAmmoChanged?.Invoke();
        yield return new WaitForSeconds(5);
        _currentAmmo = temp;
        OnAmmoChanged?.Invoke();
    }
}