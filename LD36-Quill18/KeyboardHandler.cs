using System;
namespace LD36Quill18
{
    public class KeyboardHandler
    {

        public static void Update_Keyboard()
        {
            if (Console.KeyAvailable)
            {
                // Read one key
                ConsoleKeyInfo cki = Console.ReadKey(true);

                bool cheatsEnabled = true;

                if (((cki.Modifiers & ConsoleModifiers.Control) != 0) && cki.Key == ConsoleKey.Q)
                {
                    Game.Instance.ExitGame();
                }
                if (cheatsEnabled && ((cki.Modifiers & ConsoleModifiers.Control) != 0) && cki.Key == ConsoleKey.B)
                {
                    Game.Instance.BSOD();
                }
                if (cheatsEnabled && ((cki.Modifiers & ConsoleModifiers.Control) != 0) && cki.Key == ConsoleKey.E)
                {
                    PlayerCharacter.Instance.Energy = -9999;
                }
                if (cheatsEnabled && ((cki.Modifiers & ConsoleModifiers.Control) != 0) && cki.Key == ConsoleKey.D4)
                {
                    PlayerCharacter.Instance.Money += 999999;
                }
                if (cheatsEnabled && ((cki.Modifiers & ConsoleModifiers.Control) != 0) && cki.Key == ConsoleKey.V)
                {
                    PlayerCharacter.Instance.VisionRadius += 99;
                }
                if (cheatsEnabled && ((cki.Modifiers & ConsoleModifiers.Control) != 0) && cki.Key == ConsoleKey.H)
                {
                    PlayerCharacter.Instance.Health = PlayerCharacter.Instance.MaxHealth = 999999;
                }
                else
                {
                    switch (Game.Instance.InputMode)
                    {
                        case InputMode.Normal:
                            Update_Keyboard_Normal(cki);
                            break;
                        case InputMode.Aiming:
                            Update_Keyboard_Aiming(cki);
                            break;
                        case InputMode.Inventory:
                            Update_Keyboard_Inventory(cki);
                            break;
                        case InputMode.Looking:
                            Update_Keyboard_Looking(cki);
                            break;
                        case InputMode.Fabricator:
                            Update_Keyboard_Fabricator(cki);
                            break;
                    }
                }
            }

            // If there are any more keys, drain them out of the buffer
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }




        public static void Update_Keyboard_Normal(ConsoleKeyInfo cki)
        {
            if (cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.NumPad6)
            {
                PlayerCharacter.Instance.QueueMoveBy(1, 0);
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.NumPad4)
            {
                PlayerCharacter.Instance.QueueMoveBy(-1, 0);
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.NumPad8)
            {
                PlayerCharacter.Instance.QueueMoveBy(0, -1);
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.NumPad2)
            {
                PlayerCharacter.Instance.QueueMoveBy(0, 1);
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.NumPad7 || cki.Key == ConsoleKey.Home)
            {
                PlayerCharacter.Instance.QueueMoveBy(-1, -1);
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.NumPad9 || cki.Key == ConsoleKey.PageUp)
            {
                PlayerCharacter.Instance.QueueMoveBy(1, -1);
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.NumPad3 || cki.Key == ConsoleKey.PageDown)
            {
                PlayerCharacter.Instance.QueueMoveBy(1, 1);
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.NumPad1 || cki.Key == ConsoleKey.End)
            {
                PlayerCharacter.Instance.QueueMoveBy(-1, 1);
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.NumPad5 || cki.KeyChar == '5')
            {
                // Do nothing
                Game.Instance.DoTick();
            }
			else if (cki.Key == ConsoleKey.OemPeriod || cki.KeyChar=='.' || cki.KeyChar=='>')
            {
                PlayerCharacter.Instance.GoDown();
                Game.Instance.DoTick();
            }
			else if (cki.Key == ConsoleKey.OemComma || cki.KeyChar == ',' || cki.KeyChar == '<')
            {
                PlayerCharacter.Instance.GoUp();
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.F)
            {
                if (PlayerCharacter.Instance.HasRanged == false)
                {
                    Game.Instance.Message("No ranged weapon equipped.");
                    return;
                }

                Game.Instance.InputMode = InputMode.Aiming;
                Game.Instance.aimingOverlay = new AimingOverlay();
                Game.Instance.aimingOverlay.Draw();
                Game.Instance.Message("Aim Mode Started: Hit [ENTER] to fire and [ESC] to cancel.");
            }
            else if (cki.Key == ConsoleKey.L)
            {
                Game.Instance.InputMode = InputMode.Looking;
                Game.Instance.lookingOverlay = new LookingOverlay();
                Game.Instance.lookingOverlay.Draw();
                Game.Instance.Message("Look Mode Started: Hit [ENTER] to examine and [ESC] to cancel.");
            }
            else if (cki.Key == ConsoleKey.I)
            {
                Game.Instance.InputMode = InputMode.Inventory;
                FrameBuffer.Instance.Clear();
            }
        }

