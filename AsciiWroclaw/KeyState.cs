using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsciiWroclaw
{
    class KeyState
    {
        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);

        public delegate void KeyPressedEvent(Keys key);

        public static event KeyPressedEvent OnKeyPressed;

        static Keys[] usedKeys = { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Escape, Keys.Enter };

        static Dictionary<Keys, int> isPressed;
        static Dictionary<Keys, bool> wasPressed;

        public KeyState()
        {
            isPressed = new Dictionary<Keys, int>();
            wasPressed = new Dictionary<Keys, bool>();
            foreach (var key in usedKeys)
            {
                //Wsadzic wszystkie klucze do slownika zeby nie sprawdzac potem czy istnieja
                isPressed[key] = 0;
                wasPressed[key] = false;
            }
       }

        public static bool IsKeyPushedDown(System.Windows.Forms.Keys vKey)
        {
            return 0 != (GetAsyncKeyState((int)vKey) & 0x8000);
        }

        public static void getKeys()
        {
            foreach (var key in usedKeys)
            {
                if (IsKeyPushedDown(key))
                {
                    isPressed[key]++;
                    if(isPressed[key] > 25000)
                    {
                        OnKeyPressed(key);
                        isPressed[key] = 0;
                        wasPressed[key] = true;
                    };
                }
                else
                    if (isPressed[key] > 0)
                    {
                        if (!wasPressed[key])
                            OnKeyPressed(key);
                        isPressed[key] = 0;
                    }
            }
        }
    }
}