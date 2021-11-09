using DataAccess.Concrate;
using System;

namespace DataAccess.Entities
{
    public class Error : IEntity
    {
        public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public string Message { get; private set; }

        public Error()
        {

        }

        public static Error Create(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new BusinessRuleException("Message cannot be empty or null!");

            return new Error
            {
                Id = Guid.NewGuid(),
                Message = message,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
