using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;
using System;

namespace Inventar
{
    class Inventory
    {
        private List<InventoryActor> _inv;
        private short _width;
        private short _height;

        public Grid grid;
        public Inventory(short width, short height)
        {
            _inv = new List<InventoryActor>();
            _width = width;
            _height = height;
            CreateGrid();
        }

        private Grid CreateGrid()
        {
            grid = new Grid();
            grid.ShowGridLines = true;
            grid.AllowDrop = true;
            grid.Background = new SolidColorBrush(Colors.Transparent);
            
            for(int i = 0; i < _height; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for(int i = 0; i < _width; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            grid.Drop += (sender, e) =>
            {
                
                InventoryActor origin;
                try
                {
                    origin = (InventoryActor)e.Data.GetData(typeof(InventoryActor));
                }
                catch
                {
                    return;
                }
                Tuple<short, short> pos = GetGridPosition(e);
                Move(origin, pos.Item1, pos.Item2);
            };
            return grid;
        }

        private Tuple<short, short> GetGridPosition(DragEventArgs e)
        {
            var point = e.GetPosition(grid);
            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in grid.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calc col mouse was over
            foreach (var columnDefinition in grid.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }
            return new Tuple<short, short>((short)col, (short)row);
        }

        public bool IsPlaceable(InventoryItem item, short x, short y, InventoryActor ignoreEl = null)
        {
            if (x < 0 || x + item.width > _width || y < 0 || y + item.height > _height) return false;
            List<int[]> positions = InventoryActor.GetItemPoitions(item,x,y);
            foreach(InventoryActor i in _inv)
            {
                if (i.Equals(ignoreEl)) continue;
                if (i.GetPositions().Exists(pos => positions.Exists(itemPos => itemPos[0] == pos[0] && itemPos[1] == pos[1]))) return false;
            }
            return true;
        }

        public bool Move(InventoryActor item, short x, short y)
        {
            InventoryActor found = _inv.Find(i => i.Equals(item));
            if (found == null) return false;
            if (!IsPlaceable(item.parent, x, y, item)) return false;
            found.X = x;
            found.Y = y;
            return true;
        }

        public bool AddItem(InventoryItem item, short x, short y, Color color)
        {
            if (!IsPlaceable(item, x, y)) return false;
            InventoryActor temp = new InventoryActor(item, x, y, color);
            _inv.Add(temp);
            grid.Children.Add(temp.rect);
            return true;
        }

        public void Save(SQLite.SQLiteConnection db)
        {
            List<SavedItem> items = new List<SavedItem>();
            foreach(InventoryActor item in _inv)
            {
                items.Add(new SavedItem() { Name = item.parent.name, Type = item.parent.type, Height = item.parent.height, Width = item.parent.width, X = item.X, Y = item.Y, R = item.Color.R, B = item.Color.B, G = item.Color.G });
            }
            db.InsertAll(items);
        }
    }
}
