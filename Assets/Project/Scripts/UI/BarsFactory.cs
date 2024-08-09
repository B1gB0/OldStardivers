using Build.Game.Scripts;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class BarsFactory : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBarTemplate;
    
        public HealthBar CreateHealthBar(Health health)
        {
            HealthBar healthBar = Instantiate(_healthBarTemplate);
            healthBar.Construct(health);

            return healthBar;
        }
    }
}