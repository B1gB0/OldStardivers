using System;
using UnityEngine;

namespace Project.Scripts.Cards.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Cards/Improvement Card")]
    public class ImprovementCard : Card
    {
        [field: SerializeField] public String TypeWeapon { get; private set; }
        
        [field: SerializeField] public String TypeCharacteristics { get; private set; }
        
        [field: SerializeField] public float Value { get; private set; }
    }
}