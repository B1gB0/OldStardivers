using System;
using UnityEngine;

namespace Project.Game.Scripts.MVVM
{
    public class Command : IBindable
    {
        private ViewModel _target;
        private Func<object, object[], object> _method;

        private static object[] _emptyArguments = new object[0];

        public string Name { get; private set; }

        public Command(ViewModel target, Func<object, object[], object> method, string name)
        {
            _target = target;
            _method = method;
            Name = $"{target.GetType().Name}.{name}";
        }

        public void Do()
        {
            _method.Invoke(_target, _emptyArguments);
        }
    }
}