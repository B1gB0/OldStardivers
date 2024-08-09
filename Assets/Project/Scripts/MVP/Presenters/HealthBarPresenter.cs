using Build.Game.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.MVP.Presenters
{
    public class HealthBarPresenter : IPresenter
    {
        private readonly HealthBar _healthBar;
        private readonly Health _health;

        public HealthBarPresenter(HealthBar healthBar, Health health)
        {
            _healthBar = healthBar;
            _health = health;
        }

        public void Enable()
        {
            _healthBar.Show();
            _health.Die += OnDie;
            _health.HealthChanged += OnChangedValues;
        }

        public void Disable()
        {
            _health.Die -= OnDie;
            _health.HealthChanged -= OnChangedValues;
        }

        private void OnDie()
        {
            _healthBar.Hide();
        }

        private void OnChangedValues(float currentHealth, float maxHealth, float targetHealth)
        {
            UpdateValues(currentHealth, maxHealth, targetHealth);
        }

        private void UpdateValues(float currentHealth, float maxHealth, float targetHealth)
        {
            _healthBar.SetValues(currentHealth, maxHealth, targetHealth);
        }
    }
}
