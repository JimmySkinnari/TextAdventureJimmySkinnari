using System;

namespace TextAdventureJimmySkinnari
{
    class Animate
    {
        public static void Write (string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != ' ')
                {
                    Console.Write(line[i]);
                    System.Threading.Thread.Sleep(25); // Sleep for 150 milliseconds
                }
                else
                {
                    Console.Write(line[i]);
                }
            }

            Console.ResetColor();
        }
        public static void Line(string line, ConsoleColor color)
        {
            Console.WriteLine("");
            Console.ForegroundColor = color;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != ' ')
                {
                    Console.Write(line[i]);
                    System.Threading.Thread.Sleep(25); // Sleep for 150 milliseconds
                }
                else
                {
                    Console.Write(line[i]);
                }
            }

            Console.ResetColor();
        }
        public static void Line(string line)
        {
            Console.WriteLine("");

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != ' ')
                {
                    Console.Write(line[i]);
                    System.Threading.Thread.Sleep(25); // Sleep for 150 milliseconds
                }
                else
                {
                    Console.Write(line[i]);
                }
            }
        }
    }
}
