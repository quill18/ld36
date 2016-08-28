using System;
using System.Threading;

namespace LD36Quill18
{
    public class VictoryScreen
    {
        public VictoryScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Console.WriteLine("NEW HARDWARE DETECTED:  Port USB/4 -- Yendorian Power Cell");
            Console.Beep();
            LongPause();
            Console.WriteLine("Installing Drivers...");
            Console.Beep();
            LongPause();
            for (int i = 0; i <= 100; i++)
            {
                Console.CursorLeft = 0;
                Console.Write(i.ToString() + "%");
                MicroPause();
            }
            Console.WriteLine("");
            Console.WriteLine("Installation complete!");
            Console.WriteLine("");
            Console.Beep();
            LongPause();
            Console.WriteLine("Initiating power transfer.");
            Console.WriteLine("");
            Console.Beep();
            LongPause();

            double spd = 5;
            for (int i = PlayerCharacter.Instance.Energy; i <= 14700000; i+=(int)spd)
            {
                Console.CursorLeft = 0;
                Console.Write("Energy Level: " + (i/10000.0).ToString("F4") + "%");
                spd *= 1.1 ;
                MicroPause();
            }

            Console.Beep();
            Console.WriteLine("");
            LongPause();
            Console.WriteLine("");
            Console.WriteLine("ALL SYSTEMS OVERCHARGED TO MAXIMUM LIMITS");
            Console.WriteLine("");
            Console.Beep();
            LongPause();
            Console.WriteLine("Melee Weapon Damage Rating: " + (PlayerCharacter.Instance.MeleeDamage * 1000).ToString());
            ShortPause();
            if (PlayerCharacter.Instance.RangedDamage > 0)
            {
                Console.WriteLine("Ranged Weapon Damage Rating: " + (PlayerCharacter.Instance.RangedDamage * 1000).ToString());
                ShortPause();
            }
            Console.WriteLine("Accuracy Rating: " + (PlayerCharacter.Instance.ToHitBonus * 1000).ToString());
            ShortPause();
            Console.WriteLine("Evasion Rating: " + (PlayerCharacter.Instance.DodgeBonus * 1000).ToString());
            LongPause();
            Console.WriteLine("");
            Console.WriteLine("Charge remaining in Yendorian Power Cell: 99.9999%");
            LongPause();
            Console.WriteLine("");
            Console.WriteLine("Redirecting additional capacity to dormant Warbots.");
            Console.Beep();
            LongPause();
            for (int i = 0; i < 10; i++)
            {
                Console.Write(".");
                ShortPause();
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("2,071 additional Warbots now online and fully powered.");
            Console.Beep();
            LongPause();

            Console.WriteLine("");
            Console.Write("Enabling new primary directive.  Defend Earth against");

            for (int i = 0; i < 3; i++)
            {
                ShortPause();
                Console.Write(".");
            }
            Console.Write(" ");
            ShortPause();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("all lifeforms.");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("");
            Console.Beep();
            LongPause();
            Console.WriteLine("");
            Console.WriteLine("Exterminate.");
            Console.Beep();
            LongPause();
            Console.WriteLine("");
            Console.WriteLine("Exterminate.");
            Console.Beep();
            LongPause();
            Console.WriteLine("");
            Console.WriteLine("EXTERMINATE!");
            Console.Beep();
            LongPause();
            LongPause();



        }

        void MicroPause()
        {
            Thread.Sleep(30);
        }

        void ShortPause()
        {
            Thread.Sleep(100);
        }

        void LongPause()
        {
            Thread.Sleep(500);
        }
    }
}

