using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    public class ArtistPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
