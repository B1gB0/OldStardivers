using R3;

namespace MVVM
{
    public interface IBindableView
    {
        object OnBind();
    }

    public interface IBindableView<T> : IBindableView
    {
        ReactiveProperty<T> OnBind();
        
        object IBindableView.OnBind() => OnBind();
    }
}