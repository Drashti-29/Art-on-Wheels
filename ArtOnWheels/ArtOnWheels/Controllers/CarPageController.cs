using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    public class CarPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
