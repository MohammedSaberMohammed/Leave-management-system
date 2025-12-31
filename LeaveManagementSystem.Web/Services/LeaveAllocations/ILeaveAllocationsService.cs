using LeaveManagementSystem.Web.ViewModels.LeaveAllocations;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeave(string employeeId);

    Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string? userId);
    Task<LeaveAllocationEditViewModel> GetEmployeeAllocation(string allocationId);
    Task<List<EmployeeListViewModel>> GetEmployees();
    Task EditAllocation(LeaveAllocationEditViewModel allocationEditViewModel);
}