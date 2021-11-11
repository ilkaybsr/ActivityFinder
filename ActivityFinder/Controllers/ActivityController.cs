using API.Concrate;
using Business.Abstracts;
using Business.Concrate;
using Business.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ActivityFinder.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly ILogger<ActivityController> _logger;
        private readonly IActivityCollectorService _activityCollectorService;

        public ActivityController(ILogger<ActivityController> logger,
            IActivityCollectorService activityCollectorService)
        {
            _logger = logger;
            _activityCollectorService = activityCollectorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ActivityDTO), (int)HttpStatusCode.OK)]
        [Route(nameof(Collect))]
        public async Task<IActionResult> Collect(int limit = 10)
        {
            var result = await _activityCollectorService.Collect(limit);

            return Ok(result);

        }

        [HttpPost]
        [ProducesResponseType(typeof(List<ActivityDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> List([FromBody] ActivityListFilter filter)
        {
            var result = await _activityCollectorService.GetAllActivities(filter.PageSize.Value, filter.ItemSize.Value);

            return Ok(result);
        }
    }
}
