using System;
using UnityEngine;

namespace Project.Game.Scripts.MVVM
{
    public class Property : IBindable
    {
        private ViewModel _target;
        private Func<object, object> _getter;

        public string Name { get; private set; }
        
        public event Action<Property> Changed;

        public Property(ViewModel target, Func<object, object> getter, string name)
        {
            _target = target;
            _getter = getter;
            Name = $"{target.GetType().Name}.{name}";
        }

        public object Get()
        {
            return _getter.Invoke(_target);
        }

        public void OnChanged()
        {
            Changed.Invoke(this);
        }
    }
}