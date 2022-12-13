using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMain : MonoBehaviour
{
    AudioSource _audioSource;

    public static event Action OnEnemyDied;

    public GameObject DeathPrefab;
    protected Transform _player;

    public HealthSystem _healthSystem;
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] protected float _movementSpeed = 3;
    [SerializeField] protected int _damage;
    public int Damage => _damage;
    public bool IsExplosive;

    private void Awake()
    {
        _player = PlayerMain.Instance.GetComponent<Transform>();

    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();        
        _healthSystem = new HealthSystem(_maxHealth);        
        _healthSystem.OnDead += HealthSystem_OnDead;
    }
    protected void Update()
    {
        FollowForPlayer();
    }

    private void HealthSystem_OnDead()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Instantiate(DeathPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        OnEnemyDied?.Invoke();
        _healthSystem.OnDead -= HealthSystem_OnDead;
        //SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
    }
    void FollowForPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _movementSpeed * Time.deltaTime);
        Vector2 directionToPlayer = (_player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }    
}
