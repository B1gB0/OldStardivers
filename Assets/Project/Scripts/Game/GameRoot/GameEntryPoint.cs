using System.Collections;
using Build.Game.Scripts.Game.Gameplay;
using Build.Game.Scripts.Game.Gameplay.GameplayRoot;
using R3;
using Source.Game.Scripts;
using Source.Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Build.Game.Scripts.Game.GameRoot
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        
        private Coroutines _coroutines;
        private UIRootView _uiRoot;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            _instance = new GameEntryPoint();
            _instance.StartGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
        }

        private void StartGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.MainMenu)
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
                return;
            }

            if (sceneName == Scenes.Gameplay)
            {
                var enterParameters = new GameplayEnterParameters("", 1);
                
                _coroutines.StartCoroutine(LoadAndStartGameplay(enterParameters));
                
                return;
            }

            if (sceneName != Scenes.Boot)
            {
                return;
            }
#endif

            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParameters enterParameters = null)
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.Boot);
            yield return LoadScene(Scenes.MainMenu);

            yield return new WaitForSeconds(1);

            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            sceneEntryPoint.Run(_uiRoot, enterParameters).Subscribe(mainMenuExitParameters =>
            {
                var targetSceneName = mainMenuExitParameters.TargetSceneEnterParameters.SceneName;

                if (targetSceneName == Scenes.Gameplay)
                {
                    _coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParameters.
                        TargetSceneEnterParameters.As<GameplayEnterParameters>()));
                }
            } );

            _uiRoot.HideLoadingScreen();
        }
        
        private IEnumerator LoadAndStartGameplay(GameplayEnterParameters enterParameters)
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.Boot);
            yield return LoadScene(Scenes.Gameplay);
            
            yield return new WaitForSeconds(1);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Run(_uiRoot, enterParameters).Subscribe(gameplayExitParameters =>
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParameters.MainMenuEnterParameters));
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}