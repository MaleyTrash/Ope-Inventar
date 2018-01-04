using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLite;

namespace Inventar
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SQLiteConnection db = new SQLiteConnection("db.db");
            db.CreateTable<SavedItem>();

            Inventory inv = new Inventory(10,10);
            InventoryItem itm = new InventoryItem("XD", ItemType.Ammo, 3, 3);
            InventoryItem itm2 = new InventoryItem("XD", ItemType.Ammo, 4, 4);
            Debug.WriteLine(inv.AddItem(itm, 0, 0, Color.FromRgb(200,0,0)));
            Debug.WriteLine(inv.AddItem(itm2, 5, 0, Color.FromRgb(0,200,0)));
            XD.Children.Add(inv.grid);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }

    class SavedItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
    }
}
