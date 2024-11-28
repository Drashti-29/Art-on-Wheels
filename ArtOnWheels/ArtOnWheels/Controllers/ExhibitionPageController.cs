using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    public class ExhibitionPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
