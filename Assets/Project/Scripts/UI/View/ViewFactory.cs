using Build.Game.Scripts;
using Project.Scripts.Score;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class ViewFactory : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBarTemplate;
        [SerializeField] private FloatingDamageTextView _damageTextViewTemplate;
        [SerializeField] private ProgressRadialBar _progressRadialBarPlaneTemplate;
    
        public HealthBar CreateHealthBar(Health health)
        {
            HealthBar healthBar = Instantiate(_healthBarTemplate);
            healthBar.Construct(health);

            return healthBar;
        }

        public ProgressRadialBar CreateProgressBar(Score.Score score, Transform target)
        {
            ProgressRadialBar progressRadialBar = Instantiate(_progressRadialBarPlaneTemplate);
            progressRadialBar.Construct(score, target);

            return progressRadialBar;
        }

        public FloatingDamageTextView CreateDamageTextView()
        {
            FloatingDamageTextView damageTextView = Instantiate(_damageTextViewTemplate);
            return damageTextView;
        }
    }
}