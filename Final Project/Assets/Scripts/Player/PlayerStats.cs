using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //public event Action OnExplosionBulletActive;
    private Weapon _weapon;
    public PlayerHealthBar healthBar;
    public PlayerExpirienceBar expirienceBar;

    public HealthSystem healthSystem;
    public LevelSystem levelSystem;

    //public GameObject BigExplosionPrefab;

    public PlayerSkills playerSkills;//
    public UISkills uiSkills;//

    private bool _isBulletExplosive = false;
    private bool _isBulletPoison = false;

    private float _maxHealth = 100;
    private float _damage = 20;
    private float _speedMovement = 4;
    private float _expiriencePerKill = 4;   


    private float _damageMultiply = 1;
    private float _speedMovementMultiply = 1;
    private float _expiriencePerKillMultiply = 1;
    private float _maxHealthMultiply = 1;
    private float _dropChanceMultiply = 1;

    public float DropChanceMultyply => _dropChanceMultiply;    
    public float Damage => _damage * _damageMultiply;
    public float SpeedMovement => _speedMovement * _speedMovementMultiply;
    public float ExpiriencePerKill => _expiriencePerKill * _expiriencePerKillMultiply;
    public float MaxHealth => _maxHealth * _maxHealthMultiply;
    public bool IsBulletExplosive => _isBulletExplosive;
    public bool IsBulletPoison => _isBulletPoison;
    void Awake()// На старте выкидывало ошибку
    {
        healthSystem = new HealthSystem(_maxHealth);
        healthBar.Setup(healthSystem);
        healthSystem.OnDead += HealthSystem_OnDead;

        levelSystem = new LevelSystem();
        expirienceBar.Setup(levelSystem);
        levelSystem.OnLevelChanged += LevelSystemOnLevelChanged;
        EnemyMain.OnEnemyDied += EnemyMain_OnEnemyDied;

        playerSkills = new PlayerSkills();
        uiSkills.SetPlayerSkills(playerSkills);
        playerSkills.OnSkillActivate += PlayerSkills_OnSkillUnlocked;

        _weapon = GetComponent<Weapon>();
    }

    private void LevelSystemOnLevelChanged()
    {        
        GameManager.Instance.UpdateGameStates(GameStates.SkillSelection);
    }
   

    private void PlayerSkills_OnSkillUnlocked(PlayerSkills.SkillType type)//
    {
        switch (type)
        {
            case (PlayerSkills.SkillType.LongDistanceRunner):
                SetMovementSpeedCoefficient(1.2f);                
                break;
            case (PlayerSkills.SkillType.FastShoot): 
                _weapon.SetTimeBetweenShootCoefficient(0.8f);
                break;
            case (PlayerSkills.SkillType.FastLoader):
                _weapon.SetTimeReloadCoefficient(0.7f);
                break;
            case (PlayerSkills.SkillType.AmmoManiac):
                _weapon.SetMaxAmmoCoefficient(1.2f);
                break;                
            case (PlayerSkills.SkillType.X2Damage):
                SetDamageCoefficient(1.3f);
                break;
            case (PlayerSkills.SkillType.X2Expirience):
                SetExpiriencePerKillCoefficient(1.4f);
                break;
            case (PlayerSkills.SkillType.MaxHealth):
                SetMaxHealthCoefficient(1.3f);
                break;
            case (PlayerSkills.SkillType.Lucky):
                SetDropChanceCoefficient(1.2f);
                break;
            case (PlayerSkills.SkillType.ExplosionBullet):
                SetExplosionBulletActive();
                //OnExplosionBulletActive?.Invoke();
                break;
            case (PlayerSkills.SkillType.PoisonBullet):
                SetPoisonBulletActive();//
                break;
            default:
                break;
        }

    }
    private void SetMovementSpeedCoefficient(float amount) 
    {
        _speedMovementMultiply = amount;
    }
    private void SetDamageCoefficient(float amount)
    {
        _damageMultiply = amount;
    }
    private void SetMaxHealthCoefficient(float amount)
    {
        healthSystem.SetMaxHealth(amount);        
    }
    private void SetExpiriencePerKillCoefficient(float amount)
    {
        _expiriencePerKillMultiply = amount;
    }
    private void SetDropChanceCoefficient(float amount) 
    {
        _dropChanceMultiply = amount;
    }
    private void SetExplosionBulletActive() 
    {
        _isBulletExplosive = true;
    }
    private void SetPoisonBulletActive() 
    {
        _isBulletPoison = true;
    }



    private void EnemyMain_OnEnemyDied()
    {
        //EnemyMain.OnEnemyDied -= EnemyMain_OnEnemyDied;
        levelSystem.AddExpirience(ExpiriencePerKill);
        
    }

    private void HealthSystem_OnDead()
    {
        healthSystem.OnDead -= HealthSystem_OnDead;
        levelSystem.OnLevelChanged -= LevelSystemOnLevelChanged;

        
        EnemyMain.OnEnemyDied -= EnemyMain_OnEnemyDied;
        playerSkills.OnSkillActivate -= PlayerSkills_OnSkillUnlocked;


        GameManager.Instance.UpdateGameStates(GameStates.Dead);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.TryGetComponent<EnemyMain>(out EnemyMain enemy);// Как сравнивать типы наследников
        if (enemy != null)
        {
            healthSystem.ApplyDamgage(enemy.Damage);
            if (enemy.IsExplosive)
            {
                GameObject explosion1 = Instantiate(AssetManager.Instance.BigExplosionPrefab, enemy.transform.position, Quaternion.identity) as GameObject;
                SoundManager.Instance.PlaySound(SoundManager.Sound.Explosion);
                GameManager.Instance.CameraShake.Shake(0.5f, 0.5f);
                Destroy(enemy.gameObject);
            }
        }
        collision.collider.TryGetComponent<EnemyBullet>(out EnemyBullet enemyBullet);
        if (enemyBullet != null)
        {
            healthSystem.ApplyDamgage(enemyBullet.Damage);
        }
    }    
}
