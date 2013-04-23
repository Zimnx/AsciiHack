using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiWroclaw
{
    class AnimateWindow
    {
        public delegate void AnimationFinishedEventHandler();
        public event AnimationFinishedEventHandler OnAnimationFinished;

        int topLine;
        List<string> toDraw;

        public AnimateWindow(string[][] screen, int[] spaces)
        {
            int overallLength = screen.Aggregate<string[], int, int>(0, (acc, list) => acc += list.Length, (e) => e);
            int spaceBetween = (buffer.WindowHeight - overallLength) / (screen.Length + 1);

            toDraw = new List<string>();

            int spaceNum = 0;
            foreach (var item in screen)
            {
                toDraw.AddRange(item);
                if (spaceNum >= spaces.Count())
                    continue;
                for (int i = 0; i < spaces[spaceNum]; i++)
                {
                    toDraw.Add("");
                }
                spaceNum++;
            }

            topLine = buffer.WindowHeight - 1; // +- 1
        }    

        public void Update()
        {
             if (topLine <= 0)
                return;
            topLine -= 2;

            if (topLine <= 0)
                if (OnAnimationFinished != null)
                    OnAnimationFinished();
        }

        public void Render()
        {
            if (topLine <= 0)
                return;
            
            int left = buffer.WindowHeight - topLine;
            int lineNum = 0;
            for (int i = topLine; i < buffer.WindowHeight; i++)
            {
                string line = "";
                if (lineNum < toDraw.Count)
                    line = toDraw[lineNum];
                buffer.Draw(line, (buffer.WindowWidth - line.Length) / 2, i, 14);
                lineNum++;
            }

            if (topLine <= 0)
                if (OnAnimationFinished != null)
                    OnAnimationFinished();
        }

    }
}
