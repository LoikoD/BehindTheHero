using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using CodeBase.ThrowableObjects;
using CodeBase.ThrowableObjects.Pool;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private int _enemiesCount = 5;
    [SerializeField] private int _minGroupCount = 1;
    [SerializeField] private int _maxGroupCount = 2;
    [SerializeField] private float _minSpawnDelay = 7;
    [SerializeField] private float _maxSpawnDelay = 7;
    [SerializeField] private float _randomRange = 0.1f;
    [SerializeField] private ThrowableObjectPool _lootPool;

    private int _enemiesDied;
    
    private Transform _knight;
    private EnemyStaticData _data;
    private List<ThrowableObject> _possibleLoot;

    public event Action EndLevel;

    public void Construct(Transform knight, EnemyStaticData enemyData)
    {
        _knight = knight;
        _data = enemyData;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "3")
        {
            _enemiesCount = 14;
            _minGroupCount = 1;
            _maxGroupCount = 3;
        }
        else if (SceneManager.GetActiveScene().name == "4")
        {
            _enemiesCount = 30;
            _minGroupCount = 2;
            _maxGroupCount = 4;
            _minSpawnDelay = 3;
            _maxSpawnDelay = 5;
        }

        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        int _spawnedCount = 0;
        while (_spawnedCount < _enemiesCount)
        {
            bool _xAxisToIncrease = Random.value < 0.5f;
            bool _xPosHalf = Random.value < 0.5f;
            bool _yPosHalf = Random.value < 0.5f;

            float _xPosToSpawn = GetXPosition(_xAxisToIncrease, _xPosHalf);

            float _yPosToSpawn = GetYPosition(_xAxisToIncrease, _xPosHalf);

            Vector2 _groupSpawnPos = Camera.main.ViewportToWorldPoint(new Vector2(_xPosToSpawn, _yPosToSpawn));
            
            int _enemiesInGroup = Mathf.Min(_enemiesCount - _spawnedCount, 
                (int)(Random.Range(_minGroupCount, _maxGroupCount)));
            
            for (int i = _enemiesInGroup; i > 0; i--)
            {
                Vector2 position = _groupSpawnPos + new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));

                Enemy enemy = CreateEnemy(position);
                enemy.HasDied += OnEnemyDeath;

            }
            
            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
        }
    }
    
    private Enemy CreateEnemy(Vector3 position)
    {
        GameObject enemySpawn = Instantiate(_data.Prefab, new Vector2(position.x, position.y), Quaternion.identity);

        Enemy enemy = enemySpawn.GetComponent<Enemy>();

        enemy.Construct(_data, _knight);

        return enemy;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        _enemiesDied += 1;

        if (_enemiesCount == _enemiesDied)
            EndLevel?.Invoke();
        

        if (_enemiesDied % 2 == 0)
            _lootPool.SpwanThrowableObject(enemy.transform.position);
        
        enemy.HasDied -= OnEnemyDeath;
    }

    private float GetXPosition(bool isIncrease, bool isHalf)
    {
        float _xPosToSpawn = isIncrease ? (isHalf ? 10f + Random.Range(0, _randomRange) : -10f - Random.Range(0, _randomRange)) 
            : Random.Range(0 - _randomRange, 1 + _randomRange);

        return _xPosToSpawn;
    }

    private float GetYPosition(bool isIncrease, bool isHalf)
    {
        float _yPosToSpawn = isIncrease  ? Random.Range(0 - _randomRange, 1 + _randomRange)
            : (isHalf ? 10f + Random.Range(0, _randomRange) : -10f - Random.Range(0, _randomRange));

        return _yPosToSpawn;
    }
}
