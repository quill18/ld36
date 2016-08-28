using System;

namespace LD36Quill18
{
    public static class FloorMaps
    {
        public static string[] floor = {
            @"
           ##############################
           #                            #             ###############
           #               f            #             #      1      #
           #   >                        ###############      f      #
           #         a                  +             +             #
           #                            ###############             #
           #               f            #             #             #      #####
           #                            #             #######+#######      # 0 #
           ##############################                   # #            #   #         
                                              ############### ###############+##                                      
                                              #                                #                                       
                                    ########### ############# ##################                                       
                                    #&            &#        # #                                                        
                                    ##            ##        # #                                                        
                                    #&            @#        # #                                                        
                                    ##            ##    ##### #############                                            
                                    #&     C2     &#    #                 #                                            
                                    ##            ##    #    π   π   π    #                                            
                                    #&            &#    #                 #                                            
                                    ##            ##    #                 #                                           
                                    #&            &#    #                 #                                            
                                    ################    ######++####++#####                                            
                                                            XXXXXXXXXXXX    
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
                item.Name = "Bucket";
                item.Description = "Much-needed protection for your cranial CPU, but impairs sensors. (+1 DR, -1 Vision)";
                item.Chixel = new Chixel('A', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Head;
                item.OnEquip += (i, c) =>
                {
                    c.DamageReduction += 1;
                    c.VisionRadius -= 1;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.DamageReduction -= 1;
                    c.VisionRadius += 1;

                };
                tile.Item = item;

            };

            ItemSpawner[0, 1] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Broken Girder";
                item.Description = "Physics indicate that this would increase your kinetic strike potential. (+1 Melee Damage)";
                item.Chixel = new Chixel('A', ConsoleColor.Green);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Arm;
                item.OnEquip += (i, c) =>
                {
                    c.MeleeDamage += 1;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.MeleeDamage -= 1;
                };
                tile.Item = item;

            };

            ItemSpawner[0, 2] = (tile) =>
{
    Item item = new Item();
    item.Name = "Webcam";
    item.Description = "An older-model 80k resolution Webcam. +1 Vision Radius";
    item.Chixel = new Chixel(']', ConsoleColor.Blue);
    item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
    item.EquipSlot = EquipSlot.Head;
    item.OnEquip += (i, c) =>
    {
        c.VisionRadius += 1;
        c.UpdateVision();
    };
    item.OnUnequip += (i, c) =>
    {
        c.VisionRadius -= 1;
        c.UpdateVision();
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

