using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventureJimmySkinnari
{
    //public delegate string OutputMessage (string message);
    public class Game
    {
        Player player;

        Room entrance;
        Room coridor;
        Room office;
        Room garage;
        Room factory;

        public GameArt Art { get; set; } = new GameArt();
        public List<Room> Rooms { get; set; } = new List<Room>();


        public void PlayGame()
        {
            WelcomeText();
            InitializePlayer();
            InitializeWorld();
            InitializeItems();
            PlayFirstScene();
            Update();
            EndGame();
        }

        private void WelcomeText()
        {
            Art.GetGameLogo();

            Console.ReadLine();
            Console.Clear();
        }
        private void PlayFirstScene()
        {
            player.CurrentRoom.GetRoomName();
            Console.WriteLine();
            player.CurrentRoom.GetRoomDescription();
        }


        private void Update()
        {
            bool gameIsRunning = true;

            do
            {
                Output("");

                string[] input = Console.ReadLine().ToUpper().Split(' ').RemoveAll("THE", "UP");


                if (input[0] == "LOOK")
                {
                    player.CurrentRoom.GetRoomName();
                    player.CurrentRoom.GetRoomDescription();
                    continue;
                }

                if (input[0] == "GET" || input[0] == "TAKE" || input[0] == "PICK")
                {

                    player.PickUp(input.Skip(1).ToArray());

                }
                else if (input[0] == "INVENTORY" || input[0] == "I")
                {

                    PrintInventory(player.GetInventory());

                }
                else if (input[0] == "DROP")
                {

                    if (player.Inventory.Count < 1)
                    {
                        Output("Your inventory is empty");
                        return;
                    }

                    if (input.Length < 1)
                    {
                        Output("What do you want to drop?");
                        input = Console.ReadLine().ToUpper().Split(' ');
                    }

                    player.Drop(input.Skip(1).ToArray());

                }
                else if (input[0] == "INSPECT")
                {
                    if (input.Length < 2)
                    {
                        Output("What do you want to inspect?");
                        input = Console.ReadLine().ToUpper().Split(' ');
                    }

                    if (player.Inspect(input) == null)
                    {                
                        Output("There is no object like that..");
                    }
                    else
                    {
                        Output(player.Inspect(input).InspectDescription);
                       
                    }
                }
                else if (input[0] == "GO")
                {
                    player.Go(input.Skip(1).ToArray());
                }
                else if (input[0] == "NORTH" || input[0] == "EAST" || input[0] == "SOUTH" || input[0] == "WEST")
                {
                    player.Go(input);
                }
                else if (input[0] == "USE")
                {
                    if (input.Length < 3)
                    {   
                        Output("What do you want to use??");
                        input = Console.ReadLine().ToUpper().Split(' ');
                    }

                    player.Use(input.Skip(1).ToArray());
                }
                else if (input[0] == "H" || input[0] == "HELP")
                {
                    PrintHelp();
                }
                else
                {
                    Output("What?");
                    continue;
                }

            } while (gameIsRunning);
        }


        private void InitializeWorld()
        {
            bool locked = true;

            // Initialize Rooms
            entrance = new Room("Entrance", "");
            coridor = new Room("Coridor", "");
            office = new Room("Office", "");
            garage = new Room("Garage", "");
            factory = new LastRoom("Factory", "");

            Rooms.Add(entrance);
            entrance.IsVisited = true;
            Rooms.Add(coridor);
            Rooms.Add(office);
            Rooms.Add(garage);
            Rooms.Add(factory);

            // Initialize doors
            Door hallDoor1 = new Door("", "north", "There is an open door to the north that seems to lead to the coridor.", coridor);
            Door coridorDoorNorth = new Door("Factory door", locked, 1, "north", "Smoke comes out towards you from the tiny cracks around the door to the north that leads to the factory.", "This door is locked. As far as i can remember, the only person who has the key is the supervisor.", factory);
            Door coridorDoorWest = new Door("Office door", locked, 2, "west", "The office to the west is locked.", "The supervisor allways locks his office when he leaves for the day.", office);
            Door coridorDoorEast = new Door("Garage door", "east", "There is an open door to the east that goes to the garage.", garage);
            Door coridorDoorSouth = new Door("Entrance door", "south", "The door back to the entrance is to the south.", entrance);
            Door officeDoor = new Door("", "east", "The door back to the coridor is to the east.", coridor);
            Door garageDoor = new Door("", "west", "The door back to the coridor is to the west.", coridor);
            Door coridorDoor = new Door("", "south", "The door back to the coridor is to the south.", coridor);

            factory.Doors.Add(coridorDoor);
            garage.Doors.Add(garageDoor);
            entrance.Doors.Add(hallDoor1);
            coridor.Doors.Add(coridorDoorSouth);
            coridor.Doors.Add(coridorDoorEast);
            coridor.Doors.Add(coridorDoorNorth);
            coridor.Doors.Add(coridorDoorWest);
            office.Doors.Add(officeDoor);

            player.CurrentRoom = entrance;
        }
        private void InitializeItems()
        {
            GameArt ga = new GameArt();

            Item key = new Item("key", "Key made of silver on the desk.", 1, "There is a tag to the key that says: \"Factory\".", true, false);
            Item fireAxe = new Item("Axe", "Axe with a wooden shaft on the floor.", 2, "This axe looks brutal", true, false);
            Item gasMask = new Item("Gasmask", "Emergancy mask on the shelf.", 3, "", true, false);
            Item employee = new Item("Co-worker", "Passed out co-worker lying on the floor", 3, "Co-worker whispers: \"The key to the factory is inside the office..\"", false, true);
            Item phone = new Item("phone", "Apple Iphone 11", 10, "Phone displays \"Enter pin or start emergency call\"", true, false);
            Item sprinklerSystem = new Item("sprinkler system", "Factorys sprincler system, the switch is on the wall", 11, "", false, true);
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
            Console.Write("> ");
            Console.ResetColor();
        }
        private void PrintInventory(List<Item> inventory)
        {
            if (inventory.Count < 1)
            {
                Output("Your inventory is empty...");
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
        private void PrintHelp()
        {

            Console.WriteLine("\nCommand                                 Description.");
            Console.WriteLine("\nH                                       Displays Help menu.");
            Console.WriteLine("LOOK                                    Look around the room.");
            Console.WriteLine("GET/TAKE/PICK/PICK UP                   Pick up something.");
            Console.WriteLine("INVENTORY/I                             Check your inventory.");
            Console.WriteLine("DROP + (item name)                      Drop an item from your inventory.");
            Console.WriteLine("GO + north/east/west/south              Try to go a certain direction.");
            Console.WriteLine("INSPECT + (item name)                   Inspect item in Inventory/Room.");
            Console.WriteLine("USE  + (item name)                      Use an item from your inventory.");

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
