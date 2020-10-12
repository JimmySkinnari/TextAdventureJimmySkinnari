using System;

namespace TextAdventureJimmySkinnari
{
    public class GameArt
    {

        public void GetGameLogo()
        {
            Console.SetWindowSize(147, 30);
            Console.SetBufferSize(147, 30);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(@"

              █████▒▄▄▄       ▄████▄  ▄▄▄█████▓ ▒█████   ██▀███ ▓██   ██▓    ▒█████   ███▄    █      █████▒██▓ ██▀███  ▓█████ 
            ▓██   ▒▒████▄    ▒██▀ ▀█  ▓  ██▒ ▓▒▒██▒  ██▒▓██ ▒ ██▒▒██  ██▒   ▒██▒  ██▒ ██ ▀█   █    ▓██   ▒▓██▒▓██ ▒ ██▒▓█   ▀ 
            ▒████ ░▒██  ▀█▄  ▒▓█    ▄ ▒ ▓██░ ▒░▒██░  ██▒▓██ ░▄█ ▒ ▒██ ██░   ▒██░  ██▒▓██  ▀█ ██▒   ▒████ ░▒██▒▓██ ░▄█ ▒▒███   
            ░▓█▒  ░░██▄▄▄▄██ ▒▓▓▄ ▄██▒░ ▓██▓ ░ ▒██   ██░▒██▀▀█▄   ░ ▐██▓░   ▒██   ██░▓██▒  ▐▌██▒   ░▓█▒  ░░██░▒██▀▀█▄  ▒▓█  ▄ 
            ░▒█░    ▓█   ▓██▒▒ ▓███▀ ░  ▒██▒ ░ ░ ████▓▒░░██▓ ▒██▒ ░ ██▒▓░   ░ ████▓▒░▒██░   ▓██░   ░▒█░   ░██░░██▓ ▒██▒░▒████▒
             ▒ ░    ▒▒   ▓▒█░░ ░▒ ▒  ░  ▒ ░░   ░ ▒░▒░▒░ ░ ▒▓ ░▒▓░  ██▒▒▒    ░ ▒░▒░▒░ ░ ▒░   ▒ ▒     ▒ ░   ░▓  ░ ▒▓ ░▒▓░░░ ▒░ ░
             ░       ▒   ▒▒ ░  ░  ▒       ░      ░ ▒ ▒░   ░▒ ░ ▒░▓██ ░▒░      ░ ▒ ▒░ ░ ░░   ░ ▒░    ░      ▒ ░  ░▒ ░ ▒░ ░ ░  ░
             ░ ░     ░   ▒   ░          ░      ░ ░ ░ ▒    ░░   ░ ▒ ▒ ░░     ░ ░ ░ ▒     ░   ░ ░     ░ ░    ▒ ░  ░░   ░    ░   
                         ░  ░░ ░                   ░ ░     ░     ░ ░            ░ ░           ░            ░     ░        ░  ░
             ░                                     ░ ░                                                          
                                                                                                                          ");
            Animate.Line("                                                           A text adventure game");

            Console.WriteLine("");
            Console.WriteLine("");
            Animate.Line("                                                         Press enter to continue..");
        }
        public string  GetMap()
        {
            return (@"     
                                                     Building Blueprints

                                                        _____________
                                                       |   Factory   |
                                                       |             |
                                                       |             |
                                                       |             |
                                                       |             |
                                          _____________|______\______|_____________
                                         |   Office    |   Coridor   |    Garage   |
                                         |             |             |             |
                                         |              \             \            | 
                                         |             |             |             |
                                         |_____________|______\______|_____________|    
                                                       |             |
                                                       |             |
                                                       |             |
                                                       |             |
                                                       |   Entrance  |
                                                       |_____________|
                                                                                    ");
        }
        public static void ControlsLogo()
        {
            Console.WriteLine(@"                                                                  
                                                                                               
               ░█████╗░░█████╗░███╗░░██╗████████╗██████╗░░█████╗░██╗░░░░░░██████╗
               ██╔══██╗██╔══██╗████╗░██║╚══██╔══╝██╔══██╗██╔══██╗██║░░░░░██╔════╝
               ██║░░╚═╝██║░░██║██╔██╗██║░░░██║░░░██████╔╝██║░░██║██║░░░░░╚█████╗░
               ██║░░██╗██║░░██║██║╚████║░░░██║░░░██╔══██╗██║░░██║██║░░░░░░╚═══██╗
               ╚█████╔╝╚█████╔╝██║░╚███║░░░██║░░░██║░░██║╚█████╔╝███████╗██████╔╝
               ░╚════╝░░╚════╝░╚═╝░░╚══╝░░░╚═╝░░░╚═╝░░╚═╝░╚════╝░╚══════╝╚═════╝░");

        }
        public static void PrintControls()
        {
            ControlsLogo();
            Console.WriteLine("");
            Console.Write("            ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("       C O M M A N D S          /           D E S C R I P T I O N     \n");
            Console.ResetColor();
            Console.WriteLine("\t     ------------------------------------------------------------------ "); 
            Console.WriteLine("\n\t  H                                       Displays Help menu.                ");

            Console.Write("        ");
            ChangeTextForegroundToDarkGray();
            Console.WriteLine("  LOOK                                    Look around the room.               ");

            Console.ResetColor();
            Console.WriteLine("          GET/TAKE/PICK/PICK UP                   Pick up something.                  ");

            Console.Write("        ");
            ChangeTextForegroundToDarkGray();
            Console.WriteLine("  INVENTORY/I                             Check your inventory.               ");

            Console.ResetColor();
            Console.WriteLine("          DROP + (item name)                      Drops an item from your inventory.  ");

            Console.Write("        ");
            ChangeTextForegroundToDarkGray();
            Console.WriteLine("  GO/MOVE + north/east/west/south         Try to go a certain direction.      ");

            Console.ResetColor();
            Console.WriteLine("          INSPECT + (item name)                   Inspect item in Inventory/Room.      ");

            Console.Write("        ");
            ChangeTextForegroundToDarkGray();
            Console.WriteLine("  USE  + (item name)                      Use an item from your inventory.    ");

            Console.ResetColor();

            Animate.Line("\n\n\t\t\t          Press enter to continue..");

        }
        public static void ChangeTextForegroundToDarkGray()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }
    }
}
