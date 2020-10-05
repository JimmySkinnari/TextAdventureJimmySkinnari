using System;
using System.Collections.Generic;

namespace TextAdventureJimmySkinnari
{
    public class Room
    {
        public string Name { get; set; }
        public List<Item> RoomItems { get; set; }
        public List<Door> Doors { get; set; }
        public string Description { get; set; }
        public bool IsVisited { get; set; } = false;


        public Room()
        {

            Doors = new List<Door>();
            RoomItems = new List<Item>();

        }
        public Room(string name, string description)
        {

            Name = name;
            Description = description;

            RoomItems = new List<Item>();
            Doors = new List<Door>();

        }

        public void PrintRoomItems()
        {
            foreach (var item in RoomItems)
            {
                Console.WriteLine(item.ObjectDescription);
            }
        }

        public void GetRoomDescription()
        {
            string addedRoomInfo = "";

            foreach (var door in Doors)
            {
                addedRoomInfo += "\t" + door.ObjectDescription + "\n";
            }

            addedRoomInfo += "\n";

            foreach (var item in RoomItems)
            {
                addedRoomInfo += "\t" + item.ObjectDescription + "\n";
            }

            Console.WriteLine(Description + addedRoomInfo);
        }

        public List<Door> GetDoors()
        {
            return Doors;
        }

        public void GetRoomName()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n" + "\t" + this.Name + "  \t\n");
            Console.ResetColor();
        }
    }
}
