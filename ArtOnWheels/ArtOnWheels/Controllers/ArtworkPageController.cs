using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    /// <summary>
    /// ArtworkPageController handles artwork-related operations for the MVC application.
    /// Provides actions to:
    /// - List all artworks
    /// - View details of a specific artwork
    /// - Add a new artwork
    /// - Update an existing artwork
    /// - Delete an artwork
    /// </summary>
    public class ArtworkPageController : Controller
    {
        private readonly IArtworkService _artworkService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtworkPageController"/> class.
        /// </summary>
        /// <param name="artworkService">The artwork service interface for performing operations.</param>
        public ArtworkPageController(IArtworkService artworkService)
        {
            _artworkService = artworkService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        /// <summary>
        /// Retrieves a list of all artworks.
        /// </summary>
        /// <returns>A view displaying the list of artworks.</returns>
        public async Task<IActionResult> List()
        {
            return View(await _artworkService.ListArtworks());
        }

        /// <summary>
        /// Displays details of a specific artwork.
        /// </summary>
        /// <param name="id">The ID of the artwork to view.</param>
        /// <returns>A view displaying the artwork's details.</returns>
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

        /// <summary>
        /// Displays the form to create a new artwork.
        /// </summary>
        /// <returns>A view with the create artwork form.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the submission of the create artwork form.
        /// </summary>
        /// <param name="artworkDto">The artwork details to create.</param>
        /// <returns>Redirects to the index page on success.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        /// <summary>
        /// Displays the form to edit an existing artwork.
        /// </summary>
        /// <param name="id">The ID of the artwork to edit.</param>
        /// <returns>A view with the edit artwork form.</returns>
        public async Task<IActionResult> Edit(int id)
        {
            var artwork = await _artworkService.GetArtwork(id);
            if (artwork == null)
            {
                return NotFound();
            }
            return View(artwork);
        }

        /// <summary>
        /// Handles the submission of the edit artwork form.
        /// </summary>
        /// <param name="id">The ID of the artwork to update.</param>
        /// <param name="artworkDto">The updated artwork details.</param>
        /// <returns>Redirects to the index page on success.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtworkDto artworkDto)
        {
            ServiceResponse response = await _artworkService.UpdateArtworkDetails(id, artworkDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("List", "ArtworkPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        /// <summary>
        /// Displays a confirmation page to delete an artwork.
        /// </summary>
        /// <param name="id">The ID of the artwork to delete.</param>
        /// <returns>A view confirming the delete action.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var artwork = await _artworkService.GetArtwork(id);
            if (artwork == null)
            {
                return NotFound();
            }
            return View(artwork);
        }

        /// <summary>
        /// Handles the deletion of an artwork.
        /// </summary>
        /// <param name="id">The ID of the artwork to delete.</param>
        /// <returns>Redirects to the index page on success.</returns>
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
