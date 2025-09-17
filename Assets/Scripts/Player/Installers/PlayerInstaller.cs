using Zenject;

namespace Player
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<PlayerLoadingTask>().AsSingle();
        }
    }
}