using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace FlappyBird.Model
{
    public class Box
    {
        //Fields
        Coord coords = new Coord(-550, 0);
        Rectangle rec = new Rectangle();

        //Constructor
        public Box()
        {
            Rec.Height = 30;
            Rec.Width = 30;
            Rec.Fill = Brushes.Red;
            Rec.Margin = new Thickness(coords.X, coords.Y, 0, 0);
        }

        //Properties
        public Rectangle Rec { get => rec; set => rec = value; }
        internal Coord Coords { get => coords; set => coords = value; }

        public void MoveUp(int MoveUpAmount)
        {
            coords.Y -= MoveUpAmount;
            Rec.Margin = new Thickness(coords.X, coords.Y, 0, 0);
        }

        public void MoveDown(int MoveDownAmount)
        {
            coords.Y += MoveDownAmount;
            Rec.Margin = new Thickness(coords.X, coords.Y, 0, 0);
        }
    }
}
