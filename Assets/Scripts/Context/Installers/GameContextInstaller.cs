using GameStates;
using Installers;
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
            GameStateInstaller.Install(Container);
            InputInstaller.Install(Container);
        }
    }
}