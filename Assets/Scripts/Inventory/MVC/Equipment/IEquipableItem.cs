namespace Inventory
{
    public interface IEquipableItem
    {
        ItemType Type { get; }
        bool IsAvailable { get; }

        void Equip();
        void Unequip();
        void Use();
    }
}