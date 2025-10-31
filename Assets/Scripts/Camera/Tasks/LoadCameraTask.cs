using System.Threading.Tasks;
using Constants;
using Core.MVC;
using Core.Tasks;

namespace Camera
{
    public class LoadCameraTask : AsyncTask
    {
        private readonly IGameObjectMVCFactory _gameObjectMvcFactory;

        public LoadCameraTask(IGameObjectMVCFactory gameObjectMvcFactory)
        {
            _gameObjectMvcFactory = gameObjectMvcFactory;
        }

        public override Task Execute()
        {
            return Load();
        }

        private async Task Load()
        {
            _ = _gameObjectMvcFactory.FindObjectAndBind<ThirdPersonCameraController, ThirdPersonCameraView, ThirdPersonCameraModel>();
            _ = await _gameObjectMvcFactory.InstantiateAndBindAsync<FirstPersonCameraController, FirstPersonCameraView, FirstPersonCameraModel>(AddressablesPaths.FirstPersonCameraPrefab);
        }
    }
}