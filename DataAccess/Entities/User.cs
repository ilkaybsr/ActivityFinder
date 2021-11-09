using Microsoft.AspNetCore.Identity;
using System;

namespace DataAccess.Entities
{
    public class User : IdentityUser<Guid>, IEntity
    {
    }
}
