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
            Console.WriteLine("                          A Text adventure game                                 ");

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
    }
}
