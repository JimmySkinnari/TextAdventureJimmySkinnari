using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Media;

namespace TextAdventureJimmySkinnari
{
    //public delegate string OutputMessage (string message);
    public class Game
    {
        Player player;
        string soundPath = @"C:\Users\jimmy\source\repos\TextAdventureJimmySkinnari\TextAdventureJimmySkinnari\typing.wav";
        public Room entrance { get; set; } = new Room("Entrance");
        public Room coridor { get; set; } = new Room("Coridor");
        public Room office { get; set; } = new Room("Office");
        public Room garage { get; set; } = new Room("Garage");
        public Room factory { get; set; } = new LastRoom("Factory");

        public static List<GameObject> GameObjects = new List<GameObject>();
        public GameArt Art { get; set; } = new GameArt();
        public List<Room> Rooms { get; set; } = new List<Room>();

        public void PlayGame()
        {
            Console.SetWindowSize(147, 30);
            Console.SetBufferSize(147, 30);
            InitializePlayer();
            InitializeWorld();
            InitializeItems();
            PopulateGameObjectsList();
            FirstScene();
            Update();
            EndGame();
        }

        private void WelcomeText()
        {
            Art.GetGameLogo();
            Console.ReadLine();
            Console.SetWindowSize(120, 30);
            Console.SetBufferSize(120, 30);
            Console.Clear();
        }
        private void FirstScene()
        {
            WelcomeText();
            GameArt.PrintControls();
            Console.ReadLine();
            Console.Clear();
            WriteIntroText();
            Console.Clear();
        }

        private void WriteIntroText()
        {
            string explode = "explode";
            string introText = $"\n\t\tYou are at your job and you are just about to leave for the day,\n\t\twhen suddenly one of the gasbottles in the factory ";
            string introText2 = $".....\n\n\t\tYou wake up at the entrance of the building.\n\t\tThe main door is locked due to some technical issues from the explotion." +
               $"\n\t\tYou have to rescue you co-worker and put out the fire....";

            SoundPlayer sp = new SoundPlayer(soundPath);
            sp.Play();
            Thread.Sleep(400);
            Animate.Line(introText, ConsoleColor.DarkGray);
            Animate.Write(explode, ConsoleColor.Red);
            Animate.Write(introText2, ConsoleColor.DarkGray);
            sp.Stop();
            Console.WriteLine();
            Console.Write("\n\n\n\n\t\t\t\tPress enter to continue..");

            Console.ReadKey();
        }
        private void PopulateGameObjectsList()
        {
            foreach (Room room in Rooms)
            {
                foreach (GameObject door in room.Doors)
                {
                    GameObjects.Add(door);
                }

                foreach (GameObject item in room.RoomItems)
                {
                    GameObjects.Add(item);
                }
            }
        }
        private void PrintRoomInfo(Room room)
        {
            PrintRoomName(room);
            PrintRoomObjects(room);
        }
        private void PrintRoomObjects(Room room)
        {
            string addedRoomInfo = "";

            foreach (var door in room.Doors)
            {
                addedRoomInfo += "\t" + door.ObjectDescription + "\n";
            }

            addedRoomInfo += "\n";

            foreach (var item in room.RoomItems)
            {
                addedRoomInfo += "\t" + item.ObjectDescription + "\n";
            }
            if (player.CurrentRoom.IsVisited == false)
            {
                Console.WriteLine("");
                Animate.Line(room.Description + addedRoomInfo);
                room.IsVisited = true;
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine(room.Description + addedRoomInfo);
            }

        }
        private void PrintRoomName(Room room)
        {
            Console.WriteLine("");
            Console.Write("        ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t " + room.Name + " \t");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("");
        }

        private void Update()
        {
            bool gameIsRunning = true;

            PrintRoomInfo(player.CurrentRoom);

            Console.ForegroundColor = ConsoleColor.DarkGray;

            do
            {
                Output("");

                string[] input = Console.ReadLine().ToUpper().Split(' ').RemoveAll("THE", "UP", "ON");

                if (input[0] == "LOOK")
                {
                    PrintRoomInfo(player.CurrentRoom);
                    continue;
                }

                if (input[0] == "GET" || input[0] == "TAKE" || input[0] == "PICK")
                {
                    if (input.Length < 2)
                    {
                        Console.WriteLine("What do you want to pick up?");
                        input = Console.ReadLine().ToUpper().Split(' ');
                    }

                    player.PickUp(input);
                    continue;

                }
                else if (input[0] == "INVENTORY" || input[0] == "I")
                {

                    PrintInventory(player.Inventory);
                    continue;

                }
                else if (input[0] == "DROP")
                {
                    if (player.Inventory.Count < 1)
                    {
                        Console.WriteLine("Your inventory is empty..");
                        continue;
                    }
                    else if (input.Length < 2)
                    {
                        Console.WriteLine("What do you want to drop?");
                        input = Console.ReadLine().ToUpper().Split(' ');
                    }
                    else
                    {
                        player.Drop(input);
                        continue;
                    }
                }
                else if (input[0] == "INSPECT")
                {
                    if (input.Length < 2)
                    {
                        Console.WriteLine("What do you want to inspect?");
                        input = Console.ReadLine().ToUpper().Split(' ');
                    }

                    var objectToInspect = player.Inspect(input);

                    if (objectToInspect == null)
                    {
                        Console.WriteLine("There is no object like that..");
                    }
                    else
                    {
                        Console.WriteLine();
                        Animate.Line(objectToInspect.InspectDescription, ConsoleColor.Red);
                    }
                    continue;
                }
                else if (input[0] == "GO")
                {
                    if (input.Length < 2)
                    {
                        Animate.Line("Where do you want to go?: ", ConsoleColor.DarkGray);

                        input = Console.ReadLine().ToUpper().Split(' ');
                    }
                    else
                    {
                        input = input.Skip(1).ToArray();
                        continue;
                    }
                }
                if (input[0] == "NORTH" || input[0] == "EAST" || input[0] == "SOUTH" || input[0] == "WEST")
                {
                    if (player.CanGoTo(input))
                    {
                        player.Go(input);

                        if (player.CurrentRoom.IsVisited != true)
                        {
                            PrintRoomInfo(player.CurrentRoom);
                            player.CurrentRoom.IsVisited = true;
                        }
                        else
                        {
                            PrintRoomName(player.CurrentRoom);
                        }
                    }
                }

                else if (input[0] == "USE")
                {
                    if (player.Inventory.Count < 1)
                    {
                        Animate.Line("Your inventory is empty", ConsoleColor.DarkGray);
                    }
                    if (input.Length < 3)
                    {
                        Animate.Line("You must write: \"Use {item name} on {item/door name}\"", ConsoleColor.DarkGray);
                        continue;
                    }

                    player.Use(input);
                    continue;
                }
                else if (input[0] == "H" || input[0] == "HELP")
                {
                    GameArt.PrintControls();
                }
                else
                {
                    Animate.Line("What?", ConsoleColor.DarkGray);
                    continue;
                }

            } while (gameIsRunning);

        }

