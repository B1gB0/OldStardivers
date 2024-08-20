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

namespace Build.Game.Scripts.Game.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        private readonly DataFactory _dataFactory = new ();

        [SerializeField] private ViewFactory viewFactory;
        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;

        private PlayerInitData _playerData;
        private LevelInitData _levelData;
        private EnemyInitData _enemyData;
        private StoneInitData _stoneData;
        private CapsuleInitData _capsuleData;

        private GameInitSystem _gameInitSystem;

        private EcsWorld _world;
        private EcsSystems _systems;
        
        private HealthBar _healthBar;
        private ProgressRadialBar _progressBar;
        private ScoreView _scoreView;

        private void Start()
        {
            _playerData = _dataFactory.CreatePlayerData();
            _levelData = _dataFactory.CreateLevelData();
            _enemyData = _dataFactory.CreateEnemyData();
            _stoneData = _dataFactory.CreateStoneData();
            _capsuleData = _dataFactory.CreateCapsuleData();

            Score score = new Score();

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.Inject(score);
            _systems.Add(_gameInitSystem = new GameInitSystem(_playerData, _enemyData, _stoneData, _capsuleData, _levelData));
            _systems.Add(new PlayerInputSystem());
            _systems.Add(new PlayerMoveSystem());
            _systems.Add(new MainCameraSystem(_mainCamera));
            _systems.Add(new FollowSystem());
            _systems.Add(new PlayerAnimatedSystem());
            _systems.Add(new EnemyAnimatedSystem());
            _systems.Add(new EnemyAttackSystem());
            _systems.Add(new ResourcesAnimatedSystem());
            _systems.Init();
            
            _progressBar = viewFactory.CreateProgressBar(score, _gameInitSystem.PlayerTransform);
            _healthBar = viewFactory.CreateHealthBar(_gameInitSystem.PlayerHealth);
            
            _progressBar.Show();
            _healthBar.Show();
        }

        private void Update()
        {
            _systems?.Run();
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
            _systems?.Destroy();
            _world?.Destroy();
        }
    }
}