using System;
using Project.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Transform _uiSceneContainer;

        [SerializeField] private SettingsPanel _settingsPanel;
        
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backToSceneButton;

        private void Awake()
        {
            _settingsPanel.SetVolumeValues();
            HideLoadingScreen();
        }

        private void OnEnable()
        {
            _settingsButton.onClick.AddListener(_settingsPanel.Show);
            _settingsButton.onClick.AddListener(HideUIScene);
            _backToSceneButton.onClick.AddListener(ShowUIScene);
        }

        private void OnDisable()
        {
            _settingsButton.onClick.RemoveListener(_settingsPanel.Show);
            _settingsButton.onClick.RemoveListener(HideUIScene);
            _backToSceneButton.onClick.RemoveListener(ShowUIScene);
        }

        public void ShowUIScene()
        {
            _uiSceneContainer.gameObject.SetActive(true);
        }

        public void HideUIScene()
        {
            _uiSceneContainer.gameObject.SetActive(false);
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }
        
        public void HideLoadingScreen()
        {
            _loadingScreen.SetActive(false);
        }

        public void AttachSceneUI(GameObject sceneUI)
        {
            ClearSceneUI();
            
            sceneUI.transform.SetParent(_uiSceneContainer, false);
        }

        private void ClearSceneUI()
        {
            var childCount = _uiSceneContainer.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Destroy(_uiSceneContainer.GetChild(i).gameObject);
            }
        }
    }
}