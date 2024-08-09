using System;
using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts;
using Build.Game.Scripts.ECS.Data;
using Build.Game.Scripts.Game.Gameplay;
using UnityEngine;
using Zenject;

public class HealthPlayerSceneInstaller : MonoInstaller
{
    [SerializeField] private Health _health;

    public override void InstallBindings()
    {
        Container.Bind<Health>().FromResolve().AsSingle().NonLazy();
    }
}
