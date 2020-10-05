namespace TextAdventureJimmySkinnari
{
    public class Door : GameObject
    {
        public bool IsLocked { get; set; } = false;
        public string Name { get; set; }
        public string Location { get; set; }
        public int Id { get; set; }
        public Room RoomBehindDoor { get; set; }


        public Door(string name, string location, string doorDescription, Room roomBehindDoor)
        {

            Name = name;
            ObjectDescription = doorDescription;
            Location = location;
            RoomBehindDoor = roomBehindDoor;
            InspectDescription = $"There is nothing special about the { name }";
        }

        public Door(string name, bool isLocked, int id, string location, string doorDescription, string doorInspectDescription, Room roomBehindDoor)
        {
            Name = name;
            IsLocked = isLocked;
            Id = id;
            Location = location;
            ObjectDescription = doorDescription;
            InspectDescription = doorInspectDescription;
            RoomBehindDoor = roomBehindDoor;
        }
    }
}
