using System.Collections;
using System.Collections.Generic;
using MVVM;
using UnityEngine;
using Zenject;

public class BindersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BinderFactory.RegisterBinder<TextBinder>();
    }
}
