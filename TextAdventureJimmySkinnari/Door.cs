namespace TextAdventureJimmySkinnari
{
    public class Door : GameObject
    {
        public bool IsLocked { get; set; } = false;
        public string Location { get; set; }
        public Room RoomBehindDoor { get; set; }

        public Door(string name, string location, string doorDescription, Room roomBehindDoor)
        {

            Name = name;
            ObjectDescription = doorDescription;
            Location = location;
            RoomBehindDoor = roomBehindDoor;
            InspectDescription = $"There is nothing special about the { name }";
        }

        public Door(string name, int id, string location, string doorDescription, string doorInspectDescription, Room roomBehindDoor)
        {
            Name = name;
            Id = id;
            Location = location;
            ObjectDescription = doorDescription;
            InspectDescription = doorInspectDescription;
            RoomBehindDoor = roomBehindDoor;

            IsLocked = true;
        }
    }
}
