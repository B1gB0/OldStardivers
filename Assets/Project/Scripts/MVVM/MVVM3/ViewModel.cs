namespace Project.Game.Scripts.MVVM.MVVM1.UI.ViewModels
{
    public abstract class ViewModel<T> : IViewModel
    {
        public T Model { get; }

        protected ViewModel(T model)
        {
            Model = model;
        }

        public abstract void Enable();

        public abstract void Disable();
    }
}