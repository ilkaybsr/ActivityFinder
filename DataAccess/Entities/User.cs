using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class User : IdentityUser<Guid>, IEntity
    {
        public ICollection<UserActivity> Bookmarks { get; private set; }

        public User()
        {
            Bookmarks ??= new List<UserActivity>();
        }
    }
}
