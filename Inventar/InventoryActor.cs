using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Inventar
{
    class InventoryActor
    {
        private Color DEFAULT_COLOR = Color.FromRgb(0, 111, 111);
        private Color currentColor;

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
                Grid.SetRow(rect, y);
            }
        }
        public Color Color
        {
            set
            {
                rect.Fill = new SolidColorBrush(value);
                currentColor = value;
            }
            get
            {
                return currentColor;
            }
        }

        public Rectangle rect;

        public InventoryActor(InventoryItem item, short x, short y, Color color = new Color())
        {
            if (color.Equals(new Color())) color = DEFAULT_COLOR;
            this.parent = item;
            rect = new Rectangle();
            Color = color;
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
            return GetItemPoitions(parent, X, Y);
        }

        public static List<int[]> GetItemPoitions(InventoryItem item, int x, int y)
        {
            List<int[]> ret = new List<int[]>();
            for (int i = x; i < x + item.width; i++)
            {
                for (int z = y; z < y + item.height; z++)
                {
                    ret.Add(new int[] { i, z });
                }
            }
            return ret;
        }
    }
}
