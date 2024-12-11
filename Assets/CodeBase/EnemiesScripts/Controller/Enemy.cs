using System;
using System.Collections;
using CodeBase.EnemiesScripts.Controller;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    private EnemyMover _enemyMover;
    private EnemyAttacker _enemyAttacker;
    private Transform _knight;
    private EnemyStaticData _data;
    private EnemyAnimationsController _enemyAnimationsController;
    private float _health;
    private bool _isDying;

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public Transform Transform => transform;

    public event Action HealthChanged;
    public event Action<Enemy> HasDied;

    public void Construct(EnemyStaticData data, Transform knight)
    {
        _knight = knight;
        _data = data;
        _health = _data.MaxHp;

        _enemyMover = GetComponent<EnemyMover>();
        _enemyAttacker = GetComponent<EnemyAttacker>();

        _enemyAnimationsController = GetComponent<EnemyAnimationsController>();

        _enemyMover.Construct(_data.Speed);
        _enemyAttacker.Construct(_data.Damage, _data.AttackCooldown, _data.DamageRange, _data.KnightLayer);

        _isDying = false;
    }

    private void Update()
    {
        if (_isDying)
            return;

        if (Vector2.Distance(transform.position, _knight.transform.position) > _data.AttackRange)
        {
            _enemyMover.Move(_knight);
        }
        else
        {
            _enemyAttacker.Attack();
        }
    }
    
    public void TakeDamage(float damage)
    {
        if (_isDying)
            return;

        _health -= damage;
        _enemyAnimationsController.TakeDamage();

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        _isDying = true;
        StartCoroutine(WaitForDie(_enemyAnimationsController.Die()));
    }

    private IEnumerator WaitForDie(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HasDied?.Invoke(this);
        gameObject.SetActive(false);
    }
}