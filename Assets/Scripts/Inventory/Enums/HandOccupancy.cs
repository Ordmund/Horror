namespace Inventory
{
    public enum HandOccupancy
    {
        None = 0,
        Any = 1 << 0,
        Left = 1 << 1,
        Right = 1 << 2,
        Both = Left | Right
    }
}