namespace RconnectAPI.Models
{
    public class Meeting
    {
        public Meeting(string? id, List<string> users, string host, DateTime date, List<string> billedusers)
        {
            Id = id;
            Users = users;
            Host = host;
            Date = date;
            Billedusers = billedusers;
        }

        public string? Id { get; set; }
        public List<string> Users { get; set; } = new List<string>();
        public string Host { get; set; }
        public DateTime Date { get; set; }
        public List<string> Billedusers { get; set; } = new List<string>();
    }
}
