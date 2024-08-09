using System;
using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts.ECS.EntityActors;
using MVVM;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

public sealed class MoneyViewModel : IInitializable, IDisposable
{
    [Data("Currency")]
    public ReactiveProperty<string> Money = new ReactiveProperty<string>();
    
    private readonly MoneyStorage _moneyStorage;

    public MoneyViewModel(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }

    public void Initialize()
    {
        OnMoneyChanged(_moneyStorage.Money);
        _moneyStorage.OnStateChanged += OnMoneyChanged;
    }

    public void Dispose()
    {
        _moneyStorage.OnStateChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        Money.Value = money + "$";
    }
}
