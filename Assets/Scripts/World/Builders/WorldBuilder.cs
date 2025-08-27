using Characters.Player;
using Core.MVC;

namespace World
{
    public class WorldBuilder : IWorldBuilder
    {
        private readonly IGameObjectMVCFactory _gameObjectMvcFactory;

        public WorldBuilder(IGameObjectMVCFactory gameObjectMvcFactory)
        {
            _gameObjectMvcFactory = gameObjectMvcFactory;
        }
        
        public void BuildWorld()
        {
            BuildCharacter();
        }

        private void BuildCharacter()
        {
            //TODO actually I dont need nere a player controller, it's enough only to instantiate?
            var playerController = _gameObjectMvcFactory.InstantiateAndBind<PlayerController, PlayerView, PlayerModel>("Player/Player");
        }
    }
}