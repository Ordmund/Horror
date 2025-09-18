using Zenject;

namespace World
{
    public class WorldInstaller : Installer<WorldInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<LoadWorldTask>().AsSingle();
        }
    }
}