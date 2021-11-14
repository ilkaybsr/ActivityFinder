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
    [Route("[controller]")]
    public class UserActivityController : ControllerBase
    {
        private readonly IUserActivityService _userActivityService;

        public UserActivityController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }

        [Route(nameof(BookMark))]
        [HttpPost]
        public async Task<IActionResult> BookMark(int activityId)
        {
            if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
                return Unauthorized();

            await _userActivityService.Bookmark(userId, activityId);

            return Ok();
        }

        [Route(nameof(RemoveBookmark))]
        [HttpPost]
        public async Task<IActionResult> RemoveBookmark(int Id)
        {
            if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
                return Unauthorized();


            await _userActivityService.Remove(Id);

            return Ok();
        }

        [Route(nameof(BookMarkList))]
        [HttpGet]
        public async Task<IActionResult> BookMarkList()
        {
            if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
                return Unauthorized();

            var result = await _userActivityService.List(userId);

            return Ok(result);
        }
    }
}
