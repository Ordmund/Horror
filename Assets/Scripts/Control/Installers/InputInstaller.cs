using Controllers.InputControllers;
using Notifiers;
using Zenject;

namespace Installers
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<InputController>();
            
            Container.Bind<IInputNotifier>().To<InputNotifier>().AsSingle();
        }
    }
}