using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiWroclaw
{
    class Pair
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Pair(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Pair))
                return false;
            Pair s = obj as Pair;
            return (s.X == this.X && s.Y == this.Y);
        }
    }
}
