using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    public class ExhibitionPageController : Controller
    {
            private readonly IExhibitionService _exhibitionService;

            public ExhibitionPageController(IExhibitionService exhibitionService)
            {
                _exhibitionService = exhibitionService;
            }

            public IActionResult Index()
            {
                return RedirectToAction("List");
            }

            public async Task<IActionResult> List()
            {
                var exhibitions = await _exhibitionService.ListExhibitions();
                return View(exhibitions);
            }

            [HttpGet]
            public async Task<IActionResult> Details(int id)
            {
                var exhibition = await _exhibitionService.GetExhibition(id);
                if (exhibition == null) return NotFound();
                return View(exhibition);
            }

        
            [Authorize(Roles = "Admin")]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Create(ExhibitionDto exhibitionDto)
            {
                var response = await _exhibitionService.CreateExhibition(exhibitionDto);
                if (response.Status == ServiceResponse.ServiceStatus.Created)
                    return RedirectToAction("List");
                else
                    return View("Error", new ErrorViewModel { Errors = response.Messages });
            }

        
            
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Edit(int id)
            {
                var exhibition = await _exhibitionService.GetExhibition(id);
                if (exhibition == null) return NotFound();
                return View(exhibition);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Edit(int id, ExhibitionDto exhibitionDto)
            {
                var response = await _exhibitionService.UpdateExhibitionDetails(id, exhibitionDto);
                if (response.Status == ServiceResponse.ServiceStatus.Updated)
                    return RedirectToAction("List");
                else
                    return View("Error", new ErrorViewModel { Errors = response.Messages });
            }
        
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Delete(int id)
            {
                var exhibition = await _exhibitionService.GetExhibition(id);
                if (exhibition == null) return NotFound();
                return View(exhibition);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var response = await _exhibitionService.DeleteExhibition(id);
                if (response.Status == ServiceResponse.ServiceStatus.Deleted)
                    return RedirectToAction("List");
                else
                    return View("Error", new ErrorViewModel { Errors = response.Messages });
            }
    }
}

