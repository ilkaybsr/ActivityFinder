using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    public class UserActivityController : ControllerBase
    {
        public Task<IActionResult> BookMark(int Id)
        {
            var userId = Guid.TryParse();
        }

        public Task<IActionResult> RemoveBookmark(int Id)
        {

        }

        public Task<IActionResult> List()
        {

        }
    }
}
