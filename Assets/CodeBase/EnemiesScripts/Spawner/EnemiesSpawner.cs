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

    private Camera _mainCamera;
    private Transform _knight;
    private EnemyStaticData _data;

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

        _mainCamera = Camera.main;

        _enemiesDied = 0;
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        int _spawnedCount = 0;
        while (_spawnedCount < _enemiesCount)
        {
            Vector3 _groupSpawnPos = GetRandomOffScreenPosition();

            int _enemiesInGroup = Mathf.Min(_enemiesCount - _spawnedCount, Random.Range(_minGroupCount, _maxGroupCount));
            
            for (int i = 0; i < _enemiesInGroup; i++)
            {
                Vector3 position = _groupSpawnPos + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

                Enemy enemy = CreateEnemy(position);
                enemy.HasDied += OnEnemyDeath;

                _spawnedCount++;
            }
            
            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
        }
    }
    
    private Enemy CreateEnemy(Vector3 position)
    {
        GameObject enemySpawn = Instantiate(_data.Prefab, new Vector3(position.x, position.y, 0), Quaternion.identity);

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

    private Vector3 GetRandomOffScreenPosition()
    {
        float screenHeight = _mainCamera.orthographicSize * 2;
        float screenWidth = screenHeight * _mainCamera.aspect;

        float randomAngle = Random.Range(0f, 360f);
        Vector3 direction = new(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0);
        float maxDistance = Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ? screenWidth : screenHeight;
        Vector3 offScreenPosition = _mainCamera.transform.position + direction.normalized * maxDistance;

        return offScreenPosition;
    }
}
