using System.Collections;
using UnityEngine;

public class PlayerShootingHandler : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _muzlePrefab;
    //private float _shotForce = 50;
    //private float _timeBetweenShoot = 0.3f;
    //private float _reloadtime = 2.0f;
    //private int _maxAmmo = 10;
    private float _currentAmmo;
    private bool _isReloading = false;
    public Weapon weapon;

    private float _timer;

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        weapon.WeaponType = Weapon.WeaponTypes.Pistol;
        _currentAmmo = weapon.GetMaxAmmo(weapon.WeaponType);
    }

    void FixedUpdate()
    {
        Shoot();
    }
    public IEnumerator Reload()
    {
        _isReloading = true;

        yield return new WaitForSeconds(weapon.GetTimeReloadTime(weapon.WeaponType));
        //SoundManager.Instance.PlaySound(SoundManager.Sound.PistolReloading);

        _currentAmmo = weapon.GetMaxAmmo(weapon.WeaponType);
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
            _timer += Time.fixedDeltaTime;
            if (_timer > weapon.GetTimeBetweenShoot(weapon.WeaponType) && _isReloading == false)
            {
                GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);//◊≈√Œ ¡Œ ŒÃ
                GameObject muzle = Instantiate(_muzlePrefab, _shootPoint.position, _shootPoint.rotation);
                Destroy(muzle, 0.1f);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(_shootPoint.up * weapon.GetShootForce(weapon.WeaponType), ForceMode2D.Impulse);
                SoundManager.Instance.PlaySound(SoundManager.Sound.PistolShot);
                GameManager.Instance.CameraShake.Shake(0.1f, 0.1f);
                _timer = 0;
                _currentAmmo--;
            }

        }
    }


}
