using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleSystem : MonoBehaviour
{
    public enum WaveState
    {
        Idle,
        Spawning,
        Waiting,
        StopSpawning,
        EndBattle

    }

    [Serializable]
    public class Wave
    {
        public string Name;
        public int Count;
        public float Rate;
    }
    private WaveState _waveState = WaveState.Idle;
    
    //[SerializeField] private MovingEnemyFactory _factory;
    [SerializeField] private List <GenericFactory<EnemyMain>> _factories;
    //[SerializeField] private GenericFactory<MovingEnemy> _enemyFactory;

    [SerializeField] private MovingEnemyFactory factory1;
    [SerializeField] private ShootingEnemyFactory factory2;
    [SerializeField] private ExplosionEnemyFactory factory3;

    [SerializeField] private Wave[] _waves;
    [SerializeField] private float _timeBetweenWaves;

    private float _waveCountdown;
    List<EnemyMain> _standEnemies;
    private int _nextWave;

    private void Awake()
    {
        _factories = new List<GenericFactory<EnemyMain>>();
        //_factories.Add(factory1);


    }
    private void Start()
    {
        _nextWave = 0;
        _standEnemies = new List<EnemyMain>();
        _waveCountdown = _timeBetweenWaves;
    }

    private void Update()
    {
        if (_waveState == WaveState.StopSpawning)
        {
            //if (_standEnemies == null)  ¿  «‡ÍŒÕ◊»“‹ –¿”Õƒ??????????
            //{
            //    Debug.Log("EMpTYYYYYYY");
            //}
        }
        if (_waveState != WaveState.StopSpawning)
        {
            LevelSpawn();            
        }
    }


    private void SetNextWaveNumber()
    {
        if (_nextWave + 1 < _waves.Length)
        {
            _nextWave++;
        }
        else
        {
            _waveState = WaveState.StopSpawning;
        }
    }

    void LevelSpawn()
    {       
        _waveCountdown -= Time.deltaTime;
        if (_waveCountdown <= 0)
        {
            if (_waveState != WaveState.Spawning)
            {
                StartCoroutine(SpawnWave(_waves[_nextWave]));
            }
        }
    }
    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning " + wave.Name);
        _waveState = WaveState.Spawning;
        for (int i = 0; i < wave.Count; i++)
        {
            EnemyMain enemy = factory2.GetNewInstance(GetRandomSpawnPoint());// Random factory??
            _standEnemies.Add(enemy);
            yield return new WaitForSeconds(1 / wave.Rate);
        }
        _waveState = WaveState.Waiting;
        SetNextWaveNumber();
        _waveCountdown = _timeBetweenWaves;
        yield break;
    }

    void SpawnEnemy(EnemyMain enemy)
    {
    }
    private Vector2 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(10f, 20f) + PlayerMain.Instance.GetComponent<Transform>().position;
    }
    private GenericFactory<EnemyMain> GenerGetRandomTypeOfEnemy() 
    {
        return _factories[Random.Range(0, _factories.Count)];
    }
}
