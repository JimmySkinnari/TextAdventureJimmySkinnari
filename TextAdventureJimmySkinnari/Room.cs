using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace TextAdventureJimmySkinnari
{
    public class Room
    {
        public string Name { get; set; }
        public List<Item> RoomItems { get; set; }
        public List<Door> Doors { get; set; }
        public string Description { get; set; } = "";
        public bool IsVisited { get; set; } = false;


        public Room()
        {
            Doors = new List<Door>();
            RoomItems = new List<Item>();
        }
        public Room(string name)
        {
            Name = name;

            RoomItems = new List<Item>();
            Doors = new List<Door>();
        }

        public List<GameObject> GetRoomObjects()
        {
            List<GameObject> roomObjects = new List<GameObject>();

            foreach (var item in Doors)
            {
                roomObjects.Add(item);
            }

            foreach (var item in RoomItems)
            {
                roomObjects.Add(item);
            }

            return roomObjects;
        }
    }
}