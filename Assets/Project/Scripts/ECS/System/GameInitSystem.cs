using System.Collections.Generic;
using Build.Game.Scripts.ECS.Components;
using Build.Game.Scripts.ECS.Data;
using Build.Game.Scripts.ECS.Data.SO;
using Build.Game.Scripts.ECS.EntityActors;
using Leopotam.Ecs;
using Project.Game.Scripts.Player.PlayerInputModule;
using Project.Scripts.MVP.Presenters;
using UnityEngine;

namespace Build.Game.Scripts.ECS.System
{
    public class GameInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;

        private readonly PlayerInitData _playerInitData;
        private readonly EnemyInitData _enemyInitData;
        private readonly StoneInitData _stoneInitData;
        
        private readonly List<Vector3> _enemySpawnPoints;
        private readonly List<Vector3> _stoneSpawnPoints;
        
        private readonly Vector3 _playerSpawnPoint;

        public Weapon Mines { get; private set; }

        public GameInitSystem(PlayerInitData playerData, EnemyInitData enemyData, StoneInitData stoneInitData,
            List<Vector3> enemySpawnPoints, Vector3 playerSpawnPoint, List<Vector3> stoneSpawnPoints)
        {
            _playerInitData = playerData;
            _enemyInitData = enemyData;
            _stoneInitData = stoneInitData;
            _enemySpawnPoints = enemySpawnPoints;
            _playerSpawnPoint = playerSpawnPoint;
            _stoneSpawnPoints = stoneSpawnPoints;
        }

        public void Init()
        {
            var playerActor = Object.Instantiate(_playerInitData.Prefab, _playerSpawnPoint, Quaternion.identity);
            
            CreatePlayer(playerActor.Animator, playerActor.transform, playerActor.Rigidbody,
                playerActor.PlayerInputController, playerActor.MiningToolActor, playerActor.Weapons, playerActor.Health);

            foreach (var enemySpawnPoint in _enemySpawnPoints)
            {
                var enemySpawnPosition = enemySpawnPoint + Vector3.one * Random.Range(-2f, 2f);
                enemySpawnPosition.y = 0f;
            
                EnemyActor enemy = CreateEnemy(enemySpawnPosition, playerActor);

                Mines = playerActor.Weapons[1];

                foreach (Weapon weapon in playerActor.Weapons)
                {
                    weapon.Detector.AddEnemy(enemy);
                }
            }

            foreach (var resourcesSpawnPoint in _stoneSpawnPoints)
            {
                var resourceSpawnPosition = resourcesSpawnPoint + Vector3.one * Random.Range(-2f, 2f);
                resourceSpawnPosition.y = 0;
            
                CreateResources(resourceSpawnPosition);   
            }
        }

        private void CreatePlayer(Animator animator, Transform transform, Rigidbody rigidbody,
            PlayerInputController playerInputController, MiningToolActor miningToolActor, List<Weapon> weapons, Health health)
        {
            var player = _world.NewEntity();

            ref var inputEventComponent = ref player.Get<InputEventComponent>();
            inputEventComponent.PlayerInputController = playerInputController;
            
            ref var playerComponent = ref player.Get<PlayerComponent>();
            playerComponent.miningTool = miningToolActor;
            playerComponent.weapons = weapons;
            playerComponent.health = health;

            ref var movableComponent = ref player.Get<MovableComponent>();
            movableComponent.moveSpeed = _playerInitData.DefaultMoveSpeed;
            movableComponent.rotationSpeed = _playerInitData.DefaultRotationSpeed;
            movableComponent.transform = transform;
            movableComponent.rigidbody = rigidbody;

            ref var animationsComponent = ref player.Get<AnimatedComponent>();
            animationsComponent.animator = animator;
            
            ref var attackComponent = ref player.Get<AttackComponent>();
        }
        
        private EnemyActor CreateEnemy(Vector3 atPosition, PlayerActor target)
        {
            var enemyActor = Object.Instantiate(_enemyInitData.EnemyPrefab, atPosition, Quaternion.identity);

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

        private void CreateResources(Vector3 atPosition)
        {
            var resourceActor = Object.Instantiate(_stoneInitData.StonePrefab, atPosition, Quaternion.identity);

            var resource = _world.NewEntity();

            ref var resourceComponent = ref resource.Get<ResourceComponent>();
            resourceComponent.health = resourceActor.Health;

            ref var animatedComponent = ref resource.Get<AnimatedComponent>();
            animatedComponent.animator = resourceActor.Animator;
        }
    }
}