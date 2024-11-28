using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalArtShowcase.Controllers
{
    /// <summary>
    /// ArtistsController handles artist-related operations for the MVC application.
    /// Provides actions to:
    /// - List all artists
    /// - View details of a specific artist
    /// - Add a new artist
    /// - Update an existing artist
    /// - Delete an artist
    /// </summary>
    public class ArtistPageController : Controller
    {
        private readonly IArtistService _artistService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistPageController "/> class.
        /// </summary>
        /// <param name="artistService">The artist service interface for performing operations.</param>
        public ArtistPageController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: api/ArtistAPI
        /// <summary>
        /// Retrieves a list of all artists.
        /// </summary>
        /// <returns>A collection of ArtistDto objects.</returns>
        public async Task<IActionResult> List()
        {
            return View(await _artistService.ListArtists());
        }

        /// <summary>
        /// Displays details of a specific artist.
        /// </summary>
        /// <param name="id">The ID of the artist to view.</param>
        /// <returns>A view displaying the artist's details.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var artist = await _artistService.GetArtist(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        /// <summary>
        /// Displays the form to create a new artist.
        /// </summary>
        /// <returns>A view with the create artist form.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the submission of the create artist form.
        /// </summary>
        /// <param name="artistDto">The artist details to create.</param>
        /// <returns>Redirects to the index page on success.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistDto artistDto)
        {
            ServiceResponse response = await _artistService.CreateArtist(artistDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("List", "ArtistPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        /// <summary>
        /// Displays the form to edit an existing artist.
        /// </summary>
        /// <param name="id">The ID of the artist to edit.</param>
        /// <returns>A view with the edit artist form.</returns>
        public async Task<IActionResult> Edit(int id)
        {
            var artist = await _artistService.GetArtist(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        /// <summary>
        /// Handles the submission of the edit artist form.
        /// </summary>
        /// <param name="id">The ID of the artist to update.</param>
        /// <param name="artistDto">The updated artist details.</param>
        /// <returns>Redirects to the index page on success.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtistDto artistDto)
        {
            ServiceResponse response = await _artistService.UpdateArtistDetails(id, artistDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("List", "ArtistPage");
            }
            else
            {
                return View("something went wrong");
            }
        }

            /// <summary>
            /// Displays a confirmation page to delete an artist.
            /// </summary>
            /// <param name="id">The ID of the artist to delete.</param>
            /// <returns>A view confirming the delete action.</returns>
            public async Task<IActionResult> Delete(int id)
        {
            var artist = await _artistService.GetArtist(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        /// <summary>
        /// Handles the deletion of an artist.
        /// </summary>
        /// <param name="id">The ID of the artist to delete.</param>
        /// <returns>Redirects to the index page on success.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ServiceResponse response = await _artistService.DeleteArtist(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "ArtistPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            };
        }
    }
}
