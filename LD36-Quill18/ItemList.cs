using System;
using System.Collections.Generic;

namespace LD36Quill18
{
    static public class ItemList
    {
        static ItemList()
        {
            Items = new Dictionary<char, Item>();

            char c;

            c = '%';
            Items[c] = new Item();
            Items[c].Name = "Small Battery (9v)";
            Items[c].Description = "Enough for a few more steps. (Use it to gain energy.)";
            Items[c].Chixel = new Chixel(c, ConsoleColor.Green);
            Items[c].OnUse += (item) => { OnUse_ChangeEnergy(item, 90); };
            Items[c].UsesLeft = 1;

            c = 'B';
            Items[c] = new Item();
            Items[c].Name = "Industrial Battery (24v)";
            Items[c].Description = "Oooooh...that's the stuff! (Use it to gain a LOT of energy.)";
            Items[c].Chixel = new Chixel(c, ConsoleColor.Green);
            Items[c].OnUse += (item) => { OnUse_ChangeEnergy(item, 240); };
            Items[c].UsesLeft = 1;


            c = 'W';
            Items[c] = new Item();
            Items[c].Name = "Welding Kit";
            Items[c].Description = "Enough to seal a few holes in your armor plating. (Heals you a good amount.)";
            Items[c].Chixel = new Chixel(c, ConsoleColor.Green);
            Items[c].OnUse += (item) => { OnUse_Heal(item, 35); };
            Items[c].UsesLeft = 1;

            c = 'w';
            Items[c] = new Item();
            Items[c].Name = "WD-41";
            Items[c].Description = "Helps keep your joints moving! (Heals you a little.)";
            Items[c].Chixel = new Chixel(c, ConsoleColor.Green);
            Items[c].OnUse += (item) => { OnUse_Heal(item, 15); };
            Items[c].UsesLeft = 1;

            c = 'D';
            Items[c] = new Item();
            Items[c].Name = "Duct Tape";
            Items[c].Description = "Cures what ails you! (Heals you a LOT.)";
            Items[c].Chixel = new Chixel(c, ConsoleColor.Green);
            Items[c].OnUse += (item) => { OnUse_Heal(item, 100); };
            Items[c].UsesLeft = 1;



            c = '!';
            Items[c] = new Item();
            Items[c].Name = "Access Pass";
            Items[c].Description = "A single-use keycard -- security used to be very strict. (Used automatically.)";
            Items[c].Chixel = new Chixel(c, ConsoleColor.Yellow);
            Items[c].UsesLeft = 1;
            Items[c].IsKey = true;

            c = '$';
            Items[c] = new Item();
            Items[c].Name = "Metal Scraps";
			Items[c].Description = "Useless by itself, but can be used to provide raw matter to a 3D Fabricator.";
			Items[c].Value = 100;
            Items[c].Chixel = new Chixel(c, ConsoleColor.Yellow);

            c = '&';
            Items[c] = new Item();
            Items[c].Name = "Unpowered Warbot";
            Items[c].Description = "This unit's power cells are completely depleted.";
            Items[c].Chixel = new Chixel(c, ConsoleColor.Gray);
            Items[c].Static = true;

            c = 'Y';
            Items[c] = new Item();
            Items[c].Name = "Yendorian Power Cell";
            Items[c].Description = "Contains the power of a black hole.";
            Items[c].Chixel = new Chixel(c, ConsoleColor.DarkYellow);
            Items[c].OnPickup += OnPickup_Yendor;





        }

        static public Dictionary<char, Item> Items;

        public static void OnUse_ChangeEnergy(Item item, int amt)
        {
            //frameBuffer.Write(x, y, string.Format("Energy: 0.{0}%", PlayerCharacter.Energy.ToString("d4")));
            amt = (int)(amt * PlayerCharacter.Instance.batteryEfficiency);
            PlayerCharacter.Instance.Energy += amt;
            Game.Instance.Message( Utility.WordWrap( string.Format("You gained 0.{0}% energy.\nCurrent Energy: 0.{1}% (Max Capacity: 0.{2}%) ",
                                                amt.ToString("d4"),
                                                PlayerCharacter.Instance.Energy.ToString("d4"),
                                                PlayerCharacter.Instance.MaxEnergy.ToString("d4")
                                                                  )));
        }

        public static void OnUse_Heal(Item item, int amt)
        {
            PlayerCharacter.Instance.Health += amt;
            Game.Instance.Message(string.Format("You repair {0} HPs. Current HPs: {1}/{2}", 
                                                amt, 
                                                PlayerCharacter.Instance.Health,
                                                PlayerCharacter.Instance.MaxHealth));


        }

        public static void OnPickup_Yendor(Item item)
        {
            Game.Instance.Victory();

        }


    }

}

