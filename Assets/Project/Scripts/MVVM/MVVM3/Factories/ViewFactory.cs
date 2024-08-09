using System;
using System.Reflection;
using Project.Game.Scripts.MVVM.MVVM1.UI.ViewModels;
using Binder = MVVM.Binder;

namespace Project.Game.Scripts.MVVM.MVVM1.UI.Views.Factories
{
    public class ViewFactory
    {
        private static readonly string _constuctorMethodName = "Construct";
        private static readonly BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
        
        private readonly PrefabViewFactory _prefabViewFactory = new PrefabViewFactory();
        private readonly Binder _binder = new Binder();

        public TView Create<TView, TViewModel>(TViewModel viewModel)
            where TView : View
            where TViewModel : IViewModel
        {
            TView view = _prefabViewFactory.Create<TView>();
            
            MethodInfo methodInfo = typeof(View).GetMethod(_constuctorMethodName, _bindingFlags);
            methodInfo.Invoke(view, new object[] {viewModel, _binder});

            return view;
        }
    }
}