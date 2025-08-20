using Core.MVC;
using Zenject;

namespace Characters.Player
{
    public class PlayerController : BaseController<PlayerView, PlayerModel>
    {
        public PlayerController(PlayerView view, PlayerModel model) : base(view, model)
        {
        }
        
        public class Factory : PlaceholderFactory<PlayerController>
        {
        }
    }
}