using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlappyBird.Model
{
    class Obstacle : Box
    {
        //Fields
        Coord coords2 = new Coord(850, 220);
        Rectangle recTop = new Rectangle();
        Random random = new Random();

        //Constructor
        public Obstacle()
        {
            int Height = random.Next(5, 350);
            Coords.X = 850;
            Rec.Height = Height;
            Rec.Width = 50;
            Rec.Fill = Brushes.Green;
            Coords.Y = 350;
            Rec.Margin = new Thickness(Coords.X, Coords.Y, 0, 0);
            RecTop.Height = 700 - Height;
            coords2.Y = -450 ;
            RecTop.Width = 50;
            RecTop.Fill = Brushes.Blue;
            RecTop.Margin = new Thickness(coords2.X, coords2.Y, 0, 0);
        }

        //Properties
        public Rectangle RecTop { get => recTop; set => recTop = value; }
        internal Coord Coords2 { get => coords2; set => coords2 = value; }

        //Methods
        public void MoveLeft(int MoveLeftAmount)
        {
            Coords.X -= MoveLeftAmount;
            Coords.X -= MoveLeftAmount;
            coords2.X -= MoveLeftAmount;
            coords2.X -= MoveLeftAmount;
            Rec.Margin = new Thickness(Coords.X, Coords.Y, 0, 0);
            RecTop.Margin = new Thickness(coords2.X, coords2.Y, 0, 0);
        }
    }
}
