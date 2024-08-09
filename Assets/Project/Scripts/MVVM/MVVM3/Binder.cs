using System.Reflection;
using Project.Game.Scripts.MVVM.MVVM1.UI.ViewModels;
using Project.Game.Scripts.MVVM.MVVM1.UI.Views;
using Project.Scripts.MVVM.Attributes;
using UnityEngine;

namespace MVVM
{
    public class Binder
    {
        private static readonly BindingFlags _fieldBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
        
        public void Bind(View view, IViewModel viewModel)
        {
            foreach (FieldInfo fieldInfo in viewModel.GetType().GetFields())
            {
                foreach (object attribute in fieldInfo.GetCustomAttributes(true))
                {
                    if (attribute is ComponentBindingAttribute bindingAttribute)
                    {
                        Component component = view.GetComponent(bindingAttribute.ComponentType);

                        if (component is IBindableView bindableView)
                        {
                            fieldInfo.SetValue(viewModel, bindableView.OnBind());
                        }
                    }
                }
            }
        }

        public void Unbind(View view, IViewModel viewModel)
        {
            
        }
    }
}