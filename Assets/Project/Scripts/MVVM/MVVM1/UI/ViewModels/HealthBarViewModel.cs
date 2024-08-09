using System;
using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts;
using MVVM;
using Project.Game.Scripts.MVVM.MVVM1.UI.ViewModels;
using Project.Game.Scripts.MVVM.MVVM1.UI.Views.BindableViews;
using Project.Scripts.MVVM.Attributes;
using R3;
using UnityEngine;
using Zenject;

public class HealthBarViewModel : ViewModel<Health>
{
    [ComponentBinding(typeof(HealthBarPositionBindableViews))]
    public ReactiveProperty<float> CurrentHealth;

    [ComponentBinding(typeof(HealthBarPositionBindableViews))]
    public ReactiveProperty<float> MaxHealth;

    [ComponentBinding(typeof(HealthBarPositionBindableViews))]
    public ReactiveProperty<float> TargetHealth;

    public HealthBarViewModel(Health model) : base(model) { }

    public override void Enable()
    {
        //Model.HealthChanged += OnChangedHealth;
    }

    public override void Disable()
    {
        //Model.HealthChanged -= OnChangedHealth;
    }
    
    public void OnChangedHealth(float currentHealth, float maxHealth, float targetHealth)
    {
        CurrentHealth.Value = currentHealth;
        MaxHealth.Value = maxHealth;
        TargetHealth.Value = targetHealth;
    }
}
