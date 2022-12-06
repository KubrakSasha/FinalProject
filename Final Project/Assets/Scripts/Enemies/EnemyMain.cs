using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMain : MonoBehaviour
{

    public static event Action OnEnemyDied;

    public GameObject DeathPrefab;
    protected Transform _player;
    [SerializeField] protected float _movementSpeed = 3;
    public HealthSystem _healthSystem;
    int maxHealth = 100;
    [SerializeField] protected int _damage;
    public bool IsExplosive;

    void Start()
    {
        _player = PlayerMain.Instance.GetComponent<Transform>();

        _healthSystem = new HealthSystem(maxHealth);        
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
    }
    void FollowForPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _movementSpeed * Time.deltaTime);
        Vector2 directionToPlayer = (_player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    public int GetDamage()
    {
        return _damage;
    }
}
