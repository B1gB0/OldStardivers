using System;
using MVVM;
using Project.Game.Scripts.MVVM.MVVM1.UI.ViewModels;
using UnityEngine;

namespace Project.Game.Scripts.MVVM.MVVM1.UI.Views
{
    public class View : MonoBehaviour, IView, IDisposable
    {
        private IViewModel _viewModel;
        private Binder _binder;

        private void OnEnable()
        {
            _viewModel?.Enable();
        }

        private void OnDisable()
        {
            _viewModel?.Disable();
        }

        private void Construct(IViewModel viewModel, Binder binder)
        {
            Hide();
            
            _viewModel = viewModel;
            _binder = binder;
            _binder.Bind(this, viewModel);
            
            Show();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _binder.Unbind(this, _viewModel);
        }
    }
}