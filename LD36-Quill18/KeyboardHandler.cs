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

                if (((cki.Modifiers & ConsoleModifiers.Control) != 0) && cki.Key == ConsoleKey.Q)
                {
                    Game.Instance.ExitGame();
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
            else if (cki.Key == ConsoleKey.NumPad5)
            {
                // Do nothing
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.OemPeriod)
            {
                PlayerCharacter.Instance.GoDown();
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.OemComma)
            {
                PlayerCharacter.Instance.GoUp();
                Game.Instance.DoTick();
            }
            else if (cki.Key == ConsoleKey.F)
            {
                Game.Instance.InputMode = InputMode.Aiming;
                Game.Instance.aimingOverlay = new AimingOverlay();
                Game.Instance.aimingOverlay.Draw();
                Game.Instance.Message("Aim Mode Started: Hit [ENTER] to fire.");
            }
            else if (cki.Key == ConsoleKey.L)
            {
                Game.Instance.InputMode = InputMode.Looking;
                Game.Instance.lookingOverlay = new LookingOverlay();
                Game.Instance.lookingOverlay.Draw();
                Game.Instance.Message("Look Mode Started: Hit [ENTER] to examine.");
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
            }
            else if (cki.KeyChar == '/' || cki.KeyChar == '?')
            {
                inventoryExamineMode = !inventoryExamineMode;
            }
            else if (cki.Key == ConsoleKey.E)
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

                i = (int)cki.KeyChar - 'A';
                if (i >= 0 && i < PlayerCharacter.Instance.Items.Length)
                {
                    if (inventoryExamineMode)
                    {
                        Game.Instance.Message(PlayerCharacter.Instance.Items[i].FullDescription);
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
                PlayerCharacter.Instance.FireTowards(aimingOverlay.X, aimingOverlay.Y);
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
                    Game.Instance.Message(tile.Character.Name + ": " + tile.Character.Description);
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

    }
}

