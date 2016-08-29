using System;

namespace LD36Quill18
{
    public static class FloorMaps
    {
        public static string[] floor = {
            @"     Level 0

           ##############################
           #                            #             ###############
           #               f            #             #   W     w   #
           #%  >                        ###############      1      #
           #         r                  +             +      f      #
           #%                           ###############             #
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
                                    #&            &#    #                x#                                            
                                    ##            ##    #    f       f  xx#                                           
                                    #&            &#    #              xx<#                                            
                                    ################    ######++####++#####                                            
                                                            xxxxxxxxxxxx    
",            @"  Level 1 -- Fabrication

                                             #####
                                             # < #
                                             #   #
                                             ## ##                                           
                       ########################+#######################                                        
                       #   $   $   $  ##              ##     {~}      #                                         
                       #              ##       B      ##              #                                           
                       #»»»»»»»»»»»»»»++»»»»»»»»»»»»»»++»»»»»»»»»»»»»»# 
                       #              ##              ##              #                                         
                       #  0           ##              ##      r       #                                         
                       #   r          ##   f       f  ##           !  #                                         
                       #              ##              ##              #                                         
                       #««««««««««««««++««««««««««««««++««««««««««««««#
                       #              ##              ##              #                                         
                       #   $   $   $  ##              ##     {~}      #                                         
                       ########################*#######################                                       
                                             ## ##                                                             
                                             #  w#                                                             
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
 #          #     ############   #  &    &  B &    &    &  #   #####+#########+###         ####*########                                                                                                                          
 #          #                    #                         #   #         %      W#         #%         $#                                                                                                              
 #  w       #                    ###########################   #                 #         #           #                                                                                                              
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
     #    #             #       #             #       #           w #      #     {~}     #    #                                                                                                             
     #    # %  %   %  % #       #    f        #       #             #      #        a    #    #                                                                                                             
     #    #             #       #      ! f    #       #   <    $    #      #    f        #    #                                                                                                             
     #    #             #       #             #       #             #      #             #    #                                                                                                             
     #    #######+#######       #######+#######       #######+#######      #######+#######    #                                                                                                             
     #                                                                                        #                                                                                                             
     #                                                                                        #                                                                                                             
     #                      #######+##              x x xxx    xxx################*#######    #                                                                                                             
     #       ########       #$       #          x         x    x                        W#    #                                                                                                             
     #       #  0   *       #        #          x     a   x    x                         #    #                                                                                                             
     #       #  w   #       #%   r   x          x    $    x    x          %$             #    #                                                                                                             
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
                        #  ##$  xx  x### #      !#C  #C f#C B#C r#     ###############                           
     #########       #### ############   #      !#   #   #   # r #     # m           #                           
     #       #########       #       #######*#######+###+###+#xx+#######      ###    #                           
     #   <   +         m C   *  1    +   $       +                     #      #      #                           
     #       +           C   *       +        f  +      f              +      #>#    #                           
     #       #########       #  $    #           #+###+###+xx#*#########    a ###    #                           
     #########       #################   m       # f #   #  r#  0#     #             #                           
                                     #           #C f#C w#C  #C  #     ###############                           
                                     #           #################                                               
                                     #############                                                               


                                                                                                                 
",              @"  Level 5 -- 
                                                                                                                 
                                                                                                                 
                     #####  #####                                                                                
                     # < #  # > #                                                                               
                     #   #  #   #                                                                                
                     ##+##  ##+##                                                                                
                      # ###### #                                                                                 
                      #        #                                                                                 
                      ##########
                                                                                                                 
                                                                                          
",              @"  Level 6 -- 
                                                                                                                 
       ##############     #############       #######                                                            
       #     !     0#     #   {~}     #       # <   #                          #################                 
       #            #     #   m       #########     #                          #        #      #                 
       #  i      i  #     #      m    +       +     #      ################    # %   1  #  $   #                 
       #            #     #$  m      W#########     #      #   r      r   #    #        #    $ #                 
       # %          #     ###+#########       ###+###      #xxxxxxxxxxxxxx#    #        # $    #                 
       #########+####       # #                 # #        #    $         #    #        #      #                 
               # #          # #          ######## ##########          $   ########*#########*###                 
               # ############ #          #   f             +              +                 m #                  
               #              #          #      f ###################################### ######                  
               ################          #   ######                                    # #                       
                                       ###+###                                  ########+#######                 
                                       #     #                                  #w       f     #                 
                                       #  >  #                                  #    f     !   #                 
                                       #######                                  ################                 
                                                                                                                 
",              @"  Level 7 -- 
                                                                                                                 
                                                                                                                 
                     #####  #####                                                                                
                     # < #  # > #                                                                               
                     #   #  #   #                                                                                
                     ##+##  ##+##                                                                                
                      # ###### #                                                                                 
                      #        #                                                                                 
                      ##########
                                                                                                                 
                                                                                          
",              @"  Level 8 -- 
                                                                                                                 
                                                                                                                 
        #############   #############   #############   #############                                                                                   
        #          w#   #           #   #           #   #           #                                            
        #      F    #####  %        #####  M        #####       >   #                                            
        #    F      +   +           +   +   M       +   +   !       #                                            
        #           #####     0     #####          w#####           #                                            
        ####+########   #############   #########*###   ########+####                                                                                   
           # #                                  # #            # #                                               
        ####+########   #############   #########+###   ########+####                                                                                   
        #           #   #  %        #   #        R  #   #w 1     %  #                                            
        #           #####     A     #####   R       ##### t     $   #                                            
        #  <        +   +   F       +   +           +   +      $ $  #                                            
        #           #####       A   #####           #####  t        #                                            
        #############   #############   #############   #############                                                                              
                                                                                                                
                                                                                                                 
                                                                                                                 
                                                                                                                 
",              @"  Level 9 -- 
                                                                                                                 
                                                                                                                 
                                                                                                                 
    ###########################    ###########################                                                                                                 
    #                         #    #                         #                                                     
    #                         #    #      0              <   #                                                     
    #              f          ######                         #                                                     
    #      f                  +    +      f                  #                                                     
    #                  f      ######              f          #                                                     
    #                        $#    #%                        #                                                     
    #        f               ##    #$         f      B   f   #                                                     
    #                f       $#    #%                        #                                                     
    #     f                  ##    #$                f       #                                                     
    #                         ######        f                #                                                     
    #  1       !        f     +    +                         #                                                     
    #                         ######            f            #                                                     
    #  >        f             #    #                         #                                                     
    #                         #    #                         #                                                     
    ###########################    ###########################                                                                                                         
                                                                                                                 
",              @"  Level 10 -- 
                                                                                                                 
                                                                                                                 
                                                                                                                 
            x########x          x########x                                                                       
           x#  {~}   #x        x#        #x                                                                      
           #         W##########     R   $#                                                                      
           #  <       +        +   T   > %#                                                                      
           #         0##########     R   $#                                                                      
           x#  %  %  #x        x#        #x                                                                      
            x########x          x########x                                                                       
                                                                                                                 
                                                                                                                 
",              @"  Level 11 -- 
                                                                                                                 
                                                                                                                 
                                                                                                                 
            x########x          x########x                                                                       
           x#     $  #x        x#        #x                                                                      
           #$   s     ##########     F   $#                                                                      
           #  >       +   0    +   f   < %#                                                                      
           #    s     ##########      F  $#                                                                      
           x#  %     #x        x#  !     #x                                                                      
            x########x          x########x                                                                       
                                                                                                                 
                                                                                                                 
",              @"  Level 12 -- 
                                                                                                                 
                                                                                                                 
                                                                                                                 
                                                                                                                 
      ##############################################################                                                                                                           
      #                 x                                          #                                               
      #  $      x       x      x     #    #M   #       R        D  #                                               
      #         x    F  x   A  x                             >     #                                               
      #         x       x  A   x     #0   #m   #                   #                                               
      # w       x   F   x      x                      $   r     !  #                                               
      #   <     x       x%     x     #    #M   #                   #                                               
      #         x              x                      !            #                                               
      ##############################################################                                                                                                             
                                                                                                                 
                                                                                                                 
                                                                                                                 
                                                                                                                 
",              @"  Level 13-- 
                                                                                                                 
                                                                                                                
           #################              #############                                                          
           #          $    ################   !       #                                                          
           #  f            *                     f    #                                                          
           #     $ FF      ################           #    #####################                                 
           #            $0 #              # $ M       #    #                   #                                
           ###++############              #      f    ######     F         F   #                                 
             #  #                         #####       +    +          R        #                                 
         #####++###                           #       ######                   #       ######                    
         #        #   ###################     # $    W#    ############        #      ##    ##                   
         # ! rF   #####  $     {~}  $   #     ###+#####               #   f R  #      #     t#                  
         #        +   +       M       W #       # #                   #        ######## T    #                   
         ##############          R      ######### #                   #        *          Y S#                   
                      #          f      *      r  #                   #        ######## T    #                   
        ############  # B m$         $  #####*#####                   #        #      #     t#                   
        #  !  !    #  ########## ########   # #            ############   m    #      ##    ##                  
        #    s     #           # #          # #            # %                 #       ######                    
        #  T    t W#           # #          # ##############           F       #                                 
        ####+#######           # #          #              *    f              #                                 
        #      $  %############# #          ################                   #                                 
        #  F     F *             #                         #####################                                
        #          ###############                                                                               
        #########+##                                                                                             
        #      w   #                                                                                             
        #   <      #                                                                                             
        #          #                                                                                            
        ############                                                                                             
                                                                                                                 
                                                                                                                 
                                                                                                                 
                                                                                                                 
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

            ItemSpawner[1, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Pneumatic Piston";
                item.Description = "A pneumatic leap, just when you need it. (+1 Dodge)";
                item.Chixel = new Chixel('L', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Legs;
                item.OnEquip += (i, c) =>
                {
                    c.DodgeBonus += 1;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.DodgeBonus -= 1;
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
                    c.RangedDamage += 10;
                    c.EnergyPerRanged = 10;
                    c.MoneyPerRanged = 10;
                    c.HasRanged = true;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.RangedDamage -= 10;
                    c.EnergyPerRanged = PlayerCharacter.DefaultEnergyPerRanged;
                    c.MoneyPerRanged = 0;
                    c.HasRanged = false;
                };
                tile.Item = item;
            };

            ItemSpawner[3, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Surge Protector";
                item.Description = "To balance out your erratic power supply. (+50 Max Health and +0.0050% Max Energy)";
                item.Chixel = new Chixel('[', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Chest;
                item.OnEquip += (i, c) =>
                {
                    c.MaxEnergy += 50;
                    c.MaxHealth += 50;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.MaxEnergy -= 50;
                    c.MaxHealth -= 50;
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



            ItemSpawner[4, 1] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Deflector Shield";
                item.Description = "Repels fast-moving objects. (+1 Damage Reduction)";
                item.Chixel = new Chixel('A', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Shield;
                item.OnEquip += (i, c) =>
                {
                    c.DamageReduction += 1;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.DamageReduction -= 1;
                };
                tile.Item = item;
            };


            ItemSpawner[6, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Lidar";
                item.Description = "A Laser-enhanced sensor package(+2 Vision Range, +2 Accuracy)";
                item.Chixel = new Chixel('^', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Head;
                item.OnEquip += (i, c) =>
                {
                    c.VisionRadius += 2;
                    c.ToHitBonus += 2;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.VisionRadius -= 2;
                    c.ToHitBonus -= 2;
                };
                tile.Item = item;
            };

            ItemSpawner[6, 1] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Myomer Actuator";
                item.Description = "A leftover from the Clan Wars. (+2 Dodge)";
                item.Chixel = new Chixel('^', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Legs;
                item.OnEquip += (i, c) =>
                {
                    c.DodgeBonus += 2;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.DodgeBonus -= 2;
                };
                tile.Item = item;
            };


            ItemSpawner[8, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Particle Cannon";
                item.Description = "Fires a stream of particle at near C-velocity. (+20 Ranged Damage, Costs 20 Energy per Shot.)";
                item.Chixel = new Chixel('=', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Shoulder;
                item.OnEquip += (i, c) =>
                {
                    c.RangedDamage += 20;
                    c.EnergyPerRanged = 20;
                    c.HasRanged = true;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.RangedDamage -= 20;
                    c.EnergyPerRanged = PlayerCharacter.DefaultEnergyPerRanged;
                    c.HasRanged = false;
                };
                tile.Item = item;
            };

            ItemSpawner[8, 1] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Capacitor Array";
                item.Description = "Optimized for burst activities. (+125 Max Health and +0.0125% Max Energy)";
                item.Chixel = new Chixel('[', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Chest;
                item.OnEquip += (i, c) =>
                {
                    c.MaxEnergy += 125;
                    c.MaxHealth += 125;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.MaxEnergy -= 125;
                    c.MaxHealth -= 125;
                };
                tile.Item = item;
            };


            ItemSpawner[9, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Adamantine Spike";
                item.Description = "Humans dug deeply and greedily to find this. (+5 Melee Damage)";
                item.Chixel = new Chixel('A', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Arm;
                item.OnEquip += (i, c) =>
                {
                    c.MeleeDamage += 5;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.MeleeDamage -= 5;
                };
                tile.Item = item;
            };

            ItemSpawner[9, 1] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Kinetic Shield";
                item.Description = "A near-impenetrable barrier. (+2 Damage Reduction)";
                item.Chixel = new Chixel('A', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Shield;
                item.OnEquip += (i, c) =>
                {
                    c.DamageReduction += 2;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.DamageReduction -= 2;
                };
                tile.Item = item;
            };



            ItemSpawner[10, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Reaction Control System";
                item.Description = "A complete damage-avoidance system. (+3 Dodge)";
                item.Chixel = new Chixel('L', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Legs;
                item.OnEquip += (i, c) =>
                {
                    c.DodgeBonus += 3;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.DodgeBonus -= 3;
                };
                tile.Item = item;

            };

            ItemSpawner[11, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Arc Capacitor";
                item.Description = "Very shiny. (+250 Max Health and +0.0250% Max Energy)";
                item.Chixel = new Chixel('[', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Chest;
                item.OnEquip += (i, c) =>
                {
                    c.MaxEnergy += 250;
                    c.MaxHealth += 250;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.MaxEnergy -= 250;
                    c.MaxHealth -= 250;
                };
                tile.Item = item;
            };

            ItemSpawner[12, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Electron Sonar";
                item.Description = "Scans through solid walls with ease. (+3 Vision Range, +3 Accuracy)";
                item.Chixel = new Chixel('^', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Head;
                item.OnEquip += (i, c) =>
                {
                    c.VisionRadius += 3;
                    c.ToHitBonus += 3;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.VisionRadius -= 3;
                    c.ToHitBonus -= 3;
                };
                tile.Item = item;
            };

            ItemSpawner[13, 0] = (tile) =>
            {
                Item item = new Item();
                item.Name = "Harmonic Shield";
                item.Description = "Stores kinetic energy to be used in offense. (+3 Damage Reduction, +1 Melee Damage)";
                item.Chixel = new Chixel('A', ConsoleColor.Blue);
                item.OnUse += (i) => { PlayerCharacter.Instance.Equip(i); };
                item.EquipSlot = EquipSlot.Shield;
                item.OnEquip += (i, c) =>
                {
                    c.DamageReduction += 3;
                    c.MeleeDamage += 1;
                };
                item.OnUnequip += (i, c) =>
                {
                    c.DamageReduction -= 3;
                    c.MeleeDamage -= 1;
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

