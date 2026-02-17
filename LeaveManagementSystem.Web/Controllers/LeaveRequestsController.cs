using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using LeaveManagementSystem.Web.ViewModels.LeaveRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagementSystem.Web.Controllers;

[Authorize]
public class LeaveRequestsController(
    ILeaveRequestsService _leaveRequestsService, 
    ILeaveTypesService _leaveTypesService
    ) : Controller
{
    // Employee Requests List Page
    public async Task<IActionResult> Index()
    {
        return View();
    }

    // Employee Create Request Page
    public async Task<IActionResult> Create()
    {
        var leaveTypes = await _leaveTypesService.GetAll();
        var leaveTypesList = new SelectList(leaveTypes, "Id", "Name");
        var model = new LeaveRequestCreateViewModel()
        {
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            LeaveTypes = leaveTypesList
        };

        return View(model);
    }

    // Employee Create Request Submit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveRequestCreateViewModel model)
    {
        if (await _leaveRequestsService.RequestDatesExceedAllocation(model))
        {
            ModelState.AddModelError(string.Empty, "You have exceeded your leave allocation for this leave type.");
            ModelState.AddModelError(nameof(model.EndDate), "The number of days requested is not valid.");
        }
        
        if (ModelState.IsValid)
        {
            await _leaveRequestsService.CreateLeaveRequest(model);

            return RedirectToAction(nameof(Index));
        }
        
        var leaveTypes = await _leaveTypesService.GetAll();
        model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");

        return View(model);
    }
    
    // Employee Cancel Request
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int leaveRequestId)
    {
        return View();
    }
    
    // Admin/Supervisor Review Requests
    public async Task<IActionResult> ListRequests()
    {
        return View();
    }
    
    // Admin/Supervisor Review Requests
    public async Task<IActionResult> Review(int leaveRequestId)
    {
        return View();
    }
    
    // Admin/Supervisor Review Requests
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review()
    {
        return View();
    }
}