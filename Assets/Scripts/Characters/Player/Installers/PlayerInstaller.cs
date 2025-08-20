using Core.MVC;
using Zenject;

namespace Characters.Player
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            //Container.BindFactory<PlayerController, PlayerController.Factory>().FromFactory<GameObjectFactory<PlayerController>>();
        }
    }
}