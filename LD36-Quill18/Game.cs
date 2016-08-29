using System;
using System.Collections.Generic;
using System.Threading;

namespace LD36Quill18
{
    public enum InputMode { Normal, Aiming, Inventory, Looking, Fabricator, Help, LogView }

    public class Game
    {

        public Game()
        {
            _instance = this;

            Random = new Random();
            messageLog = new List<string>();

            frameBuffer = new FrameBuffer(0, 0, Console.WindowWidth, Console.WindowHeight-1);
            LevelRenderArea = new Rect(0, 0, 80 - statAreaWidth, 24 - 3);
            //frameBufferStats = new FrameBuffer(Console.WindowWidth - statAreaWidth, 0, statAreaWidth, Console.WindowHeight);

            Map = new Map();

            SetupUpgrades();

            PlayerCharacter.UpdateVision();
            Map.CurrentFloor.Recenter(true);
        }

        public Map Map { get; protected set; }
        public FabricatorUpgrade[] FabricatorUpgrades { get; set; }

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

        private void SetupUpgrades()
        {
            FabricatorUpgrades = new FabricatorUpgrade[] {
                new FabricatorUpgrade("Improved Impact Dampening (+10 Max Health)", (pc) => { pc.MaxHealth += 10; pc.Health += 10; }),
                new FabricatorUpgrade("Wiring Improvements (10% More Energy from Batteries)", (pc) => { 
                    pc.MaxEnergy = (int)(pc.MaxEnergy * 1.1);
                    pc.batteryEfficiency += 0.1;
                }),
                new FabricatorUpgrade("Upgrade Sensors (+1 Vision Range)", (pc) => { pc.VisionRadius += 1; }),
                new FabricatorUpgrade("Add Armor Plating (+1 Damage Reduction)", (pc) => { pc.DamageReduction += 1; }),
                new FabricatorUpgrade("Servo Actuator Replacement (+1 to Dodging)", (pc) => { pc.DodgeBonus += 1; }),
                new FabricatorUpgrade("Targetting CPU Over-Clocking (+1 to Accuracy)", (pc) => { pc.ToHitBonus += 1; }),
                new FabricatorUpgrade("Myomer Strengthening (+1 Melee Damage)", (pc) => { pc.MeleeDamage += 1; }),
                new FabricatorUpgrade("Kinetic Accelerator Boost (+1 Ranged Damage)", (pc) => { pc.RangedDamage += 1; }),
};
        }

        /// <summary>
        /// Called non-stop by the main program loop
        /// </summary>
        public bool Update()
        {
            //RedrawRequests.Clear();

            if (ClearRequested)
            {
                ClearRequested = false;
                FrameBuffer.Instance.Clear();
            }

            KeyboardHandler.Update_Keyboard();

            if (InputMode == InputMode.Inventory)
            {
                DrawInventoryScreen();
            }
            else if (InputMode == InputMode.Fabricator)
            {
                DrawFabricatorScreen();
            }
            else if (InputMode == InputMode.Help)
            {
                DrawHelpScreen();
            }
            else if (InputMode == InputMode.LogView)
            {
                DrawLogViewScreen();
            }
            else
            {
                Map.CurrentFloor.Update(doTick);
                doTick = false;

                if (aimingOverlay != null)
                {
                    aimingOverlay.Draw();
                    string s = "[ESC] to leave Targetting Mode";
                    FrameBuffer.Instance.Write(20, 0, s);
                    Rect r = new Rect(20, 0, s.Length, 1);
                    RedrawRequests.Rects.Add(r);
                }

                if (lookingOverlay != null)
                {
                    lookingOverlay.Draw();
                    string s = "[ESC] to leave Looking Mode";
                    FrameBuffer.Instance.Write(20, 0, s);
                    Rect r = new Rect(20, 0, s.Length, 1);
                    RedrawRequests.Rects.Add(r);
                }

                PrintStats();

            }

            if (InputMode != InputMode.LogView)
            {
                PrintMessages();
            }

            frameBuffer.DrawFrame();

            if(exit || Restart)
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

        public bool Restart = false;

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
                Console.Write( messageLog[ messageLog.Count - 1 - i ].PadRight( FrameBuffer.Instance.Width - statAreaWidth - 1) );
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
            if (false)
            {
                Message("[DEBUG] " + m);
            }
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
                frameBuffer.Write(x, y, "\u2551                ");
                y++;
            }


            y = 0;
            x++;

