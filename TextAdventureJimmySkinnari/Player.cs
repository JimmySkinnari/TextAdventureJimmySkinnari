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


        public void PickUpItem(string[] input)
        {
            string result = "";

            if (input.Length < 1)
            {
                Console.WriteLine("What do you want to pick up?");
                input = Console.ReadLine().ToUpper().Split(' ');
                result = string.Join(" ", input);

            }

            if (CurrentRoom.RoomItems != null)
            {
                foreach (Item item in CurrentRoom.RoomItems)
                {
                    if (input.Contains(item.Name.ToUpper()) && item.CanBePickedUp)
                    {
                        Console.WriteLine("Taken.");
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
                Console.WriteLine("There is no " + result);
            }

        }

        public void DropItem(string[] input)
        {

            if (Inventory.Count < 1)
            {
                Console.WriteLine("Your inventory is empty");
                return;
            }

            if (input.Length < 1)
            {
                Console.WriteLine("What do you want to drop?");
                input = Console.ReadLine().ToUpper().Split(' ');
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
                                        Console.WriteLine("Unlocked.");

                                    }
                                    else if (item.Name == "Axe") // Special unlock message when player manage to get into the office
                                    {
                                        Console.WriteLine("You swing the axe on the door like Jack Nicholson in The Shining untill you break up the door completely");
                                        Console.WriteLine("The office door is now on the floor, in pieces and you can go inside.");
                                    }

                                    return;
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

        private void PrintCombineItemsMessage(int id)
        {
            if (id == 3)
            {
                Console.WriteLine("You put the gasmask on to the co-worker and his life might be saved!..");
            }
        }

        internal GameObject Inspect(string[] input)
        {
            foreach (Item item in Inventory)
            {
                if (input.Contains(item.Name.ToUpper()))
                {
                    //Console.WriteLine("");
                    //Console.WriteLine(item.InspectDescription);
                    return item;
                }
            }

            foreach (var item in CurrentRoom.RoomItems)
            {
                if (input.Contains(item.Name.ToUpper()))
                {
                    //Console.WriteLine("");
                    //Console.WriteLine(item.InspectDescription);
                    return item;
                }
            }

            foreach (var door in CurrentRoom.Doors)
            {
                if (input.Contains(door.Name.ToUpper()))
                {
                    //Console.WriteLine("");
                    //Console.WriteLine(door.InspectDescription);
                    return door;
                }
            }

            
            return new GameObject{inspec;
        }

        public void Go(string[] direction)
        {
            bool flag = true;

            foreach (var door in CurrentRoom.Doors)
            {
                if (direction[0] == door.Location.ToUpper())
                {
                    flag = false;

                    if (door.IsLocked == true)
                    {
                        Console.WriteLine(door.ObjectDescription);
                        continue;
                    }
                    else
                    {
                        CurrentRoom = door.RoomBehindDoor;
                        Console.WriteLine();
                        CurrentRoom.PrintRoomName();
                        Console.WriteLine();

                        if (CurrentRoom.IsVisited == false)
                        {
                            CurrentRoom.PrintRoomDescription();
                        }

                        CurrentRoom.IsVisited = true;
                        continue;
                    }
                }
            }
            if (flag)
            {
                Console.WriteLine("You can't go that way..");
            }
        }

        public void UnlockDoor(Item key, Door door)
        {
            if (key.Id == door.Id)
            {
                door.IsLocked = false;
                Console.WriteLine(key.Name + " matched the lock. The " + door.Name + " is now unlocked.");
                this.CurrentRoom = door.RoomBehindDoor;
            }

            else
            {
                Console.WriteLine(key.Name + " can't open this door..");
            }
        }

        public void PrintInventory()
        {
            if (Inventory.Count < 1)
            {
                Console.WriteLine("Your inventory is empty..");
            }

            else
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine("Item nr " + (i + 1) + ": " + Inventory[i].Name);

                }
            }
        }
    }
}
