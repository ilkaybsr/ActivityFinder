using DataAccess.Concrate;

namespace DataAccess.Entities
{
    public class Activity : IEntity
    {
        public int Id { get; private set; }

        public string Date { get; private set; }

        public string Location { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Address { get; private set; }

        public string Category { get; private set; }

        public static Activity Create(string name, string description, string address, string category, string location, string date)
        {
            if (string.IsNullOrEmpty(name))
                throw new BusinessRuleException("Name cannot empty or null!");

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

        public Activity()
        {

        }
    }
}
