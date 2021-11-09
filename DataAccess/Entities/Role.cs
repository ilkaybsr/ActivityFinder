using Microsoft.AspNetCore.Identity;
using System;

namespace DataAccess.Entities
{
    public class Role : IdentityRole<Guid>, IEntity
    {
    }
}
