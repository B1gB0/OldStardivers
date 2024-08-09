using System;
using System.Reflection;

namespace MVVM
{
    public class ObservableProperty<T>
    {
        private readonly object _target;
        private readonly PropertyInfo _propertyInfo;

        public ObservableProperty(object target, PropertyInfo propertyInfo)
        {
            _target = target;
            _propertyInfo = propertyInfo;
        }

        public event Action Changed;

        public void SetValue(T value)
        {
            _propertyInfo.SetValue(_target, value);
            Changed?.Invoke();
        }

        public T Value => (T) _propertyInfo.GetValue(_target);
    }
}