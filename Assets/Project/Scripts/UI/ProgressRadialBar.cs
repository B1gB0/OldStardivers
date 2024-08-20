using System;
using Cinemachine;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class ProgressRadialBar : RadialBar
    {
        private Score.Score _score;

        public void Construct(Score.Score score, Transform target)
        {
            _score = score;
        }

        private void OnEnable()
        {
            _score.ValueChanged += OnChangedValue;
        }

        private void OnDisable()
        {
            _score.ValueChanged -= OnChangedValue;
        }

        private void OnChangedValue(int value, int maxValue)
        {
            SetValue(value, maxValue);
        }
    }
}