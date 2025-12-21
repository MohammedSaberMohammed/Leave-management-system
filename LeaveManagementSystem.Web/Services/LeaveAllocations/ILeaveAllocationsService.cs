using LeaveManagementSystem.Web.ViewModels;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeave(string employeeId);
    Task<List<LeaveAllocation>> GetAllocations();

    Task<EmployeeAllocationViewModel> GetEmployeeAllocation();
}