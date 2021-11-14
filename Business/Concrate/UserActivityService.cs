using Business.Abstracts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class UserActivityService : IUserActivityService
    {
        readonly UserManager<User> _userManager;
        private readonly ILogger<UserActivityService> _logger;

        public UserActivityService(UserManager<User> userManager,
            ILogger<UserActivityService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> Bookmark(Guid userId, int activityId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                throw new Exception("User not found!");



        }

        public async Task<bool> RemoveBookmark(Guid userId, int activityId)
        {

        }

        public async Task Bookmarks(Guid userId)
        {

        }
    }
}
