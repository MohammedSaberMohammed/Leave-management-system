using System.ComponentModel.DataAnnotations;
using LeaveManagementSystem.Application.ViewModels.LeaveTypes;
using LeaveManagementSystem.Application.ViewModels.Periods;

namespace LeaveManagementSystem.Application.ViewModels.LeaveAllocations;

public class LeaveAllocationViewModel
{
    public string Id { get; set; } = string.Empty;
    
    [Display(Name = "Number Of Days")]
    public int Days { get; set; }
    
    [Display(Name = "Allocation Period")]
    public PeriodViewModel Period { get; set; } = new PeriodViewModel();

    public LeaveTypeReadOnlyViewModel LeaveType { get; set; } = new LeaveTypeReadOnlyViewModel();
}