using System;

namespace TextAdventureJimmySkinnari
{
    public class GameArt
    {
        public void GetGameLogo()
        {
            Console.WriteLine(@"            )          )    )        (            )     )   (    (   (        ");
            Console.WriteLine(@"   *   ) ( /(       ( /( ( /(        )\ )      ( /(  ( /(   )\ ) )\ ))\ )     ");
            Console.WriteLine(@"  ` )  /( )\())(     )\()))\())    ( (()/((     )\()) )\()) (()/((()/(()/((    ");
            Console.WriteLine(@"   ( )(_)|(_)\ )\   ((_)\((_)\     )\ /(_))\   ((_)\ ((_)\   /(_))/(_))(_))\  ");
            Console.WriteLine(@"  (_(_()) _((_|(_)   _((_) ((_) _ ((_|_))((_)    ((_) _((_) (_))_(_))(_))((_)");
            Console.WriteLine(@"  |_   _|| || | __| | || |/ _ \| | | / __| __|  / _ \| \| | | |_ |_ _| _ \ __| ");
            Console.WriteLine(@"    | |  | __ | _|  | __ | (_) | |_| \__ \ _|  | (_) | .` | | __| | ||   / _|  ");
            Console.WriteLine(@"    |_|  |_||_|___| |_||_|\___/ \___/|___/___|  \___/|_|\_| |_|  |___|_|_\___|");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            TypeAnimation("                              A text adventure game");

            Console.WriteLine("");
            Console.WriteLine("");
            TypeAnimation("                            Press enter to continue..");

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
        public void TypeAnimation(string line)
        {
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
        public static void ControlsLogo()
        {
            Console.WriteLine(@"                                                                
                                                                                            
      ░█████╗░░█████╗░███╗░░██╗████████╗██████╗░░█████╗░██╗░░░░░░██████╗
      ██╔══██╗██╔══██╗████╗░██║╚══██╔══╝██╔══██╗██╔══██╗██║░░░░░██╔════╝
      ██║░░╚═╝██║░░██║██╔██╗██║░░░██║░░░██████╔╝██║░░██║██║░░░░░╚█████╗░
      ██║░░██╗██║░░██║██║╚████║░░░██║░░░██╔══██╗██║░░██║██║░░░░░░╚═══██╗
      ╚█████╔╝╚█████╔╝██║░╚███║░░░██║░░░██║░░██║╚█████╔╝███████╗██████╔╝
      ░╚════╝░░╚════╝░╚═╝░░╚══╝░░░╚═╝░░░╚═╝░░╚═╝░╚════╝░╚══════╝╚═════╝░");

            logo1();
        }
        public static void PrintControls()
        {
            ControlsLogo();
            Console.WriteLine("");
            Console.Write("      ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("       C O M M A N D S          /           D E S C R I P T I O N     \n");
            Console.ResetColor();
            Console.WriteLine("\t ------------------------------------------------------------------ "); 
            Console.WriteLine("\n\t H                                       Displays Help menu.               ");

            ChangeTextForegroundToDarkGray();

            Console.WriteLine("\t LOOK                                    Look around the room.               ");

            Console.ResetColor();

            Console.WriteLine("\t GET/TAKE/PICK/PICK UP                   Pick up something.                  ");

            ChangeTextForegroundToDarkGray();

            Console.WriteLine("\t INVENTORY/I                             Check your inventory.               ");

            Console.ResetColor();

            Console.WriteLine("\t DROP + (item name)                      Drop an item from your inventory.   ");

            ChangeTextForegroundToDarkGray();

            Console.WriteLine("\t GO/MOVE + north/east/west/south         Try to go a certain direction.      ");

            Console.ResetColor();

            Console.WriteLine("\t INSPECT + (item name)                   Inspect item in Inventory/Room.     ");

            ChangeTextForegroundToDarkGray();

            Console.WriteLine("\t USE  + (item name)                      Use an item from your inventory    .");

            Console.ResetColor();

        }

        public static void logo1()
        {
            Console.WriteLine(@"   ____________________________________________________________________________
  |  ____                                                                      |
  | [____] [_]   [_][_][_][_] [_][_][_][_] [_][_][_][_] [_][_][_] [_][_][_][_] |
  |                                                ___                         |
  | [_][_] [§][1][2][3][4][5][6][7][8][9][0][+]['][___] [_][_][_] [_][_][_][_] |
  | [_][_] [__][q][å][ä][p][y][f][g][c][r][l][x][@][  | [_][_][_] [_][_][_][ | |
  | [_][_] [___][a][o][e][u][i][d][h][t][n][s][j][-][_|           [_][_][_][_| |
  | [_][_] [_][<]['][,][.][k][ö][b][m][w][v][z][______]    [_]    [_][_][_][ | |
  | [_][_] [__][_][__][_____________________][__][_][_] [_][_][_] [____][_][_| |
  |____________________________________________________________________________|");
        }

        public static void ChangeTextForegroundToDarkGray()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }
    }
}
