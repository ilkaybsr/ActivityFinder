using Business.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    public class UserActivityController : ControllerBase
    {
        private readonly IUserActivityService _userActivityService;

        public UserActivityController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }

        public async Task<IActionResult> BookMark(int activityId)
        {
            if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
                return Unauthorized();

            await _userActivityService.Bookmark(userId, activityId);

            return Ok();
        }

        public async Task<IActionResult> RemoveBookmark(int activityId)
        {
            if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
                return Unauthorized();


            await _userActivityService.Remove(activityId);

            return Ok();
        }

        public async Task<IActionResult> List()
        {
            if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
                return Unauthorized();

            var result = await _userActivityService.List(userId);

            return Ok(result);
        }
    }
}
