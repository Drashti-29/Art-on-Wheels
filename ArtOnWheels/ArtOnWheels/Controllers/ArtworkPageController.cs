using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    public class ArtworkPageController : Controller
    {
        private readonly IArtworkService _artworkService;

        public ArtworkPageController(IArtworkService artworkService)
        {
            _artworkService = artworkService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        // List all artworks
        public async Task<IActionResult> List()
        {
            return View(await _artworkService.ListArtworks());
        }
        // View artwork details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var artwork = await _artworkService.GetArtwork(id);
            if (artwork == null)
                return NotFound();

            return View(artwork);
        }
        // Create new artwork (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ArtworkDto artworkDto)
        {
            ServiceResponse response = await _artworkService.CreateArtwork(artworkDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("List", "ArtworkPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }
        // Edit artwork (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var artwork = await _artworkService.GetArtwork(id);
            if (artwork == null)
            {
                return NotFound();
            }
            return View(artwork);
        }
        // Edit artwork (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, ArtworkDto artworkDto)
        {
            ServiceResponse response = await _artworkService.UpdateArtworkDetails(id, artworkDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("List", "ArtworkPage");
            }
            else
            {
                return View("something went wrong");
            }
        }
        // Delete artwork (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var artwork = await _artworkService.GetArtwork(id);
            if (artwork == null)
                return NotFound();

            return View(artwork);
        }
        // Delete artwork (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ServiceResponse response = await _artworkService.DeleteArtwork(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "ArtworkPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }
    }
}
