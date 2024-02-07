namespace RconnectAPI.Models
{
    public class Hobby
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Hobby(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
