using System;

namespace DataAccess.Entities
{
    public class UserActivity
    {
        public int Id { get; private set; }

        public Guid UserId { get; private set; }

        public int ActivityId { get; private set; }

        public Activity Activity { get; private set; }

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
