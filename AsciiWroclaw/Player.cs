using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiWroclaw
{
    class Player
    {
        public int positionX;
        public int positionY;
        public int points;

        string[] animationChars = { "@", "O", "D" };
        int animPosition = 0;

        public Player()
        {
            positionX = 0;
            positionY = 0;
        }

        public Player(int x, int y)
        {
            positionX = x;
            positionY = y;
        }

        public void Update()
        {
            animPosition = (animPosition + 1) % animationChars.Count();
        }

        public void Render()
        {
            buffer.Draw(animationChars[animPosition], positionX, positionY, 12);
        }


    }
}
