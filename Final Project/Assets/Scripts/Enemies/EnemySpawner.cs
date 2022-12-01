using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyFactory factory;
    int enemiesCount = 1000;        
    [SerializeField] int radius = 20;

    List<EnemyMain> enemies;
    Vector2 playerPosition;

    void Start()
    {
        playerPosition = FindObjectOfType<PlayerMovementHandler>().transform.position;

        enemies = new List<EnemyMain>(enemiesCount);
        for (int i = 0; i < enemiesCount; i++)
        {
            var enemy = factory.GetNewInstance(GetRandomSpawnPoint());
            enemies.Add(enemy);
        }
    }
    //public Vector2 GetSpawnPoint()
    //{
    //    Vector2 spawnPoint = playerPosition*3 + Random.insideUnitCircle * radius;
    //    return spawnPoint;
    //}
    private Vector2 GetRandomSpawnPoint() 
    {
        return new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized * Random.Range(30f, 200f) + playerPosition;
    }

}
