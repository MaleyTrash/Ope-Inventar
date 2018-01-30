namespace Inventar
{
    abstract class Item
    {
        public string name { get; set; }
        public ItemType type { get; set; }

        public Item(string name, ItemType type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
