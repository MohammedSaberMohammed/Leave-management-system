using LeaveManagementSystem.Web.Services.LeaveAllocations;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using LeaveManagementSystem.Web.ViewModels.LeaveAllocations;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(
        ILeaveAllocationsService _leaveAllocationsService,
        ILeaveTypesService _leaveTypesService
        ) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<ActionResult> Index()
        {
            var employees = await _leaveAllocationsService.GetEmployees();
            
            return View(employees);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> AllocateLeave(string id)
        {
            await _leaveAllocationsService.AllocateLeave(id);
            return RedirectToAction(nameof(Details), new { userId = id });
        }
        
        public async Task<ActionResult> Details(string? userId)
        {
            var employeeAllocationViewModel = await _leaveAllocationsService.GetEmployeeAllocations(userId);
            
            return View(employeeAllocationViewModel);
        }

        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> EditAllocation(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _leaveAllocationsService.GetEmployeeAllocation(id);
            if (allocation == null)
            {
                return NotFound();
            }
            
            return View(allocation);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditViewModel allocation)
        {
            if (await _leaveTypesService.DaysExceedsMaximum(allocation.LeaveType.Id, allocation.Days))
            {
                ModelState.AddModelError("Days", "The number of days exceeds the maximum number of days for this leave type.");
            }

            if (ModelState.IsValid)
            {
                await _leaveAllocationsService.EditAllocation(allocation);
            
                return RedirectToAction(nameof(Details), new { userId = allocation.Employee.Id });
            }

            var days = allocation.Days;
            allocation = await _leaveAllocationsService.GetEmployeeAllocation(allocation.Id);
            allocation.Days = days;
            
            return View(allocation);
        }
    }
}
