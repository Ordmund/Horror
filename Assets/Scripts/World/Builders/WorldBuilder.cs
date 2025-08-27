using Characters.Player;
using Core.MVC;
using Providers;

namespace World
{
    public class WorldBuilder : IWorldBuilder
    {
        private readonly IGameObjectMVCFactory _gameObjectMvcFactory;
        private readonly IPrefabPathProvider _prefabPathProvider;

        public WorldBuilder(IGameObjectMVCFactory gameObjectMvcFactory, IPrefabPathProvider prefabPathProvider)
        {
            _gameObjectMvcFactory = gameObjectMvcFactory;
        }
        
        public void BuildWorld()
        {
            BuildCharacter();
        }

        private void BuildCharacter()
        {
            var playerPrefabPath = _prefabPathProvider.GetPathByViewType<PlayerView>(); //TODO I need to specify view two times or move to core?
            var playerController = _gameObjectMvcFactory.InstantiateAndBind<PlayerController, PlayerView, PlayerModel>(playerPrefabPath);
        }
    }
}