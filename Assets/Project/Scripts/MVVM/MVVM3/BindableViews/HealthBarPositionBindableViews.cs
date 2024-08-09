using System.Reflection;
using Build.Game.Scripts;
using MVVM;
using R3;
using UnityEngine;

namespace Project.Game.Scripts.MVVM.MVVM1.UI.Views.BindableViews
{
    public class HealthBarPositionBindableViews : MonoBehaviour, IBindableView<float>
    {
        public ReactiveProperty<float> OnBind()
        {
            return null;
            //return new ReactiveProperty<float>(this, GetType().GetProperty("TargetHealth", BindingFlags.Instance | BindingFlags.NonPublic));
        }
    }
}