using TMPro;
using UnityEngine;

namespace Project.Scripts.Score
{
    public class ScoreView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _scoreText;
        
        private Score _score;
        
        public void Construct(Score score)
        {
            _score = score;
        }

        private void OnEnable()
        {
            _score.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _score.ValueChanged -= OnValueChanged;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void OnValueChanged(int value, int maxValue)
        {
            _scoreText.text = value.ToString();
        }
    }
}