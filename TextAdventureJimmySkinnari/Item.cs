namespace TextAdventureJimmySkinnari
{
    public class Item : GameObject
    {
        public bool CanBePickedUp { get; set; } = true;

        public Item(string name, string description, string inspectDescription)
        {
            Name = name;
            ObjectDescription = description;
            InspectDescription = inspectDescription;
        }

        public Item(string name, string description, int id, string inspectDescription)
        {
            Name = name;
            ObjectDescription = description;
            Id = id;
            InspectDescription = inspectDescription;
        }
    }
}