            if (PlayerCharacter != null && lookingOverlay == null && aimingOverlay == null)
            {
                //Console.Write("HP: {0}/{1}", PlayerCharacter.Health, PlayerCharacter.MaxHealth);
                frameBuffer.Write(x, y, "  == STATS ==   ");
                y++;

                ConsoleColor col = ConsoleColor.White;
                if (PlayerCharacter.Health < 10)
                    col = ConsoleColor.Red;
                else if (PlayerCharacter.Health < 50)
                    col = ConsoleColor.Yellow;
                frameBuffer.Write(x, y, string.Format("HP: {0}/{1}", PlayerCharacter.Health, PlayerCharacter.MaxHealth), col);
                y++;

                col = ConsoleColor.White;
                if (PlayerCharacter.Energy < 10)
                    col = ConsoleColor.Red;
                else if (PlayerCharacter.Energy < 50)
                    col = ConsoleColor.Yellow;
                frameBuffer.Write(x, y, string.Format("Energy: 0.{0}%", PlayerCharacter.Energy.ToString("d4")), col);
                y++;

                frameBuffer.Write(x, y, string.Format("   Max: 0.{0}%", PlayerCharacter.MaxEnergy.ToString("d4")));
                y++;
                frameBuffer.Write(x, y, string.Format("Melee Dmg: {0}", PlayerCharacter.MeleeDamage));
                y++;
                frameBuffer.Write(x, y, string.Format("Ranged Dmg: {0}", PlayerCharacter.RangedDamage));
                y++;
                frameBuffer.Write(x, y, string.Format("Acc Bonus: +{0}", PlayerCharacter.ToHitBonus));
                y++;
                frameBuffer.Write(x, y, string.Format("Dodge Bonus: +{0}", PlayerCharacter.DodgeBonus));
                y++;
                frameBuffer.Write(x, y, string.Format("Scraps: ${0}", PlayerCharacter.Money));
                y++;
                y++;
                frameBuffer.Write(x, y, string.Format("Sub-Level: {0}", (Map.CurrentFloorIndex+5)));


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
                    frameBuffer.Write(x + 2, y, Utility.WordWrap(tile.Character.Name, statAreaWidth));
                }

                y++;

                if (tile.Item != null)
                {
                    frameBuffer.Write(x + 2, y, Utility.WordWrap(tile.Item.Name, statAreaWidth));
                }

                y++;
                y++;
            }
            else {
                y++;
                y++;
                frameBuffer.Write(x + 2, y,"No Sensor\nCoverage");
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
            int y = FrameBuffer.Instance.Height - 8;

            frameBuffer.Write(x, y, "[F]ire");
            y++;
            frameBuffer.Write(x, y, "[I]nventory");
            y++;
            frameBuffer.Write(x, y, "[L]ook Mode");
            y++;
            frameBuffer.Write(x, y, "< Go Up");
            y++;
            frameBuffer.Write(x, y, "> Go Down");
            y++;
            frameBuffer.Write(x, y, "[V]iew Log");
            y++;
            frameBuffer.Write(x, y, "[H]elp");
            y++;
            frameBuffer.Write(x, y, "[CTRL-Q]uit");
            y++;

        }

        public bool ClearRequested = false;

        void DrawFabricatorScreen()
        {
            FrameBuffer.Instance.Write(30, FrameBuffer.Instance.Height - 3, "[ESC] to Exit this Screen");
            FrameBuffer.Instance.Write(5, 0, "              -- Augmentation Station --");
            FrameBuffer.Instance.Write(5, 1, "Improving near-scrap-metal with scrap-metal since 2092!");
            FrameBuffer.Instance.Write(5, 2, "           (Permanently upgrade a statistic.)");
            FrameBuffer.Instance.Write(5, 4, "               Your Metal Scraps: $" + PlayerCharacter.Instance.Money);



            int y = 8;

            for (int i = 0; i < FabricatorUpgrades.Length; i++)
            {
                char c = (char)((int)'a' + i);
                FrameBuffer.Instance.Write(5, y++, 
                       c +") [$"+FabricatorUpgrades[i].NextUpgradeCost.ToString()+"] " + FabricatorUpgrades[i].Name
                      );
            }

        }

