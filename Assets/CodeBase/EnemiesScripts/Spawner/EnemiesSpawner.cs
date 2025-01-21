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
    private Transform _knight;
    private EnemyStaticData _enemyData;
    private SpawnerStaticData _spawnerData;
    private LevelSpawnerData _levelSpawnerData;
    private ThrowableObjectPool _lootPool;

    private List<EnemyMain> _enemiesPool;
    private int _enemiesDied;
    private PityDropSystem _pityDropSystem;
    private OffScreenPointProvider _spawnPointProvider;

    public event Action EndLevel;

    public void Construct(Transform knight, LevelSpawnerData levelSpawnerData, EnemyStaticData enemyData, ThrowableObjectPool lootPool)
    {
        _knight = knight;
        _levelSpawnerData = levelSpawnerData;
        _spawnerData = levelSpawnerData.SpawnerData;
        _enemyData = enemyData;
        _lootPool = lootPool;

        _enemiesDied = 0;

        PoolEnemies();

        _pityDropSystem = new PityDropSystem(_spawnerData.LootSpawnChance, _spawnerData.LootMaxAttemptsBeforeGuaranteedDrop);
        _spawnPointProvider = new OffScreenPointProvider(Camera.main, _spawnerData.SpawnDistanceFromEdge);


        List<Vector2> basePoints = _spawnPointProvider.GetPointsForDebug();
        for (int i = 1; i < basePoints.Count; i++)
        {
            Debug.DrawLine(basePoints[i - 1], basePoints[i], Color.yellow, 10);
        }
        Debug.DrawLine(basePoints[^1], basePoints[0], Color.yellow, 10);
    }

    public void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    private void PoolEnemies()
    {
        _enemiesPool = new List<EnemyMain>();

        for (int i = 0; i < _spawnerData.EnemiesPoolBulkAmount; i++)
        {
            EnemyMain enemy = CreateEnemy();
            enemy.gameObject.SetActive(false);
            _enemiesPool.Add(enemy);
        }

    }

    private IEnumerator Spawning()
    {
        int _spawnedCount = 0;
        while (_spawnedCount < _levelSpawnerData.TotalEnemiesCount)
        {
            int _enemiesInGroup = Mathf.Min(_levelSpawnerData.TotalEnemiesCount - _spawnedCount, Random.Range(_levelSpawnerData.MinGroupCount, _levelSpawnerData.MaxGroupCount));

            List<Vector2> _groupSpawnPoints = _spawnPointProvider.GetRandomGroupPoints(_enemiesInGroup, _spawnerData.MinEnemiesGap);

            for (int i = 0; i < _enemiesInGroup; i++)
            {
                Vector3 position = _groupSpawnPoints[i];

                SpawnEnemy(position);

                _spawnedCount++;
            }
            
            yield return new WaitForSeconds(Random.Range(_levelSpawnerData.MinSpawnDelay, _levelSpawnerData.MaxSpawnDelay));
        }
    }
    
    private EnemyMain CreateEnemy()
    {
        GameObject enemyObj = Instantiate(_enemyData.Prefab, Vector3.zero, Quaternion.identity);
        enemyObj.transform.SetParent(transform, true);

        EnemySounds sounds = enemyObj.GetComponent<EnemySounds>();

        EnemyAnimationsController animator = enemyObj.GetComponent<EnemyAnimationsController>();
        animator.Construct(sounds);

        EnemyMover mover = enemyObj.GetComponent<EnemyMover>();
        mover.Construct(_enemyData.MoveSpeed, _enemyData.SeparationRadius, _enemyData.SeparationLayer);

        EnemyAttacker attacker = enemyObj.GetComponent<EnemyAttacker>();
        attacker.Construct(animator, _enemyData.Damage, _enemyData.AttackCooldown);

        EnemyStateMachine enemyStateMachine = new(mover, attacker, _enemyData, animator, _knight);

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

        if (_levelSpawnerData.TotalEnemiesCount == _enemiesDied)
            StartCoroutine(EndLevelAfterTime());

        if (_enemiesDied == 1 || (_enemiesDied != 1 && _pityDropSystem.ShouldDrop()))
            _lootPool.SpawnThrowableObject(enemy.transform.position);
        
        enemy.Died -= OnEnemyDeath;
    }

    private IEnumerator EndLevelAfterTime()
    {
        yield return new WaitForSeconds(1);
        EndLevel?.Invoke();
    }
}
