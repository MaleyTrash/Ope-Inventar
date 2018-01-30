using SQLite;

namespace Inventar
{
    class SavedItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
    }
}