        public static bool inventoryExamineMode = false;
        public static bool inventoryEquippedMode = false;

        static void Update_Keyboard_Inventory(ConsoleKeyInfo cki)
        {
            FrameBuffer.Instance.Clear();
            if (cki.Key == ConsoleKey.Escape)
            {
                Game.Instance.InputMode = InputMode.Normal;
                RedrawRequests.FullRedraw();
                inventoryExamineMode = false;
                inventoryEquippedMode = false;
            }
            else if (cki.KeyChar == '/' || cki.KeyChar == '?')
            {
                inventoryExamineMode = !inventoryExamineMode;
            }
            else if (cki.Key == ConsoleKey.Tab)
            {
                inventoryEquippedMode = !inventoryEquippedMode;
            }
            else
            {
                // Try to map to an inventory item
                int i = (int)cki.KeyChar - 'a';

                if (i < 0 || (inventoryEquippedMode && i >= PlayerCharacter.Instance.EquippedItems.Length) ||
                   (!inventoryEquippedMode && i >= PlayerCharacter.Instance.Items.Length))
                {
                    // Out of bounds for inventory.
                    return;
                }

                if (inventoryExamineMode)
                {
                    if (inventoryEquippedMode)
                    {
                        if (PlayerCharacter.Instance.EquippedItems[i] != null)
                        {
                            Game.Instance.Message(PlayerCharacter.Instance.EquippedItems[i].FullDescription);
                        }
                    }
                    else 
                    {
                        if (PlayerCharacter.Instance.Items[i] != null)
                        {
                            Game.Instance.Message(PlayerCharacter.Instance.Items[i].FullDescription);
                        }
                    }
                }
                else
                {
                    if (inventoryEquippedMode)
                    {
                        PlayerCharacter.Instance.Unequip(i);
                    }
                    else
                    {
                        PlayerCharacter.Instance.UseItem(i);
                    }
                }

            }
        }

