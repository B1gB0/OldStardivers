using Build.Game.Scripts;
using Project.Scripts.Score;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.UI
{
    public class ViewFactory : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBarTemplate;
        [SerializeField] private ScoreView _scoreViewTemplate;
        [SerializeField] private ProgressRadialBar _progressRadialBarTemplate;
    
        public HealthBar CreateHealthBar(Health health)
        {
            HealthBar healthBar = Instantiate(_healthBarTemplate);
            healthBar.Construct(health);

            return healthBar;
        }

        public ScoreView CreateScoreView(Score.Score score)
        {
            ScoreView scoreView = Instantiate(_scoreViewTemplate);
            scoreView.Construct(score);

            return scoreView;
        }

        public ProgressRadialBar CreateProgressBar(Score.Score score, Transform target)
        {
            ProgressRadialBar progressRadialBar = Instantiate(_progressRadialBarTemplate, target);
            progressRadialBar.Construct(score, target);

            return progressRadialBar;
        }
    }
}