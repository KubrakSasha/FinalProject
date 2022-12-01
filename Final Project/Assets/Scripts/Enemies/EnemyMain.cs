using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMain : MonoBehaviour
{
    public static event Action OnEnemyDied;

    [SerializeField] Transform player;
    [SerializeField] float movementSpeed = 3;
    public HealthSystem healthSystem;
    int maxHealth = 100;
    int damage = 10;
    //LevelSystem lsplayer;


    void Start()
    {
        healthSystem = new HealthSystem(maxHealth);
        player = FindObjectOfType<PlayerMain>().transform;
        healthSystem.OnDead += HealthSystem_OnDead;
        //lsplayer = player.GetComponent<PlayerMain>().levelSystem;
        
    }
    void Update()
    {
        FollowForPlayer();
    }

    private void HealthSystem_OnDead()
    {
        Destroy(gameObject);
        OnEnemyDied?.Invoke();
        healthSystem.OnDead -= HealthSystem_OnDead;
    }   
    void FollowForPlayer() 
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
    }
    public int GetDamage() 
    {
        return damage;
    } 
}
