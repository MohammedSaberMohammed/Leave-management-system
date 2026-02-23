using AutoMapper;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.Users;
using LeaveManagementSystem.Application.ViewModels.LeaveAllocations;
using LeaveManagementSystem.Application.ViewModels.LeaveRequests;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Application.Services.LeaveRequests;

public class LeaveRequestsService(
    IMapper _mapper,
    ApplicationDbContext _context, 
    IUsersService _userService,
    ILeaveAllocationsService _leaveAllocationService
        ) : ILeaveRequestsService
{
    public async Task CreateLeaveRequest(LeaveRequestCreateViewModel model)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(model);
        // Todo: Fix param overload
        var user = await _userService.GetLoggedInUser();
        
        leaveRequest.EmployeeId = user.Id;
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;
        
        _context.LeaveRequests.Add(leaveRequest);
        
        await UpdateAllocationDays(leaveRequest, true);
        await _context.SaveChangesAsync();
    }
    
    public async Task<EmployeeLeaveRequestListViewModel> AdminGetAllLeaveRequests()
    {
        var leaveRequests = await _context.LeaveRequests.Include(l => l.LeaveType).ToListAsync();

        var leaveRequestsViewModels = leaveRequests.Select(q => new LeaveRequestReadOnlyViewModel
        {
            Id = q.Id,
            StartDate = q.StartDate,
            EndDate = q.EndDate,
            LeaveType = q.LeaveType.Name,
            NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber,
            LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
        }).ToList();

        var model = new EmployeeLeaveRequestListViewModel
        {
            LeaveRequests = leaveRequestsViewModels,
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved),
            PendingRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending),
            DeclinedRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Canceled)
        };
        
        return model;
    }
    
    public async Task<List<LeaveRequestReadOnlyViewModel>> GetEmployeeLeaveRequests()
    {
        var user = await _userService.GetLoggedInUser();
        var leaveRequests = await _context.LeaveRequests
            .Include(l => l.LeaveType)
            .Where(r => r.EmployeeId == user.Id)
            .Select(q => new LeaveRequestReadOnlyViewModel
            {
                Id = q.Id,
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                LeaveType = q.LeaveType.Name,
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
            })
            .ToListAsync();

        return leaveRequests;
    }

    public async Task CancelLeaveRequest(int leaveRequestId)
    {
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Canceled;
        
        await UpdateAllocationDays(leaveRequest, false);
        await _context.SaveChangesAsync();
    }

    public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
    {
        var user = await _userService.GetLoggedInUser();
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        
        leaveRequest.ReviewerId = user.Id;
        leaveRequest.LeaveRequestStatusId = approved ? (int)LeaveRequestStatusEnum.Approved : (int)LeaveRequestStatusEnum.Declined;

        if (!approved)
        {
            await UpdateAllocationDays(leaveRequest, false);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<ReviewLeaveRequestViewModel> GetLeaveRequestForReview(int id)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(q => q.LeaveType)
            .FirstAsync(q => q.Id == id);
        
        var user = await _userService.GetUserById(leaveRequest.EmployeeId);

        var model = new ReviewLeaveRequestViewModel
        {
            Id = leaveRequest.Id,
            StartDate = leaveRequest.StartDate,
            EndDate = leaveRequest.EndDate,
            LeaveType = leaveRequest.LeaveType.Name,
            RequestComments = leaveRequest.RequestComments,
            NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
            LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequest.LeaveRequestStatusId,
            Employee = new EmployeeListViewModel
            {
                Id = leaveRequest.EmployeeId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            }
        };

        return model;
    }

    public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateViewModel model)
    {
        var user = await _userService.GetLoggedInUser();
        var currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(p => p.EndDate.Year == currentDate.Year);
        var allocation = await _context.LeaveAllocations
            .FirstAsync(l => l.LeaveTypeId == model.LeaveTypeId 
                             && l.EmployeeId == user.Id
                             && l.PeriodId == period.Id);
        var deductedDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        
        return deductedDays > allocation.Days;
    }

    private async Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays)
    {
        var allocation = await _leaveAllocationService.GetCurrentAllocation(leaveRequest.LeaveTypeId, leaveRequest.EmployeeId);
        var numberOfDays = CalculateDays(leaveRequest.StartDate, leaveRequest.EndDate);

        if (deductDays)
        {
            allocation.Days -= numberOfDays;
        }
        else
        {
            allocation.Days += numberOfDays;
        }
        
        _context.Entry(allocation).State = EntityState.Modified;
    }

    private int CalculateDays(DateOnly start, DateOnly end)
    {
        return end.DayNumber - start.DayNumber;
    }
}