using System;

namespace TextAdventureJimmySkinnari
{
    class Program
    {
        static void Main(string[] args)
        {
            GameArt ga = new GameArt();
            ga.GetMap();
            Game game = new Game();
            game.PlayGame();


            Console.ReadLine();
        }
    }
}
