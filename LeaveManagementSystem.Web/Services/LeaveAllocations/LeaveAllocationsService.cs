using AutoMapper;
using LeaveManagementSystem.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations;

public class LeaveAllocationsService(
    IMapper _mapper,
    ApplicationDbContext _context, 
    IHttpContextAccessor _httpContextAccessor, 
    UserManager<ApplicationUser> _userManager
    ) : ILeaveAllocationsService
{
    public async Task AllocateLeave(string employeeId)
    {
        var leaveTypes = await _context.LeaveTypes.ToListAsync();

        DateTime currentDate = DateTime.Now;
        Period period = await _context.Periods.SingleAsync(p => p.EndDate.Year == currentDate.Year);
        int remainingMonths = period.EndDate.Month - period.StartDate.Month;
        
        foreach (var leaveType in leaveTypes)
        {
            decimal accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
            
            LeaveAllocation leaveAllocation = new LeaveAllocation()
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = period.Id,
                Days = (int)Math.Ceiling(remainingMonths * accuralRate),
            };
            
            _context.LeaveAllocations.Add(leaveAllocation);
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task<List<LeaveAllocation>> GetAllocations()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var allocations = await _context.LeaveAllocations
            .Include(l => l.LeaveType)
            .Include(l => l.Period)
            .Where(l => l.EmployeeId == user.Id)
            .ToListAsync();
        
        return allocations;
    }

    public async Task<EmployeeAllocationViewModel> GetEmployeeAllocation()
    {
        var allocations = await GetAllocations();
        var allocationsViewModel = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationViewModel>>(allocations);
        
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var employeeViewModel = new EmployeeAllocationViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            LeaveAllocations = allocationsViewModel
        };
     
        return employeeViewModel;
    }
}