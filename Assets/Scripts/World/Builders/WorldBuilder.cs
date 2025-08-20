using Characters.Player;

namespace World
{
    public class WorldBuilder : IWorldBuilder
    {
        private readonly PlayerController.Factory _playerControllerFactory;

        public WorldBuilder(PlayerController.Factory playerControllerFactory)
        {
            _playerControllerFactory = playerControllerFactory;
        }
        
        public void BuildWorld()
        {
            BuildCharacter();
        }

        private void BuildCharacter()
        {
            _playerControllerFactory.Create();
        }
    }
}