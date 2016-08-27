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

            c = 's';
            Monsters[c] = new MonsterCharacter();
            Monsters[c].Name = "Sectoid";
            Monsters[c].Description = "Completely different from that thing from that game.";
            Monsters[c].Chixel = new Chixel(c, ConsoleColor.Green);
            Monsters[c].Health = Monsters[c].MaxHealth = 5;
            Monsters[c].MeleeDamageFunc = () => { return Game.Instance.Random.Next(1, 5); };

            c = 'd';
            Monsters[c] = new MonsterCharacter();
            Monsters[c].Name = "Demon Dog";
            Monsters[c].Description = "Dog says \"woof\".";
            Monsters[c].Chixel = new Chixel(c, ConsoleColor.Red);
            Monsters[c].Health = Monsters[c].MaxHealth = 10;
            Monsters[c].MeleeDamageFunc = () => { return Game.Instance.Random.Next(1, 11); };



        }

        static public Dictionary<char, MonsterCharacter> Monsters;

    }
}

