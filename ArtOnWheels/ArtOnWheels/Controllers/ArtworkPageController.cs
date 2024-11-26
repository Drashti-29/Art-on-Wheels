using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    public class ArtworkPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
