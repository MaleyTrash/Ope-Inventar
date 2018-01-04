using System.Collections.Generic;

namespace Inventar
{
    class InventoryItem : Item
    {
        public short width { get; }
        public short height { get; }

        public InventoryItem(string name, ItemType type, short width, short height) : base(name, type)
        {
            this.width = width;
            this.height = height;
        }
    }
}