        static void Update_Keyboard_Aiming(ConsoleKeyInfo cki)
        {
            AimingOverlay aimingOverlay = Game.Instance.aimingOverlay;

            // Direction keys move the targeting reticle instead of the player
            if (cki.Key == ConsoleKey.Escape)
            {
                Game.Instance.InputMode = InputMode.Normal;
                Game.Instance.aimingOverlay = null;
                return;
            }
            else if (cki.Key == ConsoleKey.Enter)
            {
                // Fire!
                PlayerCharacter.Instance.QueueFireAt(aimingOverlay.X, aimingOverlay.Y);
                Game.Instance.InputMode = InputMode.Normal;
                Game.Instance.aimingOverlay = null;
                Game.Instance.DoTick();
                return;
            }
            else if (cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.NumPad6)
            {
                aimingOverlay.X += 1;
                aimingOverlay.Y += 0;
            }
            else if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.NumPad4)
            {
                aimingOverlay.X -= 1;
                aimingOverlay.Y += 0;
            }
            else if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.NumPad8)
            {
                aimingOverlay.X += 0;
                aimingOverlay.Y -= 1;
            }
            else if (cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.NumPad2)
            {
                aimingOverlay.X += 0;
                aimingOverlay.Y += 1;
            }
            else if (cki.Key == ConsoleKey.NumPad7 || cki.Key == ConsoleKey.Home)
            {
                aimingOverlay.X -= 1;
                aimingOverlay.Y -= 1;
            }
            else if (cki.Key == ConsoleKey.NumPad9 || cki.Key == ConsoleKey.PageUp)
            {
                aimingOverlay.X += 1;
                aimingOverlay.Y -= 1;
            }
            else if (cki.Key == ConsoleKey.NumPad3 || cki.Key == ConsoleKey.PageDown)
            {
                aimingOverlay.X += 1;
                aimingOverlay.Y += 1;
            }
            else if (cki.Key == ConsoleKey.NumPad1 || cki.Key == ConsoleKey.End)
            {
                aimingOverlay.X -= 1;
                aimingOverlay.Y += 0;
            }

        }

        static void Update_Keyboard_Looking(ConsoleKeyInfo cki)
        {
            LookingOverlay overlay = Game.Instance.lookingOverlay;

            // Direction keys move the targeting reticle instead of the player
            if (cki.Key == ConsoleKey.Escape)
            {
                Game.Instance.InputMode = InputMode.Normal;
                Game.Instance.lookingOverlay = null;
                return;
            }
            else if (cki.Key == ConsoleKey.Enter)
            {
                Tile tile = Game.Instance.Map.CurrentFloor.GetTile(overlay.X, overlay.Y);

                if (tile.Character != null)
                {
                    Game.Instance.Message( Utility.WordWrap(tile.Character.Name + ": " + tile.Character.Description) );
                }
                if (tile.Item != null)
                {
                    Game.Instance.Message( tile.Item.FullDescription );
                }

                return;
            }
            else if (cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.NumPad6)
            {
                overlay.X += 1;
                overlay.Y += 0;
            }
            else if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.NumPad4)
            {
                overlay.X -= 1;
                overlay.Y += 0;
            }
            else if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.NumPad8)
            {
                overlay.X += 0;
                overlay.Y -= 1;
            }
            else if (cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.NumPad2)
            {
                overlay.X += 0;
                overlay.Y += 1;
            }
            else if (cki.Key == ConsoleKey.NumPad7 || cki.Key == ConsoleKey.Home)
            {
                overlay.X -= 1;
                overlay.Y -= 1;
            }
            else if (cki.Key == ConsoleKey.NumPad9 || cki.Key == ConsoleKey.PageUp)
            {
                overlay.X += 1;
                overlay.Y -= 1;
            }
            else if (cki.Key == ConsoleKey.NumPad3 || cki.Key == ConsoleKey.PageDown)
            {
                overlay.X += 1;
                overlay.Y += 1;
            }
            else if (cki.Key == ConsoleKey.NumPad1 || cki.Key == ConsoleKey.End)
            {
                overlay.X -= 1;
                overlay.Y += 0;
            }

        }

        static void Update_Keyboard_Fabricator(ConsoleKeyInfo cki)
        {
            FrameBuffer.Instance.Clear();
            if (cki.Key == ConsoleKey.Escape)
            {
                Game.Instance.InputMode = InputMode.Normal;
                RedrawRequests.FullRedraw();
            }
            else
            {
                // Try to map to an inventory item
                int i = (int)cki.KeyChar - 'a';

                if (i < 0 || i >= Game.Instance.FabricatorUpgrades.Length)
                {
                    // Out of bounds for upgrades.
                    return;
                }

                FabricatorUpgrade fu = Game.Instance.FabricatorUpgrades[i];
                PlayerCharacter pc = PlayerCharacter.Instance;

                if (fu.NextUpgradeCost > pc.Money)
                {
                    Game.Instance.Message("Insufficient metal scraps for upgrade.");
                    return;
                }

                Game.Instance.Message("PURCHASED: " + fu.Name);
                Console.Beep();

                pc.Money -= fu.NextUpgradeCost;

                fu.OnPurchase( pc );

                fu.NextUpgradeCost = (int)(fu.NextUpgradeCost*2);
            }
        }

    }
}

