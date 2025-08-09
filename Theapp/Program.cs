using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.NetworkInformation;
//using System.Data.SqlClient;

//using System.Xml.Linq;


namespace Theapp


{
    class NPC
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        

        public List<NPC> NPCs { get; set; }
        public List<NPC> Walls { get; set; }

        public void TalkToMain<T>(List<T> script, MainPlayer main, List<string> cities = null)
        {
            if (typeof(T) == typeof(string))
            {
                try
                {
                    foreach (var item in script)
                    {
                        string line = item as string;
                        if (line != null)
                        {
                            foreach (char c in line)
                            {
                                Console.Write(c);
                                Thread.Sleep(30); // Typewriter effect
                            }
                            Console.WriteLine();
                            Thread.Sleep(900); // Dramatic pause
                        }
                        else
                        {
                            Console.WriteLine("The script contains elements that are not strings.");
                        }
                    }
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("The script contains elements that are not strings.");
                }
            }
            else if (typeof(T) == typeof(KeyValuePair<string, string>))
            {
                foreach (var item in script)
                {
                    var keypairScript = (KeyValuePair<string, string>)(object)item;
                    Console.ForegroundColor = ConsoleColor.Green;
                    foreach (char c in keypairScript.Key)
                    {
                        Console.Write(c);
                        Thread.Sleep(25); // Dramatic riddle delivery
                    }
                    Console.ResetColor();
                    Console.WriteLine();
                    Thread.Sleep(700);

                    Console.Write("Your answer: ");
                    string answer = Console.ReadLine();
                    Thread.Sleep(400);

                    if (keypairScript.Value.Equals(answer?.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Correct, detective. But the game is not over yet...");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (cities != null && cities.Count > 0)
                        {
                            string blastedCity = cities[0];
                            cities.RemoveAt(0);
                            Console.WriteLine($"Wrong! The Riddler laughs menacingly... {blastedCity} has been blasted off the map!");
                            if (cities.Count == 0)
                            {
                                Console.WriteLine("All the cities are gone. The Riddler wins. Game over!");
                                Thread.Sleep(1500);
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong! The Riddler laughs menacingly...");
                        }
                        Console.ResetColor();
                    }
                    Thread.Sleep(1200); // Pause before next riddle
                }
            }
        }

    }


    class Character : NPC
    {

    }
    class MainPlayer { 

        public string Name { get; set; }
        public string Description { get; set; }
        private static float cash { get; set; }
        public int Level { get; set; }

        public  void AddTokens(float tokens) { cash +=  tokens;  }
        public void RemoveTokens(float tokens) { cash -= tokens; }
        public float GetTokens() { return cash; }

    }

    internal class Program
    {
   
        static int returnTable(int num)
        {
            int temp = num;
            for(int i = 1; i <= 10; i++) { 
                Console.WriteLine(num + " x " + i + " = " + temp * i);
            }
            return 0;
        }

        static void PrintBlinkingQuestionMark(int times = 4, int delay = 350)
        {
            string[] questionMark = new string[]
            {
                "   #####   ",
                "  #     #  ",
                "        #  ",
                "      ##   ",
                "     #     ",
                "           ",
                "     #     ",
                "           "
            };

            // Improved, more question-mark-like ASCII art
            string[] improvedQuestionMark = new string[]
            {
                "   #####   ",
                "  #     #  ",
                "        #  ",
                "      ##   ",
                "     #     ",
                "           ",
                "     #     ",
                "           "
            };

            // Hide the cursor
            bool prevCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            int top = Console.CursorTop;
            int left = Math.Max(0, Console.WindowWidth / 2 - improvedQuestionMark[0].Length / 2);

            for (int t = 0; t < times; t++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                for (int i = 0; i < improvedQuestionMark.Length; i++)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(improvedQuestionMark[i]);
                }
                Console.ResetColor();
                Thread.Sleep(delay);

                // Erase the question mark
                for (int i = 0; i < improvedQuestionMark.Length; i++)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(new string(' ', improvedQuestionMark[i].Length));
                }
                Thread.Sleep(delay);
            }
            // Print it one last time
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < improvedQuestionMark.Length; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.Write(improvedQuestionMark[i]);
            }
            Console.ResetColor();

