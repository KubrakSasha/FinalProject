using System;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    public static event Action OnEnemyDied;
    public bool IsExplosive;
    protected bool _isAlive = true;

    protected Transform _player;
    protected Animator _animator;
    protected GameObject _minimapIcon;

    protected HealthSystem _healthSystem;
    protected AudioSource _audioSource;
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] protected float _movementSpeed = 3;
    [SerializeField] protected int _damage;
    protected float _movementSpeedMultiply = 1;
    protected float _damageMultiplier = 1;

    public HealthSystem HealthSystem => _healthSystem;
    public float Damage => _damage * _damageMultiplier;
    public float MovementSpeed => _movementSpeed * _movementSpeedMultiply;    

    private void Awake()
    {
        _minimapIcon = GameObject.Find ("MinimapIcon");// Переделать 
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _player = PlayerMain.Instance.GetComponent<Transform>();
    }
    void Start()
    {                
        _healthSystem = new HealthSystem(_maxHealth);        
        _healthSystem.OnDead += HealthSystem_OnDead;
    }
    protected void Update()
    {
        
        if (_isAlive)
        FollowForPlayer();
    }

    private void HealthSystem_OnDead()
    {
        Destroy(_minimapIcon);
        _animator.SetBool("Death", true);
        _isAlive = false;
        _audioSource.mute = true;
        if (IsExplosive)
        {
            Instantiate(AssetManager.Instance.BigExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        Instantiate(AssetManager.Instance.DeathPrefab, transform.position, Quaternion.identity);
        //_movementSpeed = 0;        
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject, 20f);
        Instantiate(AssetManager.Instance.BloodPrefab, transform.position, Quaternion.identity);
        OnEnemyDied?.Invoke();
        _healthSystem.OnDead -= HealthSystem_OnDead;        
    }
    void FollowForPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, MovementSpeed * Time.deltaTime);
        Vector2 directionToPlayer = (_player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void SetMovementSpeedCoefficient(float amount)
    {
        _movementSpeedMultiply = amount;
    }
    public void SetDamageCoefficient(float amount)
    {
        _damageMultiplier = amount;
    }
}