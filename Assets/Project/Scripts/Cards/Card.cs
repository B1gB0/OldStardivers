using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Cards/New Card")]
public class Card : ScriptableObject
{
    [field: SerializeField] public String Label { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    
    [field: SerializeField] public String Characteristics { get; private set; }
    [field: SerializeField] public String Level { get; private set; }
    
    [field: SerializeField] public String Description { get; private set; }
}
