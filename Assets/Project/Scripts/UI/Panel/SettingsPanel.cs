using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class SettingsPanel : MonoBehaviour, IView
    {
        private const string MusicVolume = nameof(MusicVolume);
        private const string EffectsVolume = nameof(EffectsVolume);

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backToSceneButton;

        [SerializeField] private AudioMixerGroup _mixer;
        
        [SerializeField] private float _minVolume = -80f;
        [SerializeField] private float _maxVolume;
        
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _effectsVolumeSlider;
        
        private float _minValueSlider = 0f;
        private float _startValueSlider = 0.8f;

        private PauseService _pauseService;

        private void OnEnable()
        {
            _backToSceneButton.onClick.AddListener(Hide);
            _settingsButton.gameObject.SetActive(false);

            if (SceneManager.GetActiveScene().name == Scenes.Gameplay)
            {
                _pauseService.StopGame();
            }

            _musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
            _effectsVolumeSlider.onValueChanged.AddListener(ChangeEffectsVolume);
        }

        private void OnDisable()
        {
            _backToSceneButton.onClick.RemoveListener(Hide);
            _settingsButton.gameObject.SetActive(true);
            
            if (SceneManager.GetActiveScene().name == Scenes.Gameplay)
            {
                _pauseService.PlayGame();
            }
            
            _musicVolumeSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
            _effectsVolumeSlider.onValueChanged.RemoveListener(ChangeEffectsVolume);
        }

        private void ChangeMusicVolume(float volume)
        {
            _mixer.audioMixer.SetFloat(MusicVolume, Mathf.Lerp(_minVolume, _maxVolume, volume));

            PlayerPrefs.SetFloat(MusicVolume, volume);
        }

        private void ChangeEffectsVolume(float volume)
        {
            _mixer.audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(_minVolume, _maxVolume, volume));

            PlayerPrefs.SetFloat(EffectsVolume, volume);
        }
        
        public void GetPauseService(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetValuesVolume()
        {
            _musicVolumeSlider.value = PlayerPrefs.GetFloat(MusicVolume);
            _effectsVolumeSlider.value = PlayerPrefs.GetFloat(EffectsVolume);

            if (PlayerPrefs.GetFloat(MusicVolume) == _minValueSlider &&
                PlayerPrefs.GetFloat(EffectsVolume) == _minValueSlider)
            {
                _musicVolumeSlider.value = _startValueSlider;
                _effectsVolumeSlider.value = _startValueSlider;
            }
        }
    }
}
