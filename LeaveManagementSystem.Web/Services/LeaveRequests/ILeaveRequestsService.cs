using LeaveManagementSystem.Web.ViewModels.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests;

public interface ILeaveRequestsService
{
    Task CreateLeaveRequest(LeaveRequestCreateViewModel model);
    Task<List<LeaveRequestReadOnlyViewModel>> GetEmployeeLeaveRequests();
    Task<EmployeeLeaveRequestListViewModel> AdminGetAllLeaveRequests();
    Task CancelLeaveRequest(int leaveRequestId);
    Task ReviewLeaveRequest(int id, bool approved);
    Task<ReviewLeaveRequestViewModel> GetLeaveRequestForReview(int id);
    Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateViewModel model);
}