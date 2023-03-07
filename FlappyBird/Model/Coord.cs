using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Model
{
    class Coord
    {
        int x;
        int y;

        public Coord()
        {

        }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }


    }
}
