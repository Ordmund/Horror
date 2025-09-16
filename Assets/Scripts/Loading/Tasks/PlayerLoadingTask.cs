using System.Collections;
using Characters.Player;
using Core.Managers.Injectable;
using Core.MVC;

namespace Loading.Tasks
{
    public class PlayerLoadingTask : Task
    {
        private readonly IGameObjectMVCFactory _gameObjectMvcFactory;

        public PlayerLoadingTask(IGameObjectMVCFactory gameObjectMvcFactory)
        {
            _gameObjectMvcFactory = gameObjectMvcFactory;
        }

        public override IEnumerator Execute()
        {
            Load();
            
            yield break;
        }

        private void Load()
        {
            var playerController = _gameObjectMvcFactory.InstantiateAndBind<PlayerController, PlayerView, PlayerModel>();
        }
    }
}