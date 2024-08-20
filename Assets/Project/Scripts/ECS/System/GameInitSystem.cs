using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts.ECS.Components;
using Build.Game.Scripts.ECS.Data;
using Build.Game.Scripts.ECS.Data.SO;
using Build.Game.Scripts.ECS.EntityActors;
using DI;
using Leopotam.Ecs;
using Project.Scripts.Score;
using UnityEngine;
using Zenject;

namespace Build.Game.Scripts.ECS.System
{
    public class GameInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const string EnemyPoolName = "EnemyPool";
        
        private readonly EcsWorld _world;

        private readonly PlayerInitData _playerInitData;
        private readonly EnemyInitData _enemyInitData;
        private readonly StoneInitData _stoneInitData;
        private readonly CapsuleInitData _capsuleInitData;
        
        private readonly List<Vector3> _enemySpawnPoints;
        private readonly List<Vector3> _stoneSpawnPoints;
        private readonly Vector3 _playerSpawnPoint;

        private Vector3 _capsuleSpawnPoint;
        private float _capsuleHeight = 20f;
        
        private PlayerActor player;
        private EnemyActor enemy;
        private CapsuleActor capsule;

        private float _lastSpawnTime;
        private float _minValue = 0f;
        private float _delay = 10f;

        private ObjectPool<EnemyActor> _enemyPool;
        private bool _isAutoExpand = true;

        private Score _score;
        
        public Health PlayerHealth { get; private set; }
        
        public Transform PlayerTransform { get; private set; }

        public GameInitSystem(PlayerInitData playerData, EnemyInitData enemyData, StoneInitData stoneInitData,
            CapsuleInitData capsuleData, LevelInitData levelData)
        {
            _playerInitData = playerData;
            _enemyInitData = enemyData;
            _stoneInitData = stoneInitData;
            _capsuleInitData = capsuleData;
            
            _enemySpawnPoints = levelData.EnemySpawnPoints;
            _playerSpawnPoint = levelData.PlayerSpawnPoint;
            _stoneSpawnPoints = levelData.ResourcesSpawnPoints;
        }

        public void Init()
        {
            _lastSpawnTime = _delay;
            
            player = CreatePlayer();
            capsule = CreateCapsule();
            
            PlayerHealth = player.Health;
            
            player.gameObject.SetActive(false);

            //_enemyPool = new ObjectPool<EnemyActor>(_enemyInitData.EnemyPrefab, _enemySpawnPoints.Count,
                //new GameObject(EnemyPoolName).transform);
            
            //_enemyPool.AutoExpand = _isAutoExpand;

            foreach (var resourcesSpawnPoint in _stoneSpawnPoints)
            {
                var resourceSpawnPosition = resourcesSpawnPoint + Vector3.one * Random.Range(-2f, 2f);
                resourceSpawnPosition.y = 0;
            
                CreateStone(resourceSpawnPosition);   
            }
        }

        public void Run()
        {
            if(capsule != null)
                LaunchCapsule(capsule);

            if (_lastSpawnTime <= _minValue)
            {
                SpawnEnemy();

                _lastSpawnTime = _delay;
            }

            _lastSpawnTime -= Time.deltaTime;
        }

        private CapsuleActor CreateCapsule()
        {
            _capsuleSpawnPoint = _playerSpawnPoint;
            _capsuleSpawnPoint.y += _capsuleHeight;
            
            var capsuleActor = Object.Instantiate(_capsuleInitData.Prefab, _capsuleSpawnPoint, Quaternion.identity);

            return capsuleActor;
        }

        private PlayerActor CreatePlayer()
        {
            var playerActor = Object.Instantiate(_playerInitData.Prefab, _playerSpawnPoint, Quaternion.identity);

            PlayerTransform = playerActor.transform;
            
            var player = _world.NewEntity();

            ref var inputEventComponent = ref player.Get<InputEventComponent>();
            inputEventComponent.PlayerInputController = playerActor.PlayerInputController;
            
            ref var playerComponent = ref player.Get<PlayerComponent>();
            playerComponent.MiningTool = playerActor.MiningToolActor;
            playerComponent.weapons = playerActor.Weapons;
            playerComponent.health = playerActor.Health;

            ref var movableComponent = ref player.Get<MovableComponent>();
            movableComponent.moveSpeed = _playerInitData.DefaultMoveSpeed;
            movableComponent.rotationSpeed = _playerInitData.DefaultRotationSpeed;
            movableComponent.transform = playerActor.transform;
            movableComponent.rigidbody = playerActor.Rigidbody;

            ref var animationsComponent = ref player.Get<AnimatedComponent>();
            animationsComponent.animator = playerActor.Animator;
            
            ref var attackComponent = ref player.Get<AttackComponent>();

            return playerActor;
        }
        
        private EnemyActor CreateEnemy(PlayerActor target)
        {
            var enemyActor = Object.Instantiate(_enemyInitData.EnemyPrefab);
            enemyActor.Construct(_score);

            var enemy = _world.NewEntity();
            
            ref var enemyComponent = ref enemy.Get<EnemyComponent>();
            enemyComponent.health = enemyActor.Health;

            ref var enemyMovableComponent = ref enemy.Get<MovableComponent>();
            enemyMovableComponent.moveSpeed = _enemyInitData.DefaultMoveSpeed;
            enemyMovableComponent.rotationSpeed = _enemyInitData.DefaultRotationSpeed;
            enemyMovableComponent.transform = enemyActor.transform;
            enemyMovableComponent.rigidbody = enemyActor.Rigidbody;

            ref var enemyAnimationsComponent = ref enemy.Get<AnimatedComponent>();
            enemyAnimationsComponent.animator = enemyActor.Animator;

            ref var followComponent = ref enemy.Get<FollowPlayerComponent>();
            followComponent.target = target;

            ref var attackComponent = ref enemy.Get<AttackComponent>();
            attackComponent.damage = _enemyInitData.DefaultDamage;

            return enemyActor;
        }

        private void CreateStone(Vector3 atPosition)
        {
            var stoneActor = Object.Instantiate(_stoneInitData.StonePrefab, atPosition, Quaternion.identity);
            stoneActor.Construct(_score);

            var resource = _world.NewEntity();

            ref var resourceComponent = ref resource.Get<ResourceComponent>();
            resourceComponent.health = stoneActor.Health;

            ref var animatedComponent = ref resource.Get<AnimatedComponent>();
            animatedComponent.animator = stoneActor.Animator;
        }

        private void SpawnEnemy()
        {
            foreach (var enemySpawnPoint in _enemySpawnPoints)
            {
                EnemyActor enemy = CreateEnemy(player);

                var enemySpawnPosition = enemySpawnPoint + Vector3.one * Random.Range(-2f, 2f);
                enemySpawnPosition.y = 0f;

                enemy.transform.position = enemySpawnPosition;

                foreach (Weapon weapon in player.Weapons)
                {
                    weapon.Detector.AddEnemy(enemy);
                }
            }
        }
        
        private void LaunchCapsule(CapsuleActor capsule)
        {
            capsule.transform.position = Vector3.MoveTowards(capsule.transform.position, _playerSpawnPoint,
                _capsuleInitData.DefaultMoveSpeed * Time.deltaTime);

            if (capsule.transform.position == _playerSpawnPoint)
            {
                player.gameObject.SetActive(true);
                capsule.Destroy();
            }
        }
    }
}