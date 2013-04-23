using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;
namespace AsciiWroclaw
{

    class Program
    {
        static int width = 100;
        static int height = 50;

        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Ascii BoulderPlex";
            System.Console.SetWindowSize(width, height);
            System.Console.SetBufferSize(width, height);
            new buffer(width, height, width, height);
            
            Logic logic = new Logic();
            logic.Start();


        }
    }
}