            // Restore cursor visibility
            Console.CursorVisible = prevCursorVisible;
            Console.SetCursorPosition(0, top + improvedQuestionMark.Length + 1);
}

        static void Main(string[] args)
        {
            PrintBlinkingQuestionMark();
            Console.WriteLine("Before we play, a puzzle for you: tell me, what name do you wear like a mask? ");
            string name_main = Console.ReadLine();
            MainPlayer main = new MainPlayer();
            main.Name = name_main;

            NPC riddler = new NPC();
            riddler.Name = "The Riddler";
            riddler.Description = "A criminal mastermind obsessed with riddles, puzzles, and word games. Can you solve his challenges?";
            List<string> script = new List<string>()
            {
                "(A shadowy figure steps from the darkness...)",
                "(A green question mark glows in the gloom.)",
                $"Bruuuuuuuuuuce Waaaaaaaaaaaaaaaaaaaayne...",
                $"You came, just as I predicted.",
                $"(The Riddler grins, twirling his cane)",
                $"Ah, the great Detective. Always so sure of himself…",
                $"Tell me, Batman, how quickly do you think you can think?",
                $"Six riddles. Six cities. Ticking clocks and ticking bombs.",
                $"It’s a beautiful equation — but only if you get the answers right."

            };
            riddler.TalkToMain(script, main);

            // List of cities at stake
            List<string> cities = new List<string> { "Gotham", "Metropolis", "Blüdhaven", "Central City", "Star City", "Coast City" };

            List<KeyValuePair<string, string>> riddles = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Riddle me this: I speak without a mouth and hear without ears. I have no body, but I come alive with wind. What am I?", "an echo"),
                new KeyValuePair<string, string>("Riddle me this: The more of this there is, the less you see. What is it?", "darkness"),
                new KeyValuePair<string, string>("Riddle me this: I can be cracked, made, told, and played. What am I?", "a joke"),
                new KeyValuePair<string, string>("Riddle me this: What belongs to you, but others use it more than you do?", "your name"),
                new KeyValuePair<string, string>("Riddle me this: I am always in front of you but can’t be seen. What am I?", "the future"),
                new KeyValuePair<string, string>("Riddle me this: What has many keys but can't even open a single door?", "a piano")
            };

            riddler.TalkToMain(riddles, main, cities);
            Thread.Sleep(1500);
Console.WriteLine();
string alfredIntro = "(A new message appears on the Batcomputer:)";
foreach (char c in alfredIntro) { Console.Write(c); Thread.Sleep(30); }
Console.WriteLine();
Thread.Sleep(900);

string bombThreat = "The Riddler: \"One last game, Batman. Alfred is in the mansion, and a bomb ticks beside him. Answer this, or he pays the price!\"";
foreach (char c in bombThreat) { Console.Write(c); Thread.Sleep(25); }
Console.WriteLine();
Thread.Sleep(1200);

// Psychological riddle
string psychRiddle = "Riddle me this: I can only live where there is light, but I die if the light shines on me. What am I?";
Console.ForegroundColor = ConsoleColor.Green;
foreach (char c in psychRiddle) { Console.Write(c); Thread.Sleep(20); }
Console.ResetColor();
Console.WriteLine();
Thread.Sleep(700);

Console.Write("Your answer: ");
string psychAnswer = Console.ReadLine();
Thread.Sleep(400);

if ("a shadow".Equals(psychAnswer?.Trim(), StringComparison.OrdinalIgnoreCase) || "shadow".Equals(psychAnswer?.Trim(), StringComparison.OrdinalIgnoreCase))
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Correct, Batman. Alfred is safe... for now.");
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Wrong! The Riddler cackles as the bomb detonates in Wayne Manor...");
    Console.WriteLine("Alfred has paid the ultimate price. Game over!");
    Console.ResetColor();
    Thread.Sleep(2000);
    Environment.Exit(0);
}
        }
    }


}
