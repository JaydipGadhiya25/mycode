using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_PLATFORM.Controllers
{
    [Authorize]
    public class MissionController : Controller
    {
        public IActionResult MissionListing()
        {
            return View();
        }

        public IActionResult MissionVolunteering()
        {
            return View();
        }
        public IActionResult StoriesListing()
        {
            return View();
        }
    }
}
