using LeaveManagementSystem.Web.Services.LeaveAllocations;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    public class LeaveAllocationController(ILeaveAllocationsService _leaveAllocationsService) : Controller
    {
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var leaveAllocations = await _leaveAllocationsService.GetAllocations();
            
            return View(leaveAllocations);
        }

    }
}
