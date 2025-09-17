using Core.MVC;

namespace Player
{
    public class PlayerController : BaseController<PlayerView, PlayerModel>
    {
        public PlayerController(PlayerView view, PlayerModel model) : base(view, model)
        {
        }
    }
}