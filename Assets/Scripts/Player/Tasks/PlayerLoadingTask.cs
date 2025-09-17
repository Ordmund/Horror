using System.Threading.Tasks;
using Core.MVC;
using Core.Tasks;

namespace Player
{
    public class PlayerLoadingTask : AsyncTask
    {
        private readonly IGameObjectMVCFactory _gameObjectMvcFactory;
        
        public PlayerLoadingTask(IGameObjectMVCFactory gameObjectMvcFactory)
        {
            _gameObjectMvcFactory = gameObjectMvcFactory;
        }
        
        public override async Task Execute()
        {
            await Load();
        }
        
        private async Task Load()
        {
            var playerController = await _gameObjectMvcFactory.InstantiateAndBindAsync<PlayerController, PlayerView, PlayerModel>(); //TODO need to unload on state change
        }
    }
}