using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Build.Game.Scripts.ECS.Components;
using Build.Game.Scripts.ECS.EntityActors;
using UnityEngine;

public class AssaultRifle : Weapon
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private int _countBullets;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _rangeAttack;
    [SerializeField] private float _delay;
    [SerializeField] private bool _isAutoExpandPool = false;

    private float _lastShotTime;
    private float _minValue = 0f;

    private EnemyActor _closestEnemy;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(_prefab, _countBullets, new GameObject("PoolBullets").transform);
        _pool.AutoExpand = _isAutoExpandPool;
    }

    private void Update()
    {
        _closestEnemy = Detector.СlosestEnemy;
        
        if (_closestEnemy != null)
        {
            if (Vector3.Distance(_closestEnemy.transform.position, transform.position) <= _rangeAttack)
            {
                Shoot();
            }
        }
    }
    
    public override void Shoot()
    {
        if (_lastShotTime <= _minValue && _closestEnemy.Health.Value > _minValue)
        {
            Bullet bullet = _pool.GetFreeElement();
            
            bullet.transform.position = _shootPoint.position;
            
            bullet.SetDirection(_closestEnemy.transform);

            _lastShotTime = _delay;
        }

        _lastShotTime -= Time.deltaTime;
    }
}