        private void InitializeWorld()
        {
            Rooms.Add(entrance);
            Rooms.Add(coridor);
            Rooms.Add(office);
            Rooms.Add(garage);
            Rooms.Add(factory);

            // Initialize doors
            entrance.Doors.Add(new Door("", "north", "There is an open door to the north that seems to lead to the coridor.", coridor));
            coridor.Doors.Add(new Door("Factory door", 1, "north", "Smoke comes out towards you from the tiny cracks around the door to the north that leads to the factory.", "This door is locked. As far as i can remember, the only person who has the key is the supervisor.", factory));
            coridor.Doors.Add(new Door("Office door", 2, "west", "The office to the west is locked.", "The supervisor allways locks his office when he leaves for the day.", office));
            coridor.Doors.Add(new Door("Garage door", "east", "There is an open door to the east that goes to the garage.", garage));
            coridor.Doors.Add(new Door("Entrance door", "south", "The door back to the entrance is to the south.", entrance));
            office.Doors.Add(new Door("", "east", "The door back to the coridor is to the east.", coridor));
            garage.Doors.Add(new Door("", "west", "The door back to the coridor is to the west.", coridor));
            factory.Doors.Add(new Door("", "south", "The door back to the coridor is to the south.", coridor));

            player.CurrentRoom = entrance;
        }
        private void InitializeItems()
        {
            GameArt ga = new GameArt();

            Item key = new Item("key", "Key made of silver on the desk.", 1, "There is a tag to the key that says: \"Factory\".");
            Item fireAxe = new Item("Axe", "Axe with a wooden shaft on the floor.", 2, "This axe looks brutal");
            Item gasMask = new Item("Gasmask", "Gasmask on the shelf.", 3, "") { CanBeCombined = true };
            Item employee = new Item("Co-worker", "Passed out co-worker lying on the floor", 3, "Co-worker whispers: \"The key to the factory is inside the office..\"") { CanBeCombined = true, CanBePickedUp = false };
            Item phone = new Item("phone", "Phone", 10, "Phone displays \"Enter pin or start emergency call\"") { CanBePickedUp = true };
            Item sprinklerSystem = new Item("sprinkler system", "Factorys sprincler system, the switch is on the wall", 11, "") { CanBePickedUp = false };
            Item bluePrints = new Item("paper", "There is some paper on the desk that looks important.", ga.GetMap());

            office.RoomItems.Add(bluePrints);
            office.RoomItems.Add(key);
            office.RoomItems.Add(gasMask);
            garage.RoomItems.Add(fireAxe);
            coridor.RoomItems.Add(employee);
            coridor.RoomItems.Add(phone);
            factory.RoomItems.Add(sprinklerSystem);
        }
        private void InitializePlayer()
        {
            Console.Write("What is your name?: ");
            string nameInput = (Console.ReadLine());

            player = new Player(nameInput);

            Console.Clear();
        }
        private void Output(string message)
        {
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("");
            Console.Write("> ");
            Console.ResetColor();
        }
        private void PrintInventory(List<Item> inventory)
        {
            if (inventory.Count < 1)
            {
                Console.WriteLine("Your inventory is empty...");
            }
            else
            {
                Console.WriteLine("\n  ---- Inventory ---- \n\n");

                for (int i = 0; i < inventory.Count; i++)
                {
                    Console.WriteLine("      " + (i + 1) + " - " + inventory[i].Name);
                }
            }
        }
        private void EndGame()
        {
            Console.WriteLine("Game Over");
            Console.WriteLine("Do you want to play another game? (Y/N)");
            string input = (Console.ReadLine());

            if (input.ToLower() == "y")
            {
                PlayGame();
            }

            else
            {
                //close app
            }
        }

    }
}
