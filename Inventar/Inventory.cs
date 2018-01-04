using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
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
                Tuple<short, short> pos = GetGridPosition();
                Move(origin, pos.Item1, pos.Item2);
            };
            return grid;
        }

        private Tuple<short, short> GetGridPosition()
        {
            var point = Mouse.GetPosition(null);
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
            int[] match = new int[] { x, y };
            foreach(InventoryActor i in _inv)
            {
                if (i.Equals(ignoreEl)) continue;
                if (i.GetPositions().Exists(var => var[0] == x && var[1] == y)) return false;
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

        public bool AddItem(InventoryItem item, short x, short y)
        {
            if (!IsPlaceable(item, x, y)) return false;
            InventoryActor temp = new InventoryActor(item, x, y);
            _inv.Add(temp);
            grid.Children.Add(temp.rect);
            return true;
        }
    }

    class InventoryActor
    {

        public InventoryItem parent;
        private short x;
        private short y;
        public short X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                Grid.SetColumn(rect, x);
            }
        }
        public short Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                Grid.SetColumn(rect, x);
            }
        }
        public Rectangle rect;

        public InventoryActor(InventoryItem item, short x, short y)
        {
            this.parent = item;
            rect = new Rectangle();
            rect.Fill = new SolidColorBrush(Color.FromRgb(0, 111, 111));
            this.X = x;
            this.Y = y;
            Grid.SetColumnSpan(rect, item.width);
            Grid.SetRowSpan(rect, item.height);
            rect.MouseMove += (sender, e) =>
            {
                Rectangle r = sender as Rectangle;
                if (r != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(r,this,DragDropEffects.Move);
                }
            };
        }

        public List<int[]> GetPositions()
        {
            List<int[]> ret = new List<int[]>();
            for (int i = X; i < X + parent.width; i++)
            {
                for (int z = Y; z < Y + parent.height; z++)
                {
                    ret.Add(new int[] { X, z });
                }
            }
            return ret;
        }
    }
}
