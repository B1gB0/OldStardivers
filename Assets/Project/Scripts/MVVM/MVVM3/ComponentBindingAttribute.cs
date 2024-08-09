using System;

namespace Project.Scripts.MVVM.Attributes
{
    public class ComponentBindingAttribute : Attribute
    {
        public ComponentBindingAttribute(Type componentType)
        {
            ComponentType = componentType;
        }
        
        public Type ComponentType { get; }
    }
}