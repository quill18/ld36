using System;
using System.Collections.Generic;
using System.Threading;

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
            LevelRenderArea = new Rect(0, 0, 80 - statAreaWidth, 24 - 3);
            //frameBufferStats = new FrameBuffer(Console.WindowWidth - statAreaWidth, 0, statAreaWidth, Console.WindowHeight);

            Map = new Map();

            PlayerCharacter.UpdateVision();
            Map.CurrentFloor.Recenter(true);
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
        public AimingOverlay aimingOverlay;
        public LookingOverlay lookingOverlay;
        #endregion

        private int statAreaWidth = 16;

        public Rect LevelRenderArea;
           
        private FrameBuffer frameBuffer;
        //private FrameBuffer frameBufferStats;

        public InputMode InputMode = InputMode.Normal;

        private List<string> messageLog;

        public int SleepFor { get; set; }

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
                if (lookingOverlay != null)
                {
                    lookingOverlay.Draw();
                }

                PrintStats();

            }

            PrintMessages();
            frameBuffer.DrawFrame();

            if(exit)
            {
                return false;
            }

            if (SleepFor > 0)
            {
                Thread.Sleep(SleepFor);
                SleepFor = 0;
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
            string[] ms = m.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries );

            foreach( string m2 in ms)
                messageLog.Add(m2);
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

            if (lookingOverlay != null)
            {
                PrintLookingInfo(Map.CurrentFloor.GetTile(lookingOverlay.X, lookingOverlay.Y));
            }
            else if (aimingOverlay != null)
            {
                PrintLookingInfo(Map.CurrentFloor.GetTile(aimingOverlay.X, aimingOverlay.Y));
            }


            PrintHotkeys();
        }

        void PrintLookingInfo(Tile tile)
        {
            int x = FrameBuffer.Instance.Width - statAreaWidth - 1;
            int y = 8;
            frameBuffer.Write(x, y, "\u255F");
            for (int i = 1; i < statAreaWidth; i++)
            {
                frameBuffer.Write(x + i, y, "\u2500");

            }

            y++;
            y++;

            if (tile != null && tile.WasSeen == true)
            {
                frameBuffer.Write(x + 2, y, tile.TileType.ToString());

                y++;

                if (tile.Character != null)
                {
                    frameBuffer.Write(x + 2, y, tile.Character.Name);
                }

                y++;

                if (tile.Item != null)
                {
                    frameBuffer.Write(x + 2, y, tile.Item.Name);
                }

                y++;
                y++;
            }
            else {
                y++;
                y++;
                frameBuffer.Write(x + 2, y,"No Sensor\nCovereage");
                y++;
                y++;
            }

            frameBuffer.Write(x, y, "\u255F");
            for (int i = 1; i < statAreaWidth; i++)
            {
                frameBuffer.Write(x + i, y, "\u2500");

            }

        }

        void PrintHotkeys()
        {
            int x = FrameBuffer.Instance.Width - statAreaWidth + 1;
            int y = FrameBuffer.Instance.Height - 10;

            frameBuffer.Write(x, y, "[F]ire");
            y++;
            frameBuffer.Write(x, y, "[I]nventory");
            y++;
            frameBuffer.Write(x, y, "[L]ook");
            y++;
            frameBuffer.Write(x, y, "[CTRL-Q] Quit");
            y++;

        }

        void DrawInventoryScreen()
        {
            FrameBuffer.Instance.Write(30, 0, "Inventory");
            if (KeyboardHandler.inventoryExamineMode)
            {
                FrameBuffer.Instance.Write(20, 1, "Hit [?] to Stop Examining");
                FrameBuffer.Instance.Write(20, 2, "Hit [E] to See Equiped");
            }
            else
            {
                FrameBuffer.Instance.Write(20, 1, "Hit [?] to Examine Items");
                FrameBuffer.Instance.Write(20, 2, "Hit [E] to See Inventory");
            }

            if (KeyboardHandler.inventoryEquippedMode)
            {
                for (int i = 0; i < PlayerCharacter.EquippedItems.Length; i++)
                {
                    int x = 5;
                    int y = i;
                    y += 5;

                    string name = "";
                    if (PlayerCharacter.Instance.EquippedItems[i] != null)
                        name = PlayerCharacter.Instance.EquippedItems[i].Name;

                    char c = (char)((int)'a' + i);

                    FrameBuffer.Instance.Write(x, y, string.Format("[{0}] {1}", c, name));
                }
            }
            else
            {
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
}

