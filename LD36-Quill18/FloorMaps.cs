using System;

namespace LD36Quill18
{
    public static class FloorMaps
    {
        public static string[] floor = {
            @"     Level 0

           ##############################
           #                            #             ###############
           #               f            #             #   W     W   #
           #   >                        ###############      1      #
           #         r                  +             +      f      #
           #                            ###############             #
           #               f            #             #             #      #####
           #                            #             #######*#######      # 0 #
           ##############################                   # #            #   #         
                                              ############### ###############+##                                      
                                              #                                #                                       
                                    ########### ############# ##################                                       
                                    #&  % % % %   &#        # #                                                        
                                    ##            ##        # #                                                        
                                    #&            @#        # #                                                        
                                    ##            ##    #####+#############                                            
                                    #&     C2     &#    #                 #                                            
                                    ##            ##    #    π   !   π    #                                            
                                    #&            &#    #                 #                                            
                                    ##            ##    #    f       f    #                                           
                                    #&            &#    #                 #                                            
                                    ################    ######++####++#####                                            
                                                            XXXXXXXXXXXX    
",            @"  Level 1 -- Fabrication

                                             #####
                                             # < #
                                             #   #
                                             ## ##                                           
                       ########################+#######################                                        
                       #   $   $   $  ##              ##     {~}      #                                         
                       #              ##              ##              #                                           
                       #»»»»»»»»»»»»»»++»»»»»»»»»»»»»»++»»»»»»»»»»»»»»# 
                       #              ##              ##              #                                         
                       #              ##              ##      r       #                                         
                       #   r          ##   f       f  ##           !  #                                         
                       #              ##              ##              #                                         
                       #««««««««««««««++««««««««««««««++««««««««««««««#
                       #              ##              ##              #                                         
                       #   $   $   $  ##              ##     {~}      #                                         
                       ########################*#######################                                       
                                             ## ##                                                             
                                             #   #                                                             
                                             # > #                                                             
                                             #####                                                             
                                                                                                               
",              @"  Level 2 -- Robotic Repair Center
                                                                                                                                                                                                                     
                                                                                                                                                                                                                     
                  ############                                             ##############                                                                                                                                         
                  #          #                                             #            #                                                                                                                               
                  #        ! #                                             #   %  >     #########                                                                                                                       
 ############ #####          #                                             #            +       #                                                                                                                       
 #          ###   +        i #   ###########################               #            ####### #                                                                                                                       
 #          +   ###          #####           {~}           ###########     #            #     # #                                                                                                                      
 #    <     ##### #        i +   +         t     r         +         #     ###+##########     # #                                                                                                                       
 #          #     # %        #####     _      0     _      ######### #       # #              # #                                                                                                                       
 #          #     #          #   #                         #       # #       # #              # #                                                                                                                       
 #          #     ############   #  &    &    &    &    &  #   #####+#########+###         ####*########                                                                                                                          
 #          #                    #                         #   #         %       #         #%         $#                                                                                                              
 #          #                    ###########################   #                 #         #           #                                                                                                              
 #######++###                                                  #     a     a     ###########          $#                                                                                                            
       #  #                                                    #                 +         *           #          
       #  #                                                    #                 ###########          $#          
       #  #                      ###############################            a    #         #  $  $  $  #                                  
       #  #                      #                      +      +         !       #         #############            
       #  #                      #                      ########                 #                                  
       #  ########################          s           #      ###################                                  
       #                         *                      #                                                           
       ###########################                      #                                                           
                                 #  % &            & %  #                                                           
                                 ########################                                                                                 
                                                                                                                 
",              @"  Level 3 -- 
                                                                                                                                                                                                                     
     ##########################################################################################                                                                                                             
     #                                                                                        #                                                                                                             
     #                                                                                        #                                                                                                             
     #    ###############       ###############       ###############      ###############    #                                                                                                             
     #    #             #       #             #       #             #      #     {~}     #    #                                                                                                             
     #    # %  %   %  % #       #    f        #       #             #      #        a    #    #                                                                                                             
     #    #             #       #      ! f    #       #   <    $    #      #    f        #    #                                                                                                             
     #    #             #       #             #       #             #      #             #    #                                                                                                             
     #    #######+#######       #######+#######       #######+#######      #######+#######    #                                                                                                             
     #                                                                                        #                                                                                                             
     #                                                                                        #                                                                                                             
     #                      #######+##              x x xxx    xxx################*#######    #                                                                                                             
     #       ########       #$       #          x         x    x                         #    #                                                                                                             
     #       #  0   *       #        #          x     a   x    x                         #    #                                                                                                             
     #       #      #       #%   r   x          x    $    x    x          %$             #    #                                                                                                             
     #       ########       #        x          x   a     x               $%             #    #                                                                                                             
     #       #   >  #       #$   r   x          x                                        #    #                                                                                                             
     #       #      +       ##+#####xx             xxxxx       xx#########################    #                                                                                                             
     #       ########                                                                         #                                                                                                             
     #                                                                                        #                                                                                                             
     ##########################################################################################                                                                                                             
",              @"  Level 4 -- Security Checkpoint/Corridor
                                                                                                                 
                          #######x   #############                                                               
                         #x      #x#xx          !#                                                           
                        #x  #xx#       xx#   m  !#################                                              
                        #  ##$  xx  x### #      !#C  #C f#C  #C r#     ###############                           
     #########       #### ############   #      !#   #   #   # r #     # m           #                           
     #       #########       #       #######*#######+###+###+#xx+#######      ###    #                           
     #   <   +         m C   *       +   $       +                     #      #      #                           
     #       +           C   *       +        f  +      f              +      #>#    #                           
     #       #########       #  $    #           #+###+###+xx#*#########    a ###    #                           
     #########       #################   m       # f #   #  r#  0#     #             #                           
                                     #           #C f#C  #C  #C  #     ###############                           
                                     #           #################                                               
                                     #############                                                               
                                                                                                                 
",


        };

