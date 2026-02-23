using LeaveManagementSystem.Application.Services.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using LeaveManagementSystem.Application.ViewModels.LeaveRequests;
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
        var model = await _leaveRequestsService.GetEmployeeLeaveRequests();

        return View(model);
    }

    // Employee Create Request Page
    public async Task<IActionResult> Create(int? leaveTypeId)
    {
        var leaveTypes = await _leaveTypesService.GetAll();
        var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);
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
    public async Task<IActionResult> Cancel(int id)
    {
        await _leaveRequestsService.CancelLeaveRequest(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = $"{Roles.Administrator}, {Roles.Supervisor}")]
    public async Task<IActionResult> ListRequests()
    {
        var model = await _leaveRequestsService.AdminGetAllLeaveRequests();
        return View(model);
    }

    [Authorize(Roles = $"{Roles.Administrator}, {Roles.Supervisor}")]
    public async Task<IActionResult> Review(int id)
    {
        var model = await _leaveRequestsService.GetLeaveRequestForReview(id);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = $"{Roles.Administrator}, {Roles.Supervisor}")]
    public async Task<IActionResult> Review(int id, bool approved)
    {
        await _leaveRequestsService.ReviewLeaveRequest(id, approved);

        return RedirectToAction(nameof(ListRequests));
    }
}