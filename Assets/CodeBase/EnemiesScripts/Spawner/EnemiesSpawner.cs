using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagment;
using System.Linq;
using CodeBase.Infrastructure.Factory;
using CodeBase.Knight.KnightFSM;
using CodeBase.Knight;
using CodeBase.StaticData;
using CodeBase.ThrowableObjects;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using CodeBase.ThrowableObjects.Pool;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Spine;
using CodeBase.EnemiesScripts.EnemyFSM;
using UnityEditor.VersionControl;
using CodeBase.EnemiesScripts.Controller;
using CodeBase.Logic;
using CodeBase.Character;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private int _enemiesCount = 5;
    [SerializeField] private int _minGroupCount = 1;
    [SerializeField] private int _maxGroupCount = 2;
    [SerializeField] private float _minSpawnDelay = 7;
    [SerializeField] private float _maxSpawnDelay = 7;
    [SerializeField] private ThrowableObjectPool _lootPool;

    private const float EnemiesGap = 10f;
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
                Vector3 position = _groupSpawnPos + new Vector3(Random.Range(-EnemiesGap, EnemiesGap), Random.Range(-EnemiesGap, EnemiesGap), 0);

                EnemyMain enemy = CreateEnemy(position);
                enemy.HasDied += OnEnemyDeath;

                _spawnedCount++;
            }
            
            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
        }
    }
    
    private EnemyMain CreateEnemy(Vector3 position)
    {
        GameObject enemyObj = Instantiate(_data.Prefab, new Vector3(position.x, position.y, 0), Quaternion.identity);

        EnemyAnimationsController animator = enemyObj.GetComponent<EnemyAnimationsController>();

        EnemyMover mover = enemyObj.GetComponent<EnemyMover>();
        mover.Construct(_data.MoveSpeed);

        EnemyAttacker attacker = enemyObj.GetComponent<EnemyAttacker>();
        attacker.Construct(animator, _data.Damage, _data.AttackCooldown);

        EnemyStateMachine enemyStateMachine = new(mover, attacker, _data, animator, _knight.GetComponent<IHealth>());

        EnemyMain enemyMain = enemyObj.GetComponent<EnemyMain>();
        enemyMain.Construct(enemyStateMachine, animator, _data.Hp);

        enemyObj.GetComponent<CharacterTurner>().Construct(enemyStateMachine, animator);

        return enemyMain;
    }

    private void OnEnemyDeath(EnemyMain enemy)
    {
        _enemiesDied += 1;

        if (_enemiesCount == _enemiesDied)
            StartCoroutine(EndLevelAfterTime());

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

    private IEnumerator EndLevelAfterTime()
    {
        yield return new WaitForSeconds(1);
        EndLevel?.Invoke();
    }
}
