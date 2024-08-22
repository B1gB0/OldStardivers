using System;
using Cinemachine;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class ProgressRadialBar : RadialBar
    {
        private Score.Score _score;
        private Transform _target;

        private float _height = 0.02f;

        public void Construct(Score.Score score, Transform target)
        {
            _score = score;
            _target = target;
        }

        private void OnEnable()
        {
            _score.ValueChanged += OnChangedValue;
        }

        private void Update()
        {
            transform.position = new Vector3(_target.position.x, _height, _target.position.z);
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