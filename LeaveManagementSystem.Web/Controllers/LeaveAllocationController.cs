using LeaveManagementSystem.Web.Services.LeaveAllocations;
using LeaveManagementSystem.Web.ViewModels.LeaveAllocations;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationsService _leaveAllocationsService) : Controller
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
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditViewModel allocationEditViewModel)
        {
            await _leaveAllocationsService.EditAllocation(allocationEditViewModel);
            
            return RedirectToAction(nameof(Details), new { userId = allocationEditViewModel.Employee.Id });
        }
    }
}
