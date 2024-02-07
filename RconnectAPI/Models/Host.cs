namespace RconnectAPI.Models
{
    public class Host
    {
        public Host(string name, string description, string adress, string city, string phone)
        {
            Name = name;
            Description = description;
            Adress = adress;
            City = city;
            Phone = phone;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}
