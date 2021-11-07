namespace DataAccess.Entities
{
    public class Activity : IEntity
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string Category { get; set; }

        public static Activity Create(string name, string description, string address, string category, string location, string date)
        {
            return new Activity
            {
                Date = date,
                Location = location,
                Name = name,
                Description = description,
                Address = address,
                Category = category
            };
        }
    }
}
