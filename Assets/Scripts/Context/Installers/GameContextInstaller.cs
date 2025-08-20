using Characters.Player;
using Installers;
using World;
using Zenject;

namespace Context
{
    public class GameContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallGameInstallers();
            
            BindGameContext();
        }

        private void BindGameContext()
        {
            Container.BindInterfacesTo<GameContext>().AsSingle().NonLazy();
        }

        private void InstallGameInstallers()
        {
            InputInstaller.Install(Container);
            WorldInstaller.Install(Container);
            PlayerInstaller.Install(Container);
        }
    }
}