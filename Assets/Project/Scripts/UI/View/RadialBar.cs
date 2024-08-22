using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.UI
{
    public class RadialBar : MonoBehaviour, IView
    {
        private readonly int RemovedSegments = Shader.PropertyToID("_RemovedSegments");
        
        [SerializeField] private Material _backgroundBarMaterial;
        [SerializeField] private Material _barMaterial;
        [SerializeField] private TMP_Text _text;

        public void Show()
        {
            gameObject.SetActive(true);
            _barMaterial.SetFloat(RemovedSegments, 0);
            _backgroundBarMaterial.SetFloat(RemovedSegments, 1);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        protected void SetValue(float value, float maxValue)
        {
            float valueForView = value / maxValue;
            _barMaterial.SetFloat(RemovedSegments, valueForView);
        }
    }
}