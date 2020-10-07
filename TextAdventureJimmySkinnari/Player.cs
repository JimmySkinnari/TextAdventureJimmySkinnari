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

        public string Message { get; set; } = "";


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
                    if (input.Contains(item.Name.ToUpper()) && item.CanBePickedUp)
                    {
                        Console.WriteLine($"\n{item.Name} taken.");
                        Inventory.Add(item);
                        CurrentRoom.RoomItems.Remove(item);

                        return;
                    }

                    else if (input.Contains(item.Name.ToUpper()) && item.CanBePickedUp == false)
                    {
                        Console.WriteLine("Can't pick that up..");
                        return;
                    }
                }

                Console.WriteLine("What?");
            }
            else
            {
                Console.WriteLine("There is no " + input[0] + " to pick up.");
            }

        }

        public void Drop(string[] input)
        {
            //Add item to current room
            //Remove item from bag

            if (Inventory.Count < 1)
            {
                Console.WriteLine("Your inventory is empty");
                return;
            }

            string result = string.Join(" ", input);

            foreach (var item in Inventory)
            {
                if (result.Contains(item.Name.ToUpper()))
                {
                    CurrentRoom.RoomItems.Add(item);
                    Inventory.Remove(item);

                    Console.WriteLine(item.Name + " has been dropped.");

                    return;
                }
            }

            Console.WriteLine("You don´t have that in your inventory..");
        }

        public void Use(string[] input)
        {
            // use item on item/door

            foreach (Item item in Inventory)
            {
                int itemIndex = Array.FindIndex(input, x => x.Contains(item.Name.ToUpper()));

                if (input.Contains(item.Name.ToUpper()))
                {
                    foreach (Door door in CurrentRoom.Doors)
                    {
                        var doorName = door.Name.Split(' ');

                        if (input.Contains(doorName[0].ToUpper()))
                        {
                            int itemToBeUsedIndex = Array.FindIndex(input, x => x.Contains(doorName[0].ToUpper()));

                            if (itemIndex < itemToBeUsedIndex) // check order of input, if user want to i.e use door on key.
                            {
                                if (door.Id == item.Id)
                                {
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

                                    return;
                                }
                                else
                                {
                                    Console.WriteLine($"Cant use {item.Name} on {door.Name}");
                                }
                            }

                            return;
                        }
                    } // Check if user wants to use item on door.

                    foreach (var itemInRoom in CurrentRoom.RoomItems)
                    {
                        var itemInRoomNameArray = itemInRoom.Name.Split(' ');

                        if (input.Contains(itemInRoomNameArray[0].ToUpper()))
                        {
                            int itemToBeUsedIndex = Array.FindIndex(input, x => x.Contains(itemInRoomNameArray[0].ToUpper()));

                            if (itemIndex < itemToBeUsedIndex)
                            {
                                if (itemInRoom.Id == item.Id && itemInRoom.CanBeCombined == true) // if items can be used on eachother.
                                {
                                    PrintCombineItemsMessage(item.Id);
                                    return;
                                }
                            }

                        }
                    } // Check if user wants to use item on item in room.

                    foreach (var itemInInventory in Inventory)
                    {

                        if (itemInInventory.Name == item.Name) // Loop skip its own item.
                        {
                            continue;
                        }

                        var itemInInventoryNameArr = itemInInventory.Name.Split(' ');

                        if (input.Contains(itemInInventoryNameArr[0].ToUpper()))
                        {
                            int itemToBeUsedIndex = Array.FindIndex(input, x => x.Contains(itemInInventoryNameArr[0].ToUpper()));

                            if (itemIndex < itemToBeUsedIndex)
                            {
                                if (itemInInventory.Id == item.Id && item.CanBeCombined == true) // if items can be used on eachother.
                                {
                                    if (item.Name == "Gasmask")
                                    {
                                        Inventory.Remove(item);
                                        PrintCombineItemsMessage(item.Id);
                                        return;
                                    }
                                    else
                                    {
                                        Inventory.Remove(item);
                                        Inventory.Remove(itemInInventory);
                                        PrintCombineItemsMessage(item.Id);
                                        return;
                                    }
                                }
                            }
                        }
                    }  // Check if user wants to use item on other item in inventory.

                    Console.WriteLine("Can't use the item on that.");
                    return;
                }
            }

            Console.WriteLine("You dont have that in your inventory.");
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
                if (direction[0] == door.Location.ToUpper())
                {
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
