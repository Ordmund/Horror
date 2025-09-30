using System.Threading.Tasks;
using Camera;
using Constants;
using Core.MVC;
using Core.Tasks;

namespace Player
{
    public class LoadPlayerTask : AsyncTask
    {
        private readonly IGameObjectMVCFactory _gameObjectMvcFactory;
        
        public LoadPlayerTask(IGameObjectMVCFactory gameObjectMvcFactory)
        {
            _gameObjectMvcFactory = gameObjectMvcFactory;
        }

        public override Task Execute()
        {
            return Load();
        }
        
        private async Task Load()
        {
            //var firstPersonCameraController = await _gameObjectMvcFactory.InstantiateAndBindAsync<FirstPersonCameraController, FirstPersonCameraView, FirstPersonCameraModel>(AddressablesPaths.FirstPersonCameraPrefab);
            var playerController = await _gameObjectMvcFactory.InstantiateAndBindAsync<PlayerController, PlayerView, PlayerModel>(AddressablesPaths.PlayerPrefab);
        }
    }
}