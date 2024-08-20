using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Card _card;

    [SerializeField] private Image _icon;
    
    [SerializeField] private Text _label;
    [SerializeField] private Text _description;
    [SerializeField] private Text _level;
    [SerializeField] private Text _characteristics;

    private void Start()
    {
        _icon.sprite = _card.Icon;
        _label.text = _card.Label;
        _description.text = _card.Description;
        _level.text = _card.Level;
        _characteristics.text = _card.Characteristics;
    }
}
