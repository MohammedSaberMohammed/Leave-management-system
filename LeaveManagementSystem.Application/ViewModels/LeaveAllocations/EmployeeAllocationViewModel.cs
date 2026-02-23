using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.ViewModels.LeaveAllocations;

public class EmployeeAllocationViewModel: EmployeeListViewModel
{
    [Display(Name = "Date Of Birth")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
    
    public bool IsCompletedAllocation { get; set; }
    public List<LeaveAllocationViewModel> LeaveAllocations { get; set; } = new List<LeaveAllocationViewModel>();
}