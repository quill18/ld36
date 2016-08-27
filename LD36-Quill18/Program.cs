using System;
using System.Threading;

namespace LD36Quill18
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Game game = new Game();

            Intro intro = new Intro();
            //intro.Play();




            while(game.Update())
            {
                // Tiny nap to stop the CPU from melting.
                Thread.Sleep(10);
            }
       
        }
    }
}
