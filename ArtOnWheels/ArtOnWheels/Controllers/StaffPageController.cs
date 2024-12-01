using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class StaffPageController : Controller
{
    private readonly IStaffService _staffService;

    public StaffPageController(IStaffService staffService)
    {
        _staffService = staffService;
    }
    public IActionResult Index()
    {
        return RedirectToAction("List");
    }

    public async Task<IActionResult> List()
    {
        var staffs = await _staffService.ListStaffs();
        return View(staffs);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var staff = await _staffService.GetStaff(id);
        if (staff == null)
        {
            return NotFound();
        }
        return View(staff);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(StaffDto staffDto)
    {
        ServiceResponse response = await _staffService.CreateStaff(staffDto);

        if (response.Status == ServiceResponse.ServiceStatus.Created)
        {
            return RedirectToAction("List", "StaffPage");
        }
        else
        {
            return View("Error", new ErrorViewModel() { Errors = response.Messages });
        }
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var staff = await _staffService.GetStaff(id);
        if (staff == null)
            return NotFound();

        return View(staff);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, StaffDto staffDto)
    {
        ServiceResponse response = await _staffService.UpdateStaff(id, staffDto);

        if (response.Status == ServiceResponse.ServiceStatus.Updated)
        {
            return RedirectToAction("List", "StaffPage");
        }
        else
        {
            return View("something went wrong");
        }
    }

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id)
    {
        var staff = await _staffService.GetStaff(id);
        if (staff == null)
            return NotFound();

        return View(staff);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        ServiceResponse response = await _staffService.DeleteStaff(id);
        if (response.Status == ServiceResponse.ServiceStatus.Deleted)
        {
            return RedirectToAction("List", "StaffPage");
        }
        else
        {
            return View("Error", new ErrorViewModel() { Errors = response.Messages });
        };
    }

}
