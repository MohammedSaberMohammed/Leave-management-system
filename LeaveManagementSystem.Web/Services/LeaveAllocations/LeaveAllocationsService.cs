using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.ViewModels.LeaveAllocations;

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
        var leaveTypes = await _context.LeaveTypes
            .Where(q => !q.LeaveAllocations.Any(l => l.EmployeeId == employeeId))
            .ToListAsync();

        DateTime currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(p => p.EndDate.Year == currentDate.Year);
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

    private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
    {
        var allocations = await _context.LeaveAllocations
            .Include(l => l.LeaveType)
            .Include(l => l.Period)
            .Where(l => l.EmployeeId == userId)
            .ToListAsync();
        
        return allocations;
    }

    public async Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string? userId)
    {
        var user = await GetUserOrDefaultAsync(userId);
        var allocations = await GetAllocations(user.Id);
        var allocationsViewModel = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationViewModel>>(allocations);
        var leaveTypesCount = await _context.LeaveTypes.CountAsync();
        
        var employeeViewModel = new EmployeeAllocationViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            LeaveAllocations = allocationsViewModel,
            IsCompletedAllocation = leaveTypesCount == allocations.Count
        };
     
        return employeeViewModel;
    }

    public async Task<LeaveAllocationEditViewModel> GetEmployeeAllocation(string? allocationId)
    {
        var allocation = await _context.LeaveAllocations
            .Include(l => l.LeaveType)
            .Include(l => l.Employee)
            .FirstOrDefaultAsync(l => l.Id.ToString() == allocationId);
        
        var allocationViewModel = _mapper.Map<LeaveAllocationEditViewModel>(allocation);
        
        return allocationViewModel;
    }

    private async Task<ApplicationUser> GetUserOrDefaultAsync(string? userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        }
        
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<List<EmployeeListViewModel>> GetEmployees()
    {
        var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
        var employees = _mapper.Map<List<EmployeeListViewModel>>(users);
        
        return employees;
    }

    public async Task EditAllocation(LeaveAllocationEditViewModel allocationEditViewModel)
    {
        // var leaveAllocation = await GetEmployeeAllocation(allocationEditViewModel.Id);
        //
        // if (leaveAllocation == null)
        // {
        //     throw new Exception("Allocation not found");
        // }
        //
        // leaveAllocation.Days = allocationEditViewModel.Days;

        // _context.Update(leaveAllocation);

        // LeaveAllocation leaveAllocation = new LeaveAllocation()
        // {
        //     EmployeeId = employeeId,
        //     LeaveTypeId = leaveType.Id,
        //     PeriodId = period.Id,
        //     Days = (int)Math.Ceiling(remainingMonths * accuralRate),
        // };
        //     
        // _context.LeaveAllocations.Add(leaveAllocation);
        // return Task.CompletedTask;

        await _context.LeaveAllocations
            .Where(q => q.Id.ToString() == allocationEditViewModel.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditViewModel.Days));
    }
}