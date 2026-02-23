using LeaveManagementSystem.Application.ViewModels.LeaveAllocations;

namespace LeaveManagementSystem.Application.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeave(string employeeId);

    Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string? userId);
    Task<LeaveAllocationEditViewModel> GetEmployeeAllocation(string allocationId);
    Task<List<EmployeeListViewModel>> GetEmployees();
    Task EditAllocation(LeaveAllocationEditViewModel allocationEditViewModel);
    Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId);
}