using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventureJimmySkinnari
{
    public class Player
    {

        public string Name { get; set; }
        public bool IsAlive { get; set; } = true;
        public Room CurrentRoom { get; set; }
        public List<Item> Gear { get; set; } = new List<Item>();
        public List<Item> Inventory { get; set; } = new List<Item>();


        public Player(string name)
        {
            Name = name;
        }

        public void PickUp(string[] input)
        {
            if (CurrentRoom.RoomItems != null)
            {

                foreach (Item item in CurrentRoom.RoomItems)
                {
                    if (!input.Contains(item.Name.ToUpper()) || item.CanBePickedUp == false)
                    {
                        Console.WriteLine("Can't pick that up..");
                        continue;
                    }

                    Console.WriteLine($"\n{item.Name} taken.");
                    Inventory.Add(item);
                    CurrentRoom.RoomItems.Remove(item);
                    break;
                }

                return;
            }
            else
            {
                Console.WriteLine("There is no " + input[0] + " to pick up.");
            }

            Console.WriteLine("What?");

        }

        public void Drop(string[] input)
        {
            string result = string.Join(" ", input);

            foreach (var item in Inventory)
            {
                if (!result.Contains(item.Name.ToUpper()))
                {
                    continue;
                }

                CurrentRoom.RoomItems.Add(item);
                Inventory.Remove(item);

                Console.WriteLine(item.Name + " has been dropped.");

                return;
            }

            Console.WriteLine("You don´t have that in your inventory..");
        }

        public void Use(string[] input)
        {
            foreach (Item item in Inventory)
            {
                int itemIndex = Array.FindIndex(input, x => x.Contains(item.Name.ToUpper()));

                if (!input.Contains(item.Name.ToUpper()))
                {
                    continue;
                }

                //  var targetItemType == GetTargetItemType();
                //  if (targetItemType == null)
                // {
                //   Console.WriteLine("Cant Use the item on that");
                // }
                //
                // else 
                // {
                //    
                // }
                //

                if (TargetIsDoor(item, input, itemIndex))
                {
                    break;
                }
                else if (TargetIsRoomItem(item, input, itemIndex))
                {
                    break;
                }
                else if (TargetIsInventoryItem(item, input, itemIndex))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Can't use the item on that.");
                }
            }

            return;
        }

        private bool TargetIsDoor(Item item, string[] input, int itemIndex)
        {
            foreach (Door door in CurrentRoom.Doors)
            {

                var doorName = door.Name.Split(' ');

                if (!input.Contains(doorName[0].ToUpper()))
                {
                    continue;
                }

                int itemToBeUsedIndex = Array.FindIndex(input, x => x.Contains(doorName[0].ToUpper()));

                if (itemIndex > itemToBeUsedIndex) // check order of input, if user want to i.e use door on key.
                {
                    return false;
                }

                if (door.Id != item.Id)
                {
                    Console.WriteLine($"Can't use {item.Name} on {door.Name}.");
                    return false;

                }

                door.IsLocked = false;

                if (item.Name != "Axe") // Standard unlock message
                {
                    Inventory.Remove(item);
                    Console.WriteLine($"{door.Name} unlocked.");

                }
                else if (item.Name == "Axe") // Special unlock message when player manage to get into the office
                {
                    Console.WriteLine("You swing the axe on the door like Jack Nicholson in The Shining until you break up the door completely.");
                    Console.WriteLine($"{door.Name} unlocked.");
                }

                return true;
            }

            return false;
        }

        private bool TargetIsRoomItem(Item item, string[] input, int itemIndex)
        {
            foreach (var itemInRoom in CurrentRoom.RoomItems)
            {
                var itemInRoomNameArray = itemInRoom.Name.Split(' ');

                if (!input.Contains(itemInRoomNameArray[0].ToUpper()))
                {
                    continue;
                }

                int itemToBeUsedIndex = Array.FindIndex(input, x => x.Contains(itemInRoomNameArray[0].ToUpper()));

                if (itemIndex < itemToBeUsedIndex)
                {
                    if (itemInRoom.Id == item.Id && itemInRoom.CanBeCombined == true) // if items can be used on eachother.
                    {
                        PrintCombineItemsMessage(item.Id);

                        return true;

                    }
                }
            }

            return false;
        }

        private bool TargetIsInventoryItem(Item item, string[] input, int itemIndex)
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

                int itemToBeUsedIndex = Array.FindIndex(input, x => x.Contains(itemInInventoryNameArr[0].ToUpper()));

                if (itemIndex < itemToBeUsedIndex)
                {
                    if (itemInInventory.Id != item.Id) // if items can be used on eachother.
                    {
                        break;
                    }
                    if (item.Name == "Gasmask")
                    {
                        Inventory.Remove(item);
                        PrintCombineItemsMessage(item.Id);
                        return true;
                    }
                    else
                    {
                        Inventory.Remove(item);
                        Inventory.Remove(itemInInventory);
                        PrintCombineItemsMessage(item.Id);
                        return true;
                    }

                }
            }

            return false;
        }


        internal GameObject Inspect(string[] input)

        {
            //Print item inspectDescription

            foreach (Item item in Inventory)
            {
                if (input.Contains(item.Name.ToUpper()))
                {
                    return item;
                }
            }

            foreach (Item item in CurrentRoom.RoomItems)
            {
                if (input.Contains(item.Name.ToUpper()))
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
                    Console.WriteLine(door.ObjectDescription);
                    return false;
                }
                else
                {
                    return true;
                }

            }

            Console.WriteLine("You can't go that way..");
            return false;
        }

        public void Go(string[] direction)
        {
            foreach (var door in CurrentRoom.Doors)
            {
                if (direction[0] == door.Location.ToUpper())
                {
                    CurrentRoom = door.RoomBehindDoor;
                }
            }

        }

        private void PrintCombineItemsMessage(int id)
        {
            if (id == 3)
            {
                Console.WriteLine("You put the gasmask on to the co-worker and his life might be saved!");
            }
        }



        public List<Item> GetInventory()
        {
            return Inventory;
        }
    }
}

