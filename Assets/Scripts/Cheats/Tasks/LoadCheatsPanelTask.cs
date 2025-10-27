using System.Threading.Tasks;
using Constants;
using Core.MVC;
using Core.Tasks;

namespace Cheats
{
    public class LoadCheatsPanelTask : AsyncTask
    {
        private readonly IGameObjectMVCFactory _gameObjectMvcFactory;
        
        private CheatsPanelController _cheatsPanelController;
        
        public LoadCheatsPanelTask(IGameObjectMVCFactory gameObjectMvcFactory)
        {
            _gameObjectMvcFactory = gameObjectMvcFactory;
        }
        
        public override Task Execute()
        {
            return Load();
        }

        private async Task Load()
        {
            _cheatsPanelController = await _gameObjectMvcFactory.InstantiateAndBindAsync<CheatsPanelController, CheatsPanelView, EmptyModel>(AddressablesPaths.CheatsPanelPrefab);
        }
    }
}