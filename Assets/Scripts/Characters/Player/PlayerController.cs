using Core.MVC;

namespace Characters.Player
{
    public class PlayerController : BaseController<PlayerView, PlayerModel>
    {
        public PlayerController(PlayerView view, PlayerModel model) : base(view, model)
        {
        }
    }
}