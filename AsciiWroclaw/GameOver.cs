using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiWroclaw
{
    class GameOver
    {
        
        AnimateWindow endAnimation;
        private bool animation;
        private int titleOffset = 1;
        private short defaultColor = 14;
        public GameOver()
        {
            animation = true;
            endAnimation = new AnimateWindow(
            new string[][] { Strings.GameOver,Strings.Credits,Strings.Maciek,Strings.Wasyl },
            new int[] { 10,5,5,5});
            endAnimation.OnAnimationFinished += endAnimation_OnAnimationFinished;
        }
        public void Render()
        {
            if (animation)
            {
                endAnimation.Render();
            }
            else
            {
                drawTitle();
                drawCredits();
            }
        }
        private void drawCredits()
        {
            int i = titleOffset + 15;
            foreach (var line in Strings.Credits)
            {
                int start = (buffer.WindowWidth - line.Length) / 2;
                buffer.Draw(line, start, i, defaultColor);
                i++;
            }
            i += 5;
            foreach (var line in Strings.Maciek)
            {
                int start = (buffer.WindowWidth - line.Length) / 2;
                buffer.Draw(line, start, i, defaultColor);
                i++;
            }
            i += 5;
            foreach (var line in Strings.Wasyl)
            {
                int start = (buffer.WindowWidth - line.Length) / 2;
                buffer.Draw(line, start, i, defaultColor);
                i++;
            }
        }
        private void drawTitle()
        {
           int i = titleOffset;
            foreach (var line in Strings.GameOver)
            {
                int start = (buffer.WindowWidth - line.Length) / 2;
                buffer.Draw(line,start,i,defaultColor);
                i++;
            }
        }
        public void Update()
        {
            if (animation)
            {
                endAnimation.Update();
            }
        }
        void endAnimation_OnAnimationFinished()
        {
            animation = false;
        }
    }
}
