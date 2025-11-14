using Core.MVC;

namespace Inventory
{
    public class BatteryController : BaseController<BatteryView, EmptyModel>, IInventoryItem
    {
        public BatteryController(BatteryView view, EmptyModel model) : base(view, model)
        {
        }

        public ItemType Type => ItemType.Battery;
    }
}