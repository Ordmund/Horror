using Zenject;

namespace GameInput
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<InputController>().AsSingle();
            
            Container.Bind<IInputNotifier>().To<InputNotifier>().AsSingle();
        }
    }
}