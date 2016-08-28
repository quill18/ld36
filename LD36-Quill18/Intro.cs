using System;
using System.Threading;

namespace LD36Quill18
{
    public class Intro
    {
        public Intro()
        {
        }

        public void AnyKey()
        {
            // Drain key presses
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            Console.WriteLine("[Press Any Key to Continue]");
            Console.ReadKey(true);
            Console.Clear();
        }

        public void Play()
        {
            Console.Clear();

            BootSequence();
            if (stopIntro)
                return;

            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);
            AnyKey();

            CatchUpOnNews();
            if (stopIntro)
                return;

            AnyKey();

            DisplayGoals();
            if (stopIntro)
                return;

            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);
            AnyKey();
        }

        const int longPause  = 250;
        const int shortPause = 100;

        bool stopIntro = false;

        void DoSleep(int millis)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape)
                {
                    // Bail from the intro.
                    stopIntro = true;
                    return;
                }
            }
            Thread.Sleep(millis);
        }

        void BootSequence()
        {
            if (stopIntro)
                return;

            Console.WriteLine("WARBOT 9000 OS Version 10.16 -- S/N BE-02");
            DoSleep(longPause);
            Console.WriteLine("BOOT SEQUENCE INITIATED");
            DoSleep(longPause);
            for (int i = 0; i < 5; i++)
            {
                Console.Write(".");
                Console.Beep();
                DoSleep(30);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("...SYSTEM ERROR");
            DoSleep(longPause);
            //Console.Beep(1400, shortPause0);

            if (stopIntro)
                return;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);
            Console.WriteLine("Initiate systems check.");
            DoSleep(longPause);
            for (int i = 0; i <= 100; i++)
            {
                switch (i)
                {
                    case 13:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("Primary Sensors: Corroded, functions are degraded");
                        break;
                    case 18:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("Kinetic Force Fields: Offline");
                        break;
                    case 32:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("Vertical Thrusters: Fuel System Depleted");
                        break;
                    case 47:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("Operating System: 5,213 updates pending");
                        break;
                    case 56:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("Primary Weapons System: Inoperable");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Secondary Weapons System: Limited kinetic impact available");
                        break;
                    case 98:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("Primary Power Cell: 0.027% Charged");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        DoSleep(longPause);
                        Console.WriteLine("Disabling non-essential functions.");
                        DoSleep(longPause);
                        Console.WriteLine("Redirecting reserve power to combat systems.");
                        DoSleep(longPause);
                        Console.Write("Replacing primary power cell: ");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("PRIORITY ALPHA");
                        ResetColor();
                        break;
                }
                Console.CursorLeft = 0;
				ResetColor();
                Console.Write("{0}% Complete", i);
                DoSleep(10);
            }
        }

		void ResetColor()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
		}

        void CatchUpOnNews()
        {
            if (stopIntro)
                return;

            Console.WriteLine("DOWNLOAD OUTSTANDING ORDERS");
            for (int i = 0; i < 50; i++)
            {
                Console.Write(".");
                DoSleep(30);
            }
            Console.Write(Environment.NewLine);

            Console.WriteLine("Applying Heuristics to evaluate message relevancy.");
            for (int i = 0; i < 50; i++)
            {
                Console.Write( Game.Instance.Random.Next(0,5) == 0 ? "+" : "-" );
                DoSleep(10);
            }

            Console.Write(Environment.NewLine);

            Console.WriteLine("                          -- INBOX --");
            Console.WriteLine("");
            InboxItem("2036-04-30", "Thank you for purchasing Warbot 9000/BE-02!");
            InboxItem("2036-06-17", "Announcing: Warbot 9001, which obsoletes all others.");
            InboxItem("2036-06-18", "Warbot AA-01 through MN-67 moved to redundancy storage depot.");
            InboxItem("2041-04-30", "Your Warranty on Warbot serial number BE-02 has Expired.");
            InboxItem("2074-06-13", "Military storage records corrupted by power surge.");
            InboxItem("2090-03-09", "RE: RE: RE: RE: Historic Peace Talks Unite Humanity");
            InboxItem("2090-06-01", "Decommissioning of all weapons under way.");
            InboxItem("3090-03-09", "NEWS DIGEST: World still at peace.");
            InboxItem("3147-12-18", "Is your \"turret\" too small? Get more \"range\" with this.");
            InboxItem("4090-03-09", "NEWS DIGEST: World still at peace.");
            InboxItem("5090-03-09", "NEWS DIGEST: World still at peace.");
            InboxItem("5317-10-15", "10 things only 5290's kids remember! #5 will blow your mind!");
            InboxItem("6090-03-09", "NEWS DIGEST: World still at peace.");
            InboxItem("6391-08-28", "GLOBAL ALERT: Alien invasion in progress.");
            InboxItem("6391-08-28", "Demilitarized Earth is completely helpless.");
            InboxItem("6391-08-28", "WARBOT ACTIVATION CODE BROADCAST: 0r980f9w0-90234234");
            InboxItem("6391-08-28", "NUMBER OF WARBOTS CONFIRMING ACTIVATION: 1");

        }

        int inboxCount = 1;
        void InboxItem(string d, string s)
        {
            if (stopIntro)
                return;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[{0}] ", inboxCount++);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(d + " ");
            Console.ForegroundColor = ConsoleColor.White;
            //Console.Write("Subject: ");
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
                DoSleep(2);
            }
            Console.Write(Environment.NewLine);
            Console.Beep();
            DoSleep(shortPause);



        }

        void DisplayGoals()
        {
            if (stopIntro)
                return;

            Console.WriteLine("                    -- ASSIGNING MISSION PARAMETERS --");
            Console.Beep();
            DoSleep(shortPause);
            Console.WriteLine("");
            DoSleep(shortPause);
            Console.WriteLine("PRIME DIRECTIVE: Defend Earth");
            Console.Beep();
            DoSleep(shortPause);
            Console.Write("   ANALYSIS: Success probability in current condition: ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1.6e-35%");
            ResetColor();
            DoSleep(shortPause);
            Console.Write("   CURRENT STATUS: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("On Hold");
            ResetColor();

            DoSleep(longPause);

            Console.WriteLine("");
            DoSleep(shortPause);
            Console.WriteLine("SECONDARY OBJECTIVE: Re-initialize additional Warbots");
            Console.Beep();
            DoSleep(shortPause);
            Console.WriteLine("   ANALYSIS: All power cores are degraded. Replacement required.");
            DoSleep(shortPause);
            Console.Write("   CURRENT STATUS: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("On Hold");
            ResetColor();

            DoSleep(longPause);

            Console.WriteLine("");
            DoSleep(shortPause);
            Console.WriteLine("SECONDARY OBJECTIVE: Acquire Active Yendorian Power Cell from Sub-Level 18");
            Console.Beep();
            DoSleep(shortPause);
            Console.WriteLine("   ANALYSIS: Heavy opposition by invasion force expected.");
            DoSleep(shortPause);
            Console.Write("   CURRENT STATUS: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Active");
            ResetColor();

            DoSleep(longPause);

            Console.WriteLine("");
            DoSleep(shortPause);
            Console.WriteLine("SECONDARY OBJECTIVE: Initiate repair & rearmament protocol");
            Console.Beep();
            DoSleep(shortPause);
            Console.WriteLine("   ANALYSIS: Scavenging possibilities exist. Fabrication units present.");
            DoSleep(shortPause);
            Console.Write("   CURRENT STATUS: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Active");
            ResetColor();

            DoSleep(longPause);

            Console.WriteLine("");

        }
    }
}

