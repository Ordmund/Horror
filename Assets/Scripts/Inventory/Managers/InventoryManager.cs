using System.Collections.Generic;
using GameStates;

namespace Inventory
{
    public class InventoryManager : IInventoryManager, ILoadable
    {
        private readonly List<IInventoryItem> _items;

        public void Load()
        {
            //Ignored
        }

        public void Reload()
        {
            //Ignored
        }
    }
}