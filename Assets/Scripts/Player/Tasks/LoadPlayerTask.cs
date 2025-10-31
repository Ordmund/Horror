using System.Threading.Tasks;
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
            _ = await _gameObjectMvcFactory.InstantiateAndBindAsync<PlayerController, PlayerView, PlayerModel>(AddressablesPaths.PlayerPrefab);
        }
    }
}