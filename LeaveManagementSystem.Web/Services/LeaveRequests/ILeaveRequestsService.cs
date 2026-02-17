using LeaveManagementSystem.Web.ViewModels.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests;

public interface ILeaveRequestsService
{
    Task CreateLeaveRequest(LeaveRequestCreateViewModel model);
    Task<EmployeeLeaveRequestListViewModel> GetEmployeeLeaveRequests(string userId);
    Task<LeaveRequestListViewModel> GetAllLeaveRequests();
    Task CancelLeaveRequest(LeaveRequestCreateViewModel model);
    Task ReviewLeaveRequest(ReviewLeaveRequestViewModel model);
    Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateViewModel model);
}