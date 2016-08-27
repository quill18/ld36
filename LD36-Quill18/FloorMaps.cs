using System;

namespace LD36Quill18
{
    public static class FloorMaps
    {
        public static string[] floor = {
            @"
           ##############################
           #                            #             ###############
           #        0                   #             #             #
           #   >@                $      ###############             #
           #                            +             +             #
           #         1           B      ###############             #
           #                            #             #             #
           #                            #             ###############
           ##############################
",


            @"
           ##############################
           #                            #             ###############
           #                    d       #             #             #
           #   <                        ###############             #
           #                     s      +             +       >     #
           #                            ###############             #
           #                    d       #             #             #
           #                            #             ###############
           ##############################
",


            @"
           ##############################
           #                            #             ###############
           #                    d       #             #             #
           #   <                        ###############             #
           #                     s      +             +       >     #
           #                            ###############             #
           #                    d       #             #             #
           #                            #             ###############
           ##############################
",

            @"
           ##############################
           #                            #             ###############
           #                    d       #             #             #
           #   <                        ###############             #
           #                     s      +             +       >     #
           #                            ###############             #
           #                    d       #             #             #
           #                            #             ###############
           ##############################
",


            @"
           ##############################
           #                            #             ###############
           #                    d       #             #             #
           #   <                        ###############             #
           #                     s      +             +       >     #
           #                            ###############             #
           #                    d       #             #             #
           #                            #             ###############
           ##############################
",


            @"
           ##############################
           #                            #             ###############
           #                    d       #             #             #
           #   <                        ###############             #
           #                     s      +             +       >     #
           #                            ###############             #
           #                    d       #             #             #
           #                            #             ###############
           ##############################
",

        };


        static FloorMaps()
        {
            // This runs AFTER the floors are created, to add
            // features that need more info than a single character

            // floor 

            ItemSpawner = new Action<Tile>[18, 10];

            ItemSpawner[0, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Punchy Punch";
                item.Description = "A punchier punch that does 4-8 damage.";
                item.Chixel = new Chixel('A', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Arm;
                item.OnEquip += (i, c) =>
                {
                    c.MeleeDamageFunc = () => { return Game.Instance.Random.Next(4, 8); };
                    c.EnergyPerMelee = 3;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.MeleeDamageFunc = PlayerCharacter.DefaultMeleeAttackDamage;
                    c.EnergyPerMelee = PlayerCharacter.DefaultEnergyPerMelee;
                };
                tile.Item = item;

            };

            ItemSpawner[0, 1] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Choppy Chop";
                item.Chixel = new Chixel('A', ConsoleColor.Green);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Arm;
                item.OnEquip += (i, c) =>
                {
                    c.MeleeDamageFunc = () => { return Game.Instance.Random.Next(40, 80); };
                    c.EnergyPerMelee = 99999;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.MeleeDamageFunc = PlayerCharacter.DefaultMeleeAttackDamage;
                    c.EnergyPerMelee = PlayerCharacter.DefaultEnergyPerMelee;
                };
                tile.Item = item;

            };



        }

        static public Action<Tile>[,] ItemSpawner;

        // what if we had a callback like  
        //          specialFeature[2][0] = () => { // some action }
        //      So this would do something to the spot labelled 0 on the floor with index 2

        // Or maybe we just need a couple more symbols for "locked door" or even "door locked by blue key"
        // The number could be the security rating of the door
        // Maybe you need to find keycards/computers to upgrade your own security level.  Maybe
        // bosses hold these?

    }
}