        // Security Corridor

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
                item.Chixel = new Chixel('^', ConsoleColor.Blue);
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
                item.Name = "Bent Girder";
                item.Description = "Physics indicate that this would increase your kinetic strike potential. (+1 Melee Damage)";
                item.Chixel = new Chixel('/', ConsoleColor.Blue);
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
                };
                item.OnUnequip += (i, c) =>
                {
                    c.VisionRadius -= 1;
                };
                tile.Item = item;

            };


            ItemSpawner[2, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Gauss Cannon";
                item.Description = "Fires metal slugs at high speed. (+10 Ranged Damage, Costs 10 Energy & 10 Metal Scraps per shot.)";
                item.Chixel = new Chixel('=', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Shoulder;
                item.OnEquip += (i, c) =>
                {
                    c.RangedDamage += 5;
                    c.EnergyPerRanged = 10;
                    c.MoneyPerRanged = 10;
                    c.HasRanged = true;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.RangedDamage -= 5;
                    c.EnergyPerRanged = PlayerCharacter.DefaultEnergyPerRanged;
                    c.MoneyPerRanged = 0;
                    c.HasRanged = false;
                };
                tile.Item = item;
            };

            ItemSpawner[3, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Arc Capacitor";
                item.Description = "(Increases Maximum Energy by 0.0250%)";
                item.Chixel = new Chixel('[', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Chest;
                item.OnEquip += (i, c) =>
                {
                    c.MaxEnergy += 250;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.MaxEnergy -= 250;
                };
                tile.Item = item;
            };


            ItemSpawner[4, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Security Helmet";
                item.Description = "Fits like a glove. Over your fist. (+3 Melee Damage)";
                item.Chixel = new Chixel('A', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Arm;
                item.OnEquip += (i, c) =>
                {
                    c.MeleeDamage += 3;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.MeleeDamage -= 3;
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

