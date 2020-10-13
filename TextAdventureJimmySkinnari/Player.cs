using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TextAdventureJimmySkinnari;

public delegate GameObject GetItem();

namespace TextAdventureJimmySkinnari
{
    public class Player
    {
        public string Name { get; set; }
        public Room CurrentRoom { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();
        public bool HasSavedCoWorker { get; set; } = false;

        public Player(string name)
        {
            Name = name;
        }

        public void PickUp(string[] input)
        {
            foreach (Item item in CurrentRoom.RoomItems)
            {
                if (!input.Contains(item.Name.ToUpper()))
                {
                    continue;
                }
                if (item.CanBePickedUp == false)
                {
                    Animate.Line($"Can't pick up {item.Name}..", ConsoleColor.White);
                    return;
                }

                Animate.Line($"\n{item.Name} taken.", ConsoleColor.DarkGreen);
                Inventory.Add(item);
                CurrentRoom.RoomItems.Remove(item);
                return;
            }
            Animate.Line($"There is no item like that to pick up...", ConsoleColor.White);
        }
        public void Drop(string[] input)
        {
            foreach (var item in Inventory)
            {
                if (!input.Contains(item.Name.ToUpper()))
                {
                    continue;
                }

                CurrentRoom.RoomItems.Add(item);
                Inventory.Remove(item);

                Animate.Line($"{item.Name} has been dropped.", ConsoleColor.White);
                return;
            }
            Animate.Line($"You don´t have that in your inventory..", ConsoleColor.White);
        }
        public void Use(string[] input)
        {
            foreach (Item item in Inventory)
            {
                if (!input.Contains(item.Name.ToUpper()))
                {
                    continue;
                }
                int itemIndex = Array.FindIndex(input, x => x.Contains(item.Name.ToUpper()));

                if (ItemToUseOnIsDoor(item, input, itemIndex))
                {
                    return;
                }
                else if (ItemToUseOnIsRoomItem(item, input, itemIndex))
                {
                    return;
                }
                else if (ItemToUseOnIsInventoryItem(item, input, itemIndex))
                {
                    return;
                }
            }

            if (input[input.Length - 1] == "DOOR")
            {

                Animate.Line("You must tell me on what door..", ConsoleColor.White);
                return;
            }

            Animate.Line("Can't use that...", ConsoleColor.White);
        }
        public bool ItemToUseOnIsDoor(Item item, string[] input, int itemInputIndex)
        {
            foreach (Door door in CurrentRoom.Doors)
            {
                var doorName = door.Name.Split(' ');

                if (!input.Contains(doorName[0].ToUpper()))
                {
                    continue;
                }

                int itemToBeUsedIndex = Array.FindIndex(input, x => x.Contains(doorName[0].ToUpper()));

                if (itemInputIndex > itemToBeUsedIndex) // check order of input, if user want to i.e use door on key && if id matches.
                {
                    Animate.Line($"Can't use {door.Name} on {item.Name}.", ConsoleColor.White);
                    return true;
                }

                if (door.Id != item.Id) // check order of input, if user want to i.e use door on key && if id matches.
                {
                    Animate.Line($"Can't use {item.Name} on {door.Name}.", ConsoleColor.White);
                    return true;
                }

                door.IsLocked = false;
                door.ObjectDescription = $"The {door.Name} to the {door.Location} is unlocked.";
                door.InspectDescription = $"There is nothing special about the {door.Name}";

                if (item.Name != "Axe") // Standard unlock message
                {
                    Inventory.Remove(item);

                    Animate.Line($"{door.Name} unlocked.", ConsoleColor.DarkGreen);
                    return true;
                }
                else if (item.Name == "Axe") // Special unlock message when player manage to get into the office
                {
                    Animate.Line($"You swing the axe on the door like Jack Nicholson in The Shining until you break up the door completely.", ConsoleColor.DarkGreen);
                    Console.WriteLine("");
                    Animate.Line($"{door.Name} unlocked.", ConsoleColor.DarkGreen);
                    return true;
                }
            }
            return false; ;
        }
        private bool ItemToUseOnIsRoomItem(Item item, string[] input, int itemIndex)
        {
            foreach (var itemInRoom in CurrentRoom.RoomItems)
            {
                var itemInRoomNameArray = itemInRoom.Name.Split(' ');

                if (!input.Contains(itemInRoomNameArray[0].ToUpper()))
                {
                    continue;
                }
                int itemToBeUsedIndex = Array.FindIndex(input, x => x.Contains(itemInRoomNameArray[0].ToUpper()));

                if (itemIndex > itemToBeUsedIndex)
                {
                    Animate.Line($"Can´t use {itemInRoom.Name} on {item.Name}..", ConsoleColor.White);
                    return true;
                }

                if (itemInRoom.Id != item.Id)
                {
                    Animate.Line($"Can´t use {item.Name} on {itemInRoom.Name}..", ConsoleColor.White);
                    return true;
                }

                if (itemInRoom.CanBeCombined == true)
                {
                    if (item.Id == 3)
                    {
                        Animate.Line($"You put the gasmask on to the co-worker and his life might be saved!", ConsoleColor.DarkGreen);
                        HasSavedCoWorker = true;
                        Inventory.Remove(item);
                        return true;
                    }
                }

                return true;
            }

            return false;
        }
        private bool ItemToUseOnIsInventoryItem(Item item, string[] input, int itemIndex)
        {
            foreach (var itemInInventory in Inventory)
            {
                if (itemInInventory.Name == item.Name) // Loop skip its own item.
                {
                    continue;
                }

                var itemInInventoryNameArr = itemInInventory.Name.Split(' ');

                if (!input.Contains(itemInInventoryNameArr[0].ToUpper()))
                {
                    continue;
                }

                int itemToBeUsedOnIndex = Array.FindIndex(input, x => x.Contains(itemInInventoryNameArr[0].ToUpper()));

                if (itemIndex > itemToBeUsedOnIndex)
                {
                    Animate.Line($"Can´t use {itemInInventory.Name} on {item.Name}..", ConsoleColor.White);
                    return true;
                }

                else if (itemInInventory.Id != item.Id) // if items can be used on eachother.
                {
                    Animate.Line($"Can´t use {item.Name} on {itemInInventory.Name}..", ConsoleColor.White);
                    return true;
                }

                Inventory.Remove(item);
                Inventory.Remove(itemInInventory);
                return true;
            }

            return false;
        }

        internal GameObject Inspect(string[] input)
        {
            var list = GetVisibleObjects();


            foreach (var item in list)
            {
                var itemNameArr = item.Name.ToUpper().Split(' ');

                if (input.Contains(itemNameArr[0]))
                {
                    return item;
                }
            }

            foreach (var item in Inventory)
            {
                var itemNameArr = item.Name.ToUpper().Split(' ');

                if (input.Contains(itemNameArr[0]))
                {
                    return item;
                }
            }
            return null;
        }
        public bool CanGoTo(string[] direction)
        {
            foreach (var door in CurrentRoom.Doors)
            {
                if (!direction.Contains(door.Location.ToUpper()))
                {
                    continue;
                }
                if (door.IsLocked == true)
                {
                    Animate.Line(door.ObjectDescription, ConsoleColor.White);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            Animate.Line("You can't go that way..", ConsoleColor.White);
            return false;
        }
        public void Go(string[] direction)
        {
            var doorToPass = CurrentRoom.Doors.Find(x => direction.Contains(x.Location.ToUpper()));
            CurrentRoom = doorToPass.RoomBehindDoor;
        }

        public List<GameObject> GetVisibleObjects()
        {
            List<GameObject> visibleObjects = new List<GameObject>();


            foreach (GameObject gameObject in CurrentRoom.RoomItems)
            {
                visibleObjects.Add(gameObject);
            }

            foreach (GameObject gameObject in CurrentRoom.Doors)
            {
                visibleObjects.Add(gameObject);
            }

            return visibleObjects;
        }
    }
}