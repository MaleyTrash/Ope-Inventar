using System;
using System.Collections.Generic;
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
        Inventory _inv = new Inventory(10, 10);
        SQLiteConnection _db = new SQLiteConnection("db.db");
        public MainWindow()
        {
            InitializeComponent();
            _db.CreateTable<SavedItem>();

            List<SavedItem> items = new List<SavedItem>(_db.Table<SavedItem>().AsEnumerable<SavedItem>());

            foreach (SavedItem item in items)
            {
                //item.Width, item.Height
                Item temp = new Zbran(item.Name);
                _inv.AddItem(temp, item.X, item.Y, item.Width, item.Height, Color.FromRgb(item.R, item.G, item.B));
            }
            XD.Children.Add(_inv.grid);
            _inv.Save(_db);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _inv.Save(_db);
        }
    }
}
