using Business.Abstracts;
using Business.DTO;
using DataAccess.Abstracts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class UserActivityService : IUserActivityService
    {
        readonly UserManager<User> _userManager;
        private readonly ILogger<UserActivityService> _logger;
        private readonly IGenericRepository<UserActivity> _userActiviyRepository;
        private readonly IGenericRepository<Activity> _activityRepository;

        public UserActivityService(UserManager<User> userManager,
             IGenericRepository<UserActivity> userActivityRepository,
             IGenericRepository<Activity> activityRepository,
            ILogger<UserActivityService> logger)
        {
            _userManager = userManager;
            _activityRepository = activityRepository;
            _logger = logger;
            _userActiviyRepository = userActivityRepository;
        }

        public async Task<bool> Bookmark(Guid userId, int activityId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                throw new Exception("User not found!");

            var activity = await _activityRepository.GetByIdAsync(activityId);
            if (activity is null)
                throw new Exception("Activity not found!");

            var found = _userActiviyRepository.Find(x => x.ActivityId == activityId && x.UserId == userId);
            if (found.Count() > 0)
                throw new Exception("This activity already bookmarked!");

            var userAct = UserActivity.Create(userId, activityId);
            await _userActiviyRepository.AddAsync(userAct);
            await _userActiviyRepository.SaveChangesAsync();

            return true;
        }

        public async Task<List<ActivityDTO>> List(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                throw new Exception("User not found");

            return user.Bookmarks.Select(x => new ActivityDTO
            {
                Address = x.Activity.Address,
                Category = x.Activity.Category,
                Date = x.Activity.Date,
                Description = x.Activity.Description,
                Location = x.Activity.Location,
                Name = x.Activity.Name
            }).ToList();
        }

        public async Task<bool> Remove(int Id)
        {
            var activity = await _userActiviyRepository.GetByIdAsync(Id);
            if (activity is null)
                throw new Exception("Activity not found!");

            _userActiviyRepository.Remove(activity);
            await _userActiviyRepository.SaveChangesAsync();

            return true;
        }
    }
}
