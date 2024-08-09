using Zenject;

namespace DI
{
    public class ViewModelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MoneyViewModel>().AsSingle().NonLazy();
        }
    }
}