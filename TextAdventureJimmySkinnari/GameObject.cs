namespace TextAdventureJimmySkinnari
{
    public abstract class GameObject
    {
        public string Name { get; set; }
        public string ObjectDescription { get; set; }
        public string InspectDescription { get; set; }
        public int Id { get; set; }
        public bool CanBeCombined { get; set; } = false;
    }
}
