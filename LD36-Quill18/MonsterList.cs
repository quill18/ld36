using System;
using System.Collections.Generic;

namespace LD36Quill18
{
    static public class MonsterList
    {
        static MonsterList()
        {
            Monsters = new Dictionary<char, MonsterCharacter>();

            char c;

            c = 'f';
            Monsters[c] = new MonsterCharacter();
            Monsters[c].Name = "Reptoid Footsoldier";
            Monsters[c].Description = "";
            Monsters[c].Chixel = new Chixel(c, ConsoleColor.Green);
            Monsters[c].Health = Monsters[c].MaxHealth = 5;
            Monsters[c].MeleeDamage = 4;

            c = 'm';
            Monsters[c] = new MonsterCharacter();
            Monsters[c].Name = "Tuboid Moderator";
            Monsters[c].Description = "";
            Monsters[c].Chixel = new Chixel(c, ConsoleColor.Magenta);
            Monsters[c].Health = Monsters[c].MaxHealth = 10;
            Monsters[c].MeleeDamage = 6;


            c = 'i';
            Monsters[c] = new MonsterCharacter();       // TODO: NEEDS TO BE FAST
            Monsters[c].Name = "Reptoid Infiltrator";
            Monsters[c].Description = "";
            Monsters[c].Chixel = new Chixel(c, ConsoleColor.Green);
            Monsters[c].Health = Monsters[c].MaxHealth = 5;
            Monsters[c].MeleeDamage = 4;


            c = 't';
            Monsters[c] = new MonsterCharacter();
            Monsters[c].Name = "Behemoid Taskmaster";
            Monsters[c].Description = "";
            Monsters[c].Chixel = new Chixel(c, ConsoleColor.Red);
            Monsters[c].Health = Monsters[c].MaxHealth = 20;
            Monsters[c].MeleeDamage = 10;
            Monsters[c].DamageReduction = 2;

            // RANGED

            c = 'a';
            Monsters[c] = new MonsterCharacter();       // Runs away, fires at long range
            Monsters[c].Name = "Reptoid Sniper";
            Monsters[c].Description = "";
            Monsters[c].Chixel = new Chixel(c, ConsoleColor.Red);
            Monsters[c].Health = Monsters[c].MaxHealth = 5;
            Monsters[c].MeleeDamage = 3;
            Monsters[c].RangedDamage = 5;
            Monsters[c].RangedAmmo = 10;
            Monsters[c].MaximumRange = 10;
            Monsters[c].TriesToKeepMinimumDistance = 5;

            c = 's';
            Monsters[c] = new MonsterCharacter();           // Toss 1 spear at short range, then charge into melee.
            Monsters[c].Name = "Behemoid Siegebreaker";
            Monsters[c].Description = "";
            Monsters[c].Chixel = new Chixel(c, ConsoleColor.Red);
            Monsters[c].Health = Monsters[c].MaxHealth = 20;
            Monsters[c].MeleeDamage = 10;
            Monsters[c].RangedDamage = 10;
            Monsters[c].RangedAmmo = 1;
            Monsters[c].MaximumRange = 5;


            c = 'r';
            Monsters[c] = new MonsterCharacter();       // Medium Range, doesn't run away
            Monsters[c].Name = "Tuboid Reporter";
            Monsters[c].Description = "";
            Monsters[c].Chixel = new Chixel(c, ConsoleColor.Red);
            Monsters[c].Health = Monsters[c].MaxHealth = 10;
            Monsters[c].MeleeDamage = 5;
            Monsters[c].RangedDamage = 5;
            Monsters[c].RangedAmmo = 5;
            Monsters[c].MaximumRange = 7;



            Dictionary<char, MonsterCharacter> BaseMonsters = new Dictionary<char, MonsterCharacter>(Monsters);
            foreach (MonsterCharacter baseMonster in BaseMonsters.Values)
            {
                // Make an elite version in uppercase?
                // Make sure no collision with items
                char newC = baseMonster.Chixel.Glyph.ToString().ToUpper()[0];
                MonsterCharacter newM = new MonsterCharacter(baseMonster);
                newM.Name = "Elite " + newM.Name ;
                Chixel ch = new Chixel(baseMonster.Chixel);
                ch.Glyph = newC;
                newM.Chixel = ch;
                Monsters.Add(newC, newM);
            }


        }

        static public Dictionary<char, MonsterCharacter> Monsters;

    }
}

