using System.ComponentModel.DataAnnotations;
using LeaveManagementSystem.Web.ViewModels.LeaveTypes;
using LeaveManagementSystem.Web.ViewModels.Periods;

namespace LeaveManagementSystem.Web.ViewModels.LeaveAllocations;

public class LeaveAllocationViewModel
{
    public string Id { get; set; } = string.Empty;
    
    [Display(Name = "Number Of Days")]
    public int Days { get; set; }
    
    [Display(Name = "Allocation Period")]
    public PeriodViewModel Period { get; set; } = new PeriodViewModel();

    public LeaveTypeReadOnlyViewModel LeaveType { get; set; } = new LeaveTypeReadOnlyViewModel();
}