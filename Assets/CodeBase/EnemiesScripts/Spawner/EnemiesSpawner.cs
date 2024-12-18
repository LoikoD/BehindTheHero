using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using CodeBase.ThrowableObjects.Pool;
using CodeBase.EnemiesScripts.EnemyFSM;
using CodeBase.EnemiesScripts.Controller;
using CodeBase.Logic;
using CodeBase.Character;
using UnityEngine;
using Random = UnityEngine.Random;
using CodeBase.Logic.Utilities;

public class EnemiesSpawner : MonoBehaviour
{

    private const float EnemiesGap = 10f;
    private const int PoolBulkAmount = 15;
    private const float SpawnDistanceInScreens = 0.7f;
    private const float LootSpawnChance = 0.5f;
    private const int LootMaxAttemptsBeforeGuaranteedDrop = 5;

    private Camera _mainCamera;
    private Transform _knight;
    private EnemyStaticData _enemyData;
    private LevelStaticData _levelData;
    private ThrowableObjectPool _lootPool;

    private List<EnemyMain> _enemiesPool;
    private int _enemiesDied;
    private PityDropSystem _pityDropSystem;

    public event Action EndLevel;

    public void Construct(Transform knight, EnemyStaticData enemyData, LevelStaticData levelData, ThrowableObjectPool lootPool)
    {
        _knight = knight;
        _enemyData = enemyData;
        _levelData = levelData;
        _lootPool = lootPool;

        _mainCamera = Camera.main;
        _enemiesDied = 0;

        PoolEnemies();

        _pityDropSystem = new PityDropSystem(LootSpawnChance, LootMaxAttemptsBeforeGuaranteedDrop);
    }

    public void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    private void PoolEnemies()
    {
        _enemiesPool = new List<EnemyMain>();

        for (int i = 0; i < PoolBulkAmount; i++)
        {
            EnemyMain enemy = CreateEnemy();
            enemy.gameObject.SetActive(false);
            _enemiesPool.Add(enemy);
        }

    }

    private IEnumerator Spawning()
    {
        int _spawnedCount = 0;
        while (_spawnedCount < _levelData.EnemiesCount)
        {
            Vector3 _groupSpawnPos = GetRandomOffScreenPosition();

            int _enemiesInGroup = Mathf.Min(_levelData.EnemiesCount - _spawnedCount, Random.Range(_levelData.MinGroupCount, _levelData.MaxGroupCount));
            
            for (int i = 0; i < _enemiesInGroup; i++)
            {
                Vector3 position = _groupSpawnPos + new Vector3(Random.Range(-EnemiesGap, EnemiesGap), Random.Range(-EnemiesGap, EnemiesGap), 0);

                SpawnEnemy(position);

                _spawnedCount++;
            }
            
            yield return new WaitForSeconds(Random.Range(_levelData.MinSpawnDelay, _levelData.MaxSpawnDelay));
        }
    }
    
    private EnemyMain CreateEnemy()
    {
        GameObject enemyObj = Instantiate(_enemyData.Prefab, Vector3.zero, Quaternion.identity);
        enemyObj.transform.SetParent(transform, true);

        EnemySounds sounds = enemyObj.GetComponent<EnemySounds>();
        sounds.Construct();

        EnemyAnimationsController animator = enemyObj.GetComponent<EnemyAnimationsController>();
        animator.Construct(sounds);

        EnemyMover mover = enemyObj.GetComponent<EnemyMover>();
        mover.Construct(_enemyData.MoveSpeed);

        EnemyAttacker attacker = enemyObj.GetComponent<EnemyAttacker>();
        attacker.Construct(animator, _enemyData.Damage, _enemyData.AttackCooldown);

        EnemyStateMachine enemyStateMachine = new(mover, attacker, _enemyData, animator, _knight.GetComponent<IHealth>());

        EnemyMain enemyMain = enemyObj.GetComponent<EnemyMain>();
        enemyMain.Construct(enemyStateMachine, animator, _enemyData.Hp);

        enemyObj.GetComponent<CharacterTurner>().Construct(enemyStateMachine, animator);

        return enemyMain;
    }

    private EnemyMain SpawnEnemy(Vector3 position)
    {
        EnemyMain enemy = _enemiesPool.FirstOrDefault();

        if (enemy == null)
        {
            PoolEnemies();
            SpawnEnemy(position);
        }
        else
        {
            _enemiesPool.Remove(enemy);
            enemy.ResetState();
            enemy.Died += OnEnemyDeath;
            enemy.transform.position = position;
            enemy.gameObject.SetActive(true);
        }

        return enemy;
    }

    private void OnEnemyDeath(EnemyMain enemy)
    {
        _enemiesDied += 1;
        _enemiesPool.Add(enemy);

        if (_levelData.EnemiesCount == _enemiesDied)
            StartCoroutine(EndLevelAfterTime());

        if (_enemiesDied == 1 || (_enemiesDied != 1 && _pityDropSystem.ShouldDrop()))
            _lootPool.SpawnThrowableObject(enemy.transform.position);
        
        enemy.Died -= OnEnemyDeath;
    }

    private Vector3 GetRandomOffScreenPosition()
    {
        float screenHeight = _mainCamera.orthographicSize * 2;
        float screenWidth = screenHeight * _mainCamera.aspect;
        float distance = SpawnDistanceInScreens * (screenWidth > screenHeight ? screenWidth : screenHeight);

        float randomAngle = Random.Range(0f, 360f);
        Vector2 direction = new(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        Vector3 offScreenPosition = (Vector2)_mainCamera.transform.position + direction.normalized * distance;

        return offScreenPosition;
    }

    private IEnumerator EndLevelAfterTime()
    {
        yield return new WaitForSeconds(1);
        EndLevel?.Invoke();
    }
}
