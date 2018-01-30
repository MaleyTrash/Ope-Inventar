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

        public Item parent;
        private short x;
        private short y;
        public short width { get; }
        public short height { get; }
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

        public InventoryActor(Item item, short x, short y, short width, short height, Color color = new Color())
        {
            rect = new Rectangle();
            if (color.Equals(new Color()))
            {
                color = DEFAULT_COLOR;
            }
            this.parent = item;
            Color = color;
            this.X = x;
            this.Y = y;
            this.width = width;
            this.height = height;
        }

        public void Init()
        {
            Grid.SetColumnSpan(rect, width);
            Grid.SetRowSpan(rect, height);
            rect.MouseMove += (sender, e) =>
            {
                Rectangle r = sender as Rectangle;
                if (r != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(r, this, DragDropEffects.Move);
                }
            };
        }

        public List<int[]> GetPositions()
        {
            return GetItemPoitions(this, X, Y);
        }

        public static List<int[]> GetItemPoitions(InventoryActor item, int x, int y)
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
