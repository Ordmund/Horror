using Characters.Player;
using Core.MVC;

namespace World
{
    public class WorldBuilder : IWorldBuilder
    {
        private readonly IMVCFactory _mvcFactory;
        //private readonly PlayerController.Factory _playerControllerFactory;

        public WorldBuilder(IMVCFactory mvcFactory)
        {
            _mvcFactory = mvcFactory;
            //_playerControllerFactory = playerControllerFactory;
        }
        
        public void BuildWorld()
        {
            BuildCharacter();
        }

        private void BuildCharacter()
        {
            var player = _mvcFactory.InstantiateAndBind<PlayerController, PlayerView, PlayerModel>("Player/Player");
            //_playerControllerFactory.Create();
        }
    }
}