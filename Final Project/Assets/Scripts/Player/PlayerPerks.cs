using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerPerks : MonoBehaviour
{
    private PlayerStats _stats;
    private PlayerShootingHandler _shootingHandler;
    private Weapon _weapon;    
    private float _perksTime = 5;

    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
        _weapon = GetComponent<Weapon>();
        _shootingHandler = GetComponent<PlayerShootingHandler>();
    }

    public enum Perks
    {
        Heal,
        Explosion,
        Gun,
        Pistol,
        Shotgun,
        Rifle,
        Freeze,
        GodMode,
        NoReloading,        
        SlowDown,
        RandomWeapon
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Heal>(out Heal heal);
        if (heal != null)
        {
            _stats.HealthSystem.ApplyHeal(30);
            Destroy(collision.gameObject);
            Instantiate(AssetManager.Instance.PerkHealthPrefab, transform.position, Quaternion.identity);
        }

        collision.TryGetComponent<Explosion>(out Explosion explosion);
        if (explosion != null)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 15);
            foreach (Collider2D enemy in enemies)
            {
                enemy.TryGetComponent<EnemyMain>(out EnemyMain enem);
                if (enem != null)
                {
                    enem.HealthSystem.ApplyDamgage(100);
                }
            }
            GameManager.Instance.CameraShake.Shake(0.5f, 0.5f);
            SoundManager.Instance.PlaySound(SoundManager.Sound.Explosion);
            Instantiate(AssetManager.Instance.BigExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }

        collision.TryGetComponent<Pistol>(out Pistol pistol);
        if (pistol != null)
        {
            _shootingHandler.WeaponChange(Weapon.WeaponTypes.Pistol);
            Destroy(collision.gameObject);
        }

        collision.TryGetComponent<Rifle>(out Rifle rifle);
        if (rifle != null)
        {
            _shootingHandler.WeaponChange(Weapon.WeaponTypes.Rifle);
            Destroy(collision.gameObject);
        }

        collision.TryGetComponent<Shotgun>(out Shotgun gun);
        if (gun != null)
        {
            _shootingHandler.WeaponChange(Weapon.WeaponTypes.Shotgun);
            Destroy(collision.gameObject);
        }

        collision.TryGetComponent<Freeze>(out Freeze freeze);
        if (freeze != null)
        {
            Instantiate(AssetManager.Instance.PerkFreezePrefab, transform.position, Quaternion.identity);
            StartCoroutine(FreezeOrSlowDownEnemies(0));
            Destroy(collision.gameObject);
        }

        collision.TryGetComponent<SlowDown>(out SlowDown slowdown);
        if (slowdown != null)
        {
            StartCoroutine(FreezeOrSlowDownEnemies(0.4f));
            Destroy(collision.gameObject);
        }

        collision.TryGetComponent<GodMode>(out GodMode godMode);
        if (godMode != null)
        {
            Instantiate(AssetManager.Instance.PerkGodModePrefab, transform.position, Quaternion.identity);
            StartCoroutine(GodMode());
            Destroy(collision.gameObject);            
        }

        collision.TryGetComponent<RandomWeapon>(out RandomWeapon randomWeapon);
        if (randomWeapon != null)
        {
            SetRandomWeapon();
            Destroy(collision.gameObject);
        }

        collision.TryGetComponent<NoReloading>(out NoReloading noReloading);
        if (noReloading != null)
        {
            StartCoroutine(PlayerMain.Instance.ShootingHandler.SetUnlimiteAmmo());
            Destroy(collision.gameObject);
        }
    }

    private Collider2D[] EnemiesAroundPlayer(float radius)
    {
        return Physics2D.OverlapCircleAll(transform.position, radius);
    }
    private void SetRandomWeapon() 
    {
        //List<Weapon.WeaponTypes> weapons = new List<Weapon.WeaponTypes>();
        //weapons = Enum.GetValues(typeof(Weapon.WeaponTypes)).Cast<Weapon.WeaponTypes>().ToList();
        _shootingHandler.WeaponChange( _weapon.GetAllWeaponTypes()[Random.Range(0, _weapon.GetAllWeaponTypes().Count)]);
    }
    private IEnumerator FreezeOrSlowDownEnemies(float coef)
    {
        foreach (var enemy in EnemiesAroundPlayer(30))
        {
            enemy.TryGetComponent<EnemyMain>(out EnemyMain enemyMain);
            if (enemyMain != null)
            {
                enemyMain.SetMovementSpeedCoefficient(coef);
            }
        }
        yield return new WaitForSeconds(_perksTime);
        foreach (var enemy in EnemiesAroundPlayer(30)) 
        {
            enemy.TryGetComponent<EnemyMain>(out EnemyMain enemyMain);
            if (enemyMain != null)
            {
                enemyMain.SetMovementSpeedCoefficient(1);
            }
        }
    }
    private IEnumerator GodMode() 
    {
        foreach (var enemy in EnemiesAroundPlayer(30))
        {
            enemy.TryGetComponent<EnemyMain>(out EnemyMain enemyMain);
            if (enemyMain != null)
            {
                enemyMain.SetDamageCoefficient(0);
            }
        }        
        yield return new WaitForSeconds(_perksTime);
        foreach (var enemy in EnemiesAroundPlayer(30))
        {
            enemy.TryGetComponent<EnemyMain>(out EnemyMain enemyMain);
            if (enemyMain != null)
            {
                enemyMain.SetDamageCoefficient(1);
            }
        }        
    }
}