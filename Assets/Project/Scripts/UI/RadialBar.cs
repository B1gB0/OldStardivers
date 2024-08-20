using TMPro;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class RadialBar : MonoBehaviour, IView
    {
        private const string Value = "_RemovedSegments";
        
        private readonly int RemovedSegments = Shader.PropertyToID(Value);
        
        [SerializeField] private Material _backgroundBarMaterial;
        [SerializeField] private Material _barmaterial;
        [SerializeField] private TMP_Text _text;

        public void Show()
        {
            gameObject.SetActive(true);
            _barmaterial.SetFloat(RemovedSegments, 0);
            _backgroundBarMaterial.SetFloat(RemovedSegments, 1);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        protected void SetValue(float value, float maxValue)
        {
            float valueForView = value / maxValue;
            Debug.Log(valueForView);
            _barmaterial.SetFloat(RemovedSegments, valueForView);
        }
    }
}