using System;

namespace DataAccess.Entities
{
    public class UserActivity
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public int ActivityId { get; set; }

        public static UserActivity Create(Guid userId, int activityId)
        {
            return new UserActivity
            {
                UserId = userId,
                ActivityId = activityId
            };
        }
    }
}
