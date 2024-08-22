using System;
using Build.Game.Scripts.ECS.Data;
using Build.Game.Scripts.ECS.Data.SO;
using Build.Game.Scripts.ECS.System;
using Build.Game.Scripts.Game.Gameplay.GameplayRoot;
using Cinemachine;
using Leopotam.Ecs;
using Project.Scripts.Score;
using Project.Scripts.UI;
using R3;
using Source.Game.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Build.Game.Scripts.Game.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        private readonly DataFactory _dataFactory = new ();

        [SerializeField] private ViewFactory _viewFactory;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;

        private PlayerInitData _playerData;
        private LevelInitData _levelData;
        private EnemyInitData _enemyData;
        private StoneInitData _stoneData;
        private CapsuleInitData _capsuleData;

        private GameInitSystem _gameInitSystem;

        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        
        private HealthBar _healthBar;
        private ProgressRadialBar _progressBar;

        private void Start()
        {
            _playerData = _dataFactory.CreatePlayerData();
            _levelData = _dataFactory.CreateLevelData();
            _enemyData = _dataFactory.CreateEnemyData();
            _stoneData = _dataFactory.CreateStoneData();
            _capsuleData = _dataFactory.CreateCapsuleData();
            
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);

            FloatingDamageTextView damageTextView = _viewFactory.CreateDamageTextView();
            FloatingDamageTextPresenter damageTextPresenter = new FloatingDamageTextPresenter(damageTextView); ;
            
            Score score = new Score();

            _updateSystems.Inject(score);
            _updateSystems.Inject(damageTextPresenter);
            _updateSystems.Add(_gameInitSystem = new GameInitSystem(_playerData, _enemyData, _stoneData, _capsuleData, _levelData));
            _updateSystems.Add(new PlayerInputSystem());
            _updateSystems.Add(new MainCameraSystem(_cinemachineVirtualCamera));
            _updateSystems.Add(new PlayerAnimatedSystem());
            _updateSystems.Add(new EnemyAnimatedSystem());
            _updateSystems.Add(new EnemyAttackSystem());
            _updateSystems.Add(new ResourcesAnimatedSystem());
            _updateSystems.Init();
            
            _fixedUpdateSystems.Add(new PlayerMoveSystem());
            _fixedUpdateSystems.Add(new FollowSystem());
            _fixedUpdateSystems.Init();
            
            _healthBar = _viewFactory.CreateHealthBar(_gameInitSystem.PlayerHealth);
            _progressBar = _viewFactory.CreateProgressBar(score, _gameInitSystem.PlayerTransform);
            
            _gameInitSystem.PlayerIsLanded += _healthBar.Show;
            _gameInitSystem.PlayerIsLanded += _progressBar.Show;
        }

        private void OnDisable()
        {
            if (_gameInitSystem != null)
            {
                _gameInitSystem.PlayerIsLanded -= _healthBar.Show;
                _gameInitSystem.PlayerIsLanded -= _progressBar.Show;
            }
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        public Observable<GameplayExitParameters> Run(UIRootView uiRoot, GameplayEnterParameters enterParameters)
        {
            UIGameplayRootBinder uiScene = Instantiate(_sceneUIRootPrefab);
            _healthBar.transform.SetParent(uiScene.transform);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalSubject = new Subject<Unit>();
            uiScene.Bind(exitSceneSignalSubject);

            var mainMenuEnterParameters = new MainMenuEnterParameters("Enter parameters");
            var exitParameters = new GameplayExitParameters(mainMenuEnterParameters);
            var exitToMainMenuSceneSignal = exitSceneSignalSubject.Select(_ => exitParameters);

            return exitToMainMenuSceneSignal;
        }

        private void OnDestroy()
        {
            _updateSystems?.Destroy();
            _world?.Destroy();
        }
    }
}