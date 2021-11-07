using Business.Concrate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ActivityFinder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly ILogger<ActivityController> _logger;
        private readonly ActivityCollectorService _activityCollectorService;

        public ActivityController(ILogger<ActivityController> logger,
            ActivityCollectorService activityCollectorService)
        {
            _logger = logger;
            _activityCollectorService = activityCollectorService;
        }

        [HttpGet]
        [Route(nameof(Collect))]
        public async Task<IActionResult> Collect()
        {
            var result = await _activityCollectorService.Collect();

            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _activityCollectorService.GetAllActivities();

            return Ok(result);
        }
    }
}
