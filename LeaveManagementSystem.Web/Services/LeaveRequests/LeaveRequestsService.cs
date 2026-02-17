using AutoMapper;
using LeaveManagementSystem.Web.ViewModels.LeaveRequests;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveRequests;

public class LeaveRequestsService(
    IMapper _mapper,
    ApplicationDbContext _context, 
    IHttpContextAccessor _httpContextAccessor, 
    UserManager<ApplicationUser> _userManager
    ) : ILeaveRequestsService
{
    public async Task CreateLeaveRequest(LeaveRequestCreateViewModel model)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(model);
        // Todo: Fix param overload
        var user = await GetUserOrDefaultAsync(string.Empty);
        
        leaveRequest.EmployeeId = user.Id;
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatus.Pending;
        
        _context.LeaveRequests.Add(leaveRequest);
        
        var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        var allocationToDeduct = await _context.LeaveAllocations
            .FirstAsync(l => l.LeaveTypeId == model.LeaveTypeId && l.EmployeeId == user.Id);
        
        allocationToDeduct.Days -= numberOfDays;
        // _context.LeaveAllocations.Update(allocationToDeduct);
        
        await _context.SaveChangesAsync();
    }

    public Task<EmployeeLeaveRequestListViewModel> GetEmployeeLeaveRequests(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<LeaveRequestListViewModel> GetAllLeaveRequests()
    {
        throw new NotImplementedException();
    }

    public Task CancelLeaveRequest(LeaveRequestCreateViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<LeaveRequestListViewModel> GetLeaveRequests()
    {
        throw new NotImplementedException();
    }

    public Task CancelLeaveRequest(int leaveRequestId)
    {
        throw new NotImplementedException();
    }

    public Task ReviewLeaveRequest(ReviewLeaveRequestViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateViewModel model)
    {
        // Todo: Fix param overload
        var user = await GetUserOrDefaultAsync(string.Empty);
        var allocation = await _context.LeaveAllocations
            .FirstAsync(l => l.LeaveTypeId == model.LeaveTypeId && l.EmployeeId == user.Id);
        var deductedDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        
        return deductedDays > allocation.Days;
    }

    private async Task<ApplicationUser> GetUserOrDefaultAsync(string? userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        }
        
        return await _userManager.FindByIdAsync(userId);
    }
    
}