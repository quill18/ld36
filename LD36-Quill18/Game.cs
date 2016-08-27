using System;
using System.Collections.Generic;

namespace LD36Quill18
{
    public enum InputMode { Normal, Aiming, Inventory, Looking }

    public class Game
    {

        public Game()
        {
            if(_instance != null)
            {
                throw new Exception();
            }
            _instance = this;

            Random = new Random();
            messageLog = new List<string>();

            frameBuffer = new FrameBuffer(0, 0, Console.WindowWidth, Console.WindowHeight);
            //frameBufferStats = new FrameBuffer(Console.WindowWidth - statAreaWidth, 0, statAreaWidth, Console.WindowHeight);

            Map = new Map();

            Map.CurrentFloor.RedrawFullMap();
        }

        public Map Map { get; protected set; }

        static public Game Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Game();
                }

                return _instance;
            }
        }

        public PlayerCharacter PlayerCharacter { get; set; }

        public Random Random;

        static private Game _instance;

        #region bookkeeping
        private bool exit = false;
        private bool doTick = false;
        public  AimingOverlay aimingOverlay;
        #endregion

        private int statAreaWidth = 16;

        private FrameBuffer frameBuffer;
        //private FrameBuffer frameBufferStats;

        public InputMode InputMode = InputMode.Normal;

        private List<string> messageLog;

        /// <summary>
        /// Called non-stop by the main program loop
        /// </summary>
        public bool Update()
        {
            //RedrawRequests.Clear();

            KeyboardHandler.Update_Keyboard();

            if (InputMode == InputMode.Inventory)
            {
                DrawInventoryScreen();
            }
            else
            {
                Map.CurrentFloor.Update(doTick);
                doTick = false;

                if (aimingOverlay != null)
                {
                    aimingOverlay.Draw();
                }

                PrintStats();

            }

            PrintMessages();
            frameBuffer.DrawFrame();

            if(exit)
            {
                return false;
            }

            return true;
        }

        void PrintMessages()
        {
            int numLines = messageLog.Count;
            if (numLines > 3)
            {
                numLines = 3;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < numLines; i++)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 1 - i);
                Console.Write( messageLog[ messageLog.Count - 1 - i ].PadRight( FrameBuffer.Instance.Width - statAreaWidth - 1 ) );
            }
        }

        public void Message(string m)
        {
            messageLog.Add(m);
        }

        public void DebugMessage(string m)
        {
            // check debug flag
            Message("[DEBUG] " + m);
        }

        public void DoTick()
        {
            doTick = true;
        }

        public void ExitGame()
        {
            exit = true;
        }


        void PrintStats()
        {
            int x = FrameBuffer.Instance.Width - statAreaWidth - 1;
            int y = 0;

            // Blank the area first
            while (y < Console.WindowHeight)
            {
                //Console.SetCursorPosition(x, y);
                frameBuffer.Write(x, y, "\u2551               ");
                y++;
            }


            y = 0;
            x++;
            
            if (PlayerCharacter != null)
            {
                //Console.Write("HP: {0}/{1}", PlayerCharacter.Health, PlayerCharacter.MaxHealth);
                frameBuffer.Write(x, y, "  == STATS ==   ");
                y++;
                frameBuffer.Write(x, y, string.Format("HP: {0}/{1}", PlayerCharacter.Health, PlayerCharacter.MaxHealth));
                y++;
                frameBuffer.Write(x, y, string.Format("Energy: {0}", PlayerCharacter.Energy));
                y++;


            }

            PrintHotkeys();
        }

        void PrintHotkeys()
        {
            int x = FrameBuffer.Instance.Width - statAreaWidth + 1;
            int y = FrameBuffer.Instance.Height - 10;

            frameBuffer.Write(x, y, "[F]ire");
            y++;
            frameBuffer.Write(x, y, "[I]nventory");
            y++;
            frameBuffer.Write(x, y, "[CTRL-Q] Quit");
            y++;

        }

        void DrawInventoryScreen()
        {
            FrameBuffer.Instance.Write(30, 0, "Inventory");

            for (int i = 0; i < PlayerCharacter.Instance.Items.Length; i++)
            {
                bool col1 = i < PlayerCharacter.Instance.Items.Length / 2;
                int x = col1 ? 5 : 35;
                int y = col1 ? i : i - PlayerCharacter.Instance.Items.Length / 2;
                y += 5;

                string name = "";
                if (PlayerCharacter.Instance.Items[i] != null)
                    name = PlayerCharacter.Instance.Items[i].Name;

                char c = (char)((int)'a' + i);

                FrameBuffer.Instance.Write(x, y, string.Format("[{0}] {1}", c, name));
            }
        }
    }
}

