using Zenject;

namespace GameStates
{
    public class GameStateInstaller : Installer<GameStateInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }
    }
}