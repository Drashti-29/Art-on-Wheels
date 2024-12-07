using ArtOnWheels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
          //  _logger = logger;
        //}

        private readonly ICarArtworkService _carArtworkService;

        public HomeController(ICarArtworkService carArtworkService)
        {
            _carArtworkService = carArtworkService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _carArtworkService.GetCarArtworkViewModel();
            if (viewModel.CarList == null || !viewModel.CarList.Any())
            {
                // Log or debug
                Console.WriteLine("CarList is null or empty");
            }
            return View(viewModel);
        }
    }
}
