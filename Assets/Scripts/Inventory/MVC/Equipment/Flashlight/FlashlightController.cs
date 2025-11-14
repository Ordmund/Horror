using Core.MVC;

namespace Inventory
{
    public class FlashlightController : BaseController<FlashlightView, EmptyModel>, IInventoryItem, IEquipableItem
    {
        public FlashlightController(FlashlightView view, EmptyModel model) : base(view, model)
        {
        }

        public ItemType Type => ItemType.Flashlight;
        
        public bool IsAvailable { get; }

        public void Equip()
        {
            View.Equip();
        }

        public void Unequip()
        {
            View.Unequip();
        }

        public void Use()
        {
            View.Use();
        }
    }
}