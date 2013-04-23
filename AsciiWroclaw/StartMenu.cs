using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiWroclaw
{
    class StartMenu
    {
        public delegate void MenuOptionClickedHandler(Logic.GameState newState);
        public event MenuOptionClickedHandler OnMenuOptionClick;

        private int currentPosition = 0;
        private int optionsCount = 2;
        private int titleOffset = 1;
        private int titleHeight = Strings.Title.Length;
        private int buttonsOffset = 5;
        private short defaultColor = 14; // yellow
        private short activeButtonColor = 4; // red
        enum Option { START = 0 , EXIT = 1 } ;
        Option[] options = { Option.START, Option.EXIT };
        public StartMenu()
        {
            KeyState.OnKeyPressed += KeyState_OnKeyPressed;
        }

        void KeyState_OnKeyPressed(System.Windows.Forms.Keys key)
        {
            switch (key)
            {
                case System.Windows.Forms.Keys.Up:
                    {
                        if (currentPosition > 0)
                            currentPosition--;
                        break;
                    }
                case System.Windows.Forms.Keys.Down:
                    {
                        if (currentPosition < optionsCount-1)
                            currentPosition++;
                        break;
                    }
                case System.Windows.Forms.Keys.Enter:
                    {
                        switch (currentPosition)
                        {
                            case ((int)Option.START):
                                OnMenuOptionClick(Logic.GameState.GAME);
                                break;
                            case ((int)Option.EXIT):
                                OnMenuOptionClick(Logic.GameState.EXIT);
                                KeyState.OnKeyPressed -= KeyState_OnKeyPressed;
                                break;
                        }
                        break;
                    }
            }
        }
       
        private void DrawTitle()
        {
            int i = titleOffset;
            foreach (var line in Strings.Title)
            {
                int start = (buffer.WindowWidth - line.Length) / 2;
                buffer.Draw(line,start,i,defaultColor);
                i++;
            }
        }
        private void DrawButtons()
        {
            int starty = titleOffset + titleHeight + buttonsOffset;
            
            foreach (var line in Strings.Play)
            {
                int x = (buffer.WindowWidth - line.Length) / 2;
                short color = defaultColor;
                if (currentPosition == (int)Option.START)
                    color = activeButtonColor;
                buffer.Draw(line, x, starty, color);
                starty++;
            }
            starty += buttonsOffset;
            foreach (var line in Strings.Exit)
            {
                int x = (buffer.WindowWidth - line.Length) / 2;
                short color = defaultColor;
                if (currentPosition == (int)Option.EXIT)
                    color = activeButtonColor;
                buffer.Draw(line, x, starty, color);
                starty++;
            }
        }
        public void Render()
        {
            DrawTitle();
            DrawButtons();
        }
    }
}