        void DrawInventoryScreen()
        {
            FrameBuffer.Instance.Write(30, FrameBuffer.Instance.Height - 3, "[ESC] to Exit this Screen");
            FrameBuffer.Instance.Write(30, 0, "-- Inventory --");
            if (KeyboardHandler.inventoryExamineMode)
            {
                FrameBuffer.Instance.Write(20, 1, "Hit [?] to Stop Examining");
            }
            else
            {
                FrameBuffer.Instance.Write(20, 1, "Hit [?] to Examine Items");
            }

            if (KeyboardHandler.inventoryEquippedMode)
            {
                FrameBuffer.Instance.Write(20, 2, "Hit [TAB] to See Inventory");
                for (int i = 0; i < PlayerCharacter.EquippedItems.Length; i++)
                {
                    int x = 5;
                    int y = i;
                    y += 5;

                    string name = "";
                    if (PlayerCharacter.Instance.EquippedItems[i] != null)
                        name = PlayerCharacter.Instance.EquippedItems[i].Name;

                    char c = (char)((int)'a' + i);

                    FrameBuffer.Instance.Write(x, y, string.Format("[{0}] {2}: {1}", c, name, Enum.GetNames(typeof(EquipSlot))[i]));
                }
            }
            else
            {
                ConsoleColor col = ConsoleColor.White;
                FrameBuffer.Instance.Write(20, 2, "Hit [TAB] to See Equipped");
                FrameBuffer.Instance.Write(5, 3, "Hit [DELETE] followed by a letter to scrap an item.", col);

                if (KeyboardHandler.inventoryDeleteMode == true)
                {
                    col = ConsoleColor.Red;
                }
                else if (KeyboardHandler.inventoryExamineMode == true)
                {
                    col = ConsoleColor.Blue;
                }

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

                    FrameBuffer.Instance.Write(x, y, string.Format("[{0}] {1}", c, name), col);
                }
            }
        }

        public void RestartGame()
        {
            Restart = true;
        }

        public void Victory()
        {
            VictoryScreen vs = new VictoryScreen();

            Console.WriteLine("");
            Console.WriteLine("");

            while (true)
            {
                Console.WriteLine("Abort, Retry, Ignore?");
                string s = Console.ReadLine();
                if (s.ToLower() == "abort" || s.ToLower() == "a")
                {
                    ExitGame();
                    return;
                }
                if (s.ToLower() == "retry" || s.ToLower() == "r")
                {
                    RestartGame();
                    return;
                }
            }
        }

        public void BSOD()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Console.WriteLine("A problem has been detected and Warbot 9000 has been shut down to prevent damage to your facility.");
            Console.WriteLine("");
            Console.WriteLine("STRUCTURAL_INTEGRITY_FAILURE");
            Console.WriteLine("");
            Console.WriteLine("If this is the first time you've seen this error screen,");
            Console.WriteLine("restart your Warbot. If this screen appears again, follow");
            Console.WriteLine("these steps:");
            Console.WriteLine("");
            Console.WriteLine("Check to make sure any new hardware or software is properly installed.");
            Console.WriteLine("If this is a new installation, ask your military supplier for any Warbot");
            Console.WriteLine("updates you might need.");
            Console.WriteLine("");
            Console.WriteLine("If problems continue, cease all invasion plans immediately and begin to");
            Console.WriteLine("purge your military command structure. Disable BIOS memory options such as");
            Console.WriteLine("caching or biological weaponry.");
            Console.WriteLine("");
            Console.WriteLine("");

            Thread.Sleep(500);
            Console.WriteLine("Not ready reading drive A");
            Console.CursorVisible = true;

            while (true)
            {
                Console.WriteLine("[A]bort, [R]etry, [I]gnore?");
                string s = Console.ReadLine();
                if (s.ToLower() == "abort" || s.ToLower() == "a")
                {
                    ExitGame();
                    return;
                }
                if (s.ToLower() == "retry" || s.ToLower() == "r")
                {
                    RestartGame();
                    return;
                }
            }
        }

        void DrawHelpScreen()
        {
            FrameBuffer.Instance.Write(30, FrameBuffer.Instance.Height - 3, "[ESC] to Exit this Screen");
            FrameBuffer.Instance.Write(30, 0, "-- HELP --");

            int y = 3;
            FrameBuffer.Instance.Write(5, y++, "Use the Numpad or Arrow Keys to move around.");
            y++;
            FrameBuffer.Instance.Write(5, y++, "Use [<] and [>] on stairs to move up and down floors.");
            y++;
            FrameBuffer.Instance.Write(5, y++, "Moving into enemies will melee attack them");
            FrameBuffer.Instance.Write(5, y++, "Moving on to items will pick them up.");
            y++;
            FrameBuffer.Instance.Write(5, y++, "Use [L] to look around, and [ENTER] to examine objects.");
            y++;
            FrameBuffer.Instance.Write(5, y++, Utility.WordWrap("If you have a ranged weapon equipped, [F] will enter Aiming Mode. [ENTER] fire."));
            y++;
            FrameBuffer.Instance.Write(5, y++, "Access your Inventory by pressing [I]. Items can be used or equipped.");
            y++;
            y++;
            FrameBuffer.Instance.Write(5, y++, Utility.WordWrap("Your goal is to acquire the Yendorian Power Cell from the bottom of the complex."));


        }

        void DrawLogViewScreen()
        {
            FrameBuffer.Instance.Write(30, FrameBuffer.Instance.Height - 1, "[ESC] to Exit this Screen");
            FrameBuffer.Instance.Write(30, 0, "-- VIEW LOG --");

            int top = 2;
            int numLinesToShow = FrameBuffer.Instance.Height - top - 2;

            int startAtIndex = messageLog.Count - numLinesToShow;
            if (startAtIndex < 0)
                startAtIndex = 0;

            for (int i = 0; i < numLinesToShow; i++)
            {
                if (startAtIndex + i >= messageLog.Count)
                {
                    break;
                }

                FrameBuffer.Instance.Write(2, top + i, messageLog[startAtIndex + i]);
            }
        }


    }
}

