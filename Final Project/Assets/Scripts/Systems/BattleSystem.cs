using System;
using System.Collections;
using System.Collections.Generic;
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
        public Wave(int count, int rate)
        {
            Count = count;
            Rate = rate;
        }
    }
        
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private List<EnemyMain> _enemyMainPrefabs;
    [SerializeField] private int _enemiesInWave = 5;

    private WaveState _waveState = WaveState.Idle;
    private List<Wave> _wavesList;
    private float _waveCountdown;    
    private int _nextWave;
    private int _startRate;    
    private int _wavesSpawn;
    private int _spawnEnemies = 0;
    private int _killedEnemies = 0;

    public int SpawnEnemies => _spawnEnemies;
    public int KilledEnemies => _killedEnemies;
    
    private void Start()
    {
        EnemyMain.OnEnemyDied += EnemyMain_OnEnemyDied;
        _wavesSpawn = (int)SoundManager.Instance.WavesCount;        
        _nextWave = 0;        
        _waveCountdown = _timeBetweenWaves;
        _startRate = 1;        
        SetWavesInFirstRound();
    }

    private void Update()
    {       
        if (_waveState == WaveState.StopSpawning)
        {
            if (_spawnEnemies == _killedEnemies)
            {
                _waveState = WaveState.EndBattle;
                GameManager.Instance.UpdateGameStates(GameStates.Win);
            }
        }
        if (_waveState != WaveState.StopSpawning)
        {
            
            LevelSpawn();            
        }
    }

    private void EnemyMain_OnEnemyDied()
    {
        _killedEnemies++;
    }
    private void SetWavesInFirstRound()
    {
        _wavesList = new List<Wave>();
        _wavesList.Add(new Wave(_enemiesInWave, _startRate));
    }
    private void SetWavesInNextRound() 
    {
        _enemiesInWave += 10;
        //_enemiesInWave += (10 * _nextWave);
        _startRate += 1;
        _wavesList.Add(new Wave(_enemiesInWave, _startRate));
    }
    private void SetNextWaveNumber()
    {
        if (_nextWave < _wavesList.Count)
        {
            _nextWave++;
        }
        else
        {
            _waveState = WaveState.StopSpawning;
        }
    }

    private void LevelSpawn()
    {       
        _waveCountdown -= Time.deltaTime;
        if (_waveCountdown <= 0)
        {
            if (_waveState != WaveState.Spawning)
            {
                StartCoroutine(SpawnWave(_wavesList[_nextWave]));
            }
        }
    }
    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning " + wave.Name);
        _waveState = WaveState.Spawning;
        for (int i = 0; i < wave.Count; i++)
        {
            EnemyMain enemy = GetNewInstance(GetRandomSpawnPoint());
            _spawnEnemies++;// Random factory??
            
            //yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(1 / _wavesList[_nextWave].Rate);
        }
        _waveState = WaveState.Waiting;
        SetNextWaveNumber();
        if (_nextWave >= _wavesSpawn)
        {
            _waveState = WaveState.StopSpawning;
        }
        SetWavesInNextRound();
        _waveCountdown = _timeBetweenWaves;
        yield break;
    }
    
    private Vector2 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(10f, 20f) + PlayerMain.Instance.GetComponent<Transform>().position;
    }
    
    private EnemyMain GetRandomTypeOfEnemy()
    {
        return _enemyMainPrefabs[Random.Range(0, _enemyMainPrefabs.Count)];
    }
    private EnemyMain GetNewInstance(Vector2 spawnPoint)
    {
        return Instantiate(GetRandomTypeOfEnemy(), spawnPoint, Quaternion.identity);
    }
    private void OnDestroy()
    {
        EnemyMain.OnEnemyDied -= EnemyMain_OnEnemyDied;
    }
}