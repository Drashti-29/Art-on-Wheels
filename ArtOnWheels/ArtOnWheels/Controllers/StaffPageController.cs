using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    public class StaffPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
