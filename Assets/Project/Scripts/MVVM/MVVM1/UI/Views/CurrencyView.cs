using System.Collections;
using System.Collections.Generic;
using MVVM;
using Project.Game.Scripts.MVVM;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    [Data("Currency")]
    [SerializeField] private Text _currencyText;
}
