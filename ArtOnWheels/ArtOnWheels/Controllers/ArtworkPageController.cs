using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using ArtOnWheels.Services;
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
        public async Task<IActionResult> List()
        {
            return View(await _artworkService.ListArtworks());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var artwork = await _artworkService.GetArtwork(id);
            if (artwork == null)
            {
                return NotFound();
            }
            return View(artwork);
        }

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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var artworkDto = await _artworkService.GetArtwork(id);
            if (artworkDto == null)
            {
                return NotFound();
            }
            return View(artworkDto);
        }
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var artwork = await _artworkService.GetArtwork(id);
            if (artwork == null)
            {
                return NotFound();
            }
            return View(artwork);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
