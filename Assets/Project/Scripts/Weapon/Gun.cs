using System;
using Build.Game.Scripts.ECS.EntityActors;
using Project.Game.Scripts;
using UnityEngine;

public class Gun : Weapon
{
    private const string ObjectPoolBulletName = "PoolBullets";
    private const string ObjectPoolSoundsOfShotsName = "PoolAssaultRifleSoundsOfShots";
    private const float MinValue = 0f;
    private readonly GunCharacteristics _gunCharacteristics = new();
    
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private AssaultRifleSoundOfShot soundPrefab;
    
    [SerializeField] private int _countBullets;

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private bool _isAutoExpandPool = false;

    private float _lastShotTime;
    private Bullet _bullet;

    private ClosestEnemyDetector _detector;
    private EnemyActor closestEnemy;

    private ObjectPool<Bullet> _poolBullets;
    private ObjectPool<AssaultRifleSoundOfShot> _poolSoundsOfShots;


    public void Construct(ClosestEnemyDetector detector)
    {
        _detector = detector;
    }

    private void Awake()
    {
        _poolBullets = new ObjectPool<Bullet>(_bulletPrefab, _countBullets, new GameObject(ObjectPoolBulletName).transform);
        _poolSoundsOfShots = new ObjectPool<AssaultRifleSoundOfShot>(soundPrefab, _countBullets,
            new GameObject(ObjectPoolSoundsOfShotsName).transform);

        _poolSoundsOfShots.AutoExpand = _isAutoExpandPool;
        _poolBullets.AutoExpand = _isAutoExpandPool;
    }

    private void FixedUpdate()
    {
        closestEnemy = _detector.СlosestEnemy;
        
        if (closestEnemy != null)
        {
            if (Vector3.Distance(closestEnemy.transform.position, transform.position) <= _gunCharacteristics.RangeAttack)
            {
                Shoot();
            }
        }
    }
    
    public override void Shoot()
    {
        if (_lastShotTime <= MinValue && closestEnemy.Health.Value > MinValue)
        {
            _bullet = _poolBullets.GetFreeElement();

            AssaultRifleSoundOfShot sound = _poolSoundsOfShots.GetFreeElement();
            
            sound.AudioSource.PlayOneShot(sound.AudioSource.clip);
            
            StartCoroutine(sound.OffSound());

            _bullet.transform.position = _shootPoint.position;

            _bullet.SetDirection(closestEnemy.transform);
            _bullet.SetCharacteristics(_gunCharacteristics.Damage, _gunCharacteristics.BulletSpeed);

            _lastShotTime = _gunCharacteristics.FireRate;
        }

        _lastShotTime -= Time.fixedDeltaTime;
    }
}
