using Cheats.Cheats;
using Zenject;

namespace Cheats
{
    public class CheatsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<LoadCheatsPanelTask>().AsSingle();
            
            Container.Bind<ICheatsPanelNotifier>().To<CheatsPanelNotifier>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CheatsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<CheatsContext>().AsSingle().NonLazy();
        }
    }
}