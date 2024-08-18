using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Build.Game.Scripts.ECS.Components;
using Build.Game.Scripts.ECS.EntityActors;
using Project.Game.Scripts;
using UnityEngine;

public class AssaultRifle : Weapon
{
    private const string ObjectPoolBulletName = "PoolBullets";
    private const string ObjectPoolSoundsOfShotsName = "PoolAssaultRifleSoundsOfShots";
    
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private AssaultRifleSoundOfShot soundPrefab;
    
    [SerializeField] private int _countBullets;

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _rangeAttack;
    [SerializeField] private float _delay;
    [SerializeField] private bool _isAutoExpandPool = false;

    private float _lastShotTime;
    private float _minValue = 0f;

    private EnemyActor _closestEnemy;

    private ObjectPool<Bullet> _poolBullets;
    private ObjectPool<AssaultRifleSoundOfShot> _poolSoundsOfShots;

    private void Awake()
    {
        _poolBullets = new ObjectPool<Bullet>(_bulletPrefab, _countBullets, new GameObject(ObjectPoolBulletName).transform);
        _poolSoundsOfShots = new ObjectPool<AssaultRifleSoundOfShot>(soundPrefab, _countBullets,
            new GameObject(ObjectPoolSoundsOfShotsName).transform);

        _poolSoundsOfShots.AutoExpand = _isAutoExpandPool;
        _poolBullets.AutoExpand = _isAutoExpandPool;
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
            Bullet bullet = _poolBullets.GetFreeElement();

            AssaultRifleSoundOfShot sound = _poolSoundsOfShots.GetFreeElement();
            
            sound.AudioSource.PlayOneShot(sound.AudioSource.clip);
            
            StartCoroutine(sound.OffSound());

            bullet.transform.position = _shootPoint.position;

            bullet.SetDirection(_closestEnemy.transform);

            _lastShotTime = _delay;
        }

        _lastShotTime -= Time.deltaTime;
    }
}
