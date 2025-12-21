using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.ViewModels;

public class LeaveAllocationViewModel
{
    public string Id { get; set; }
    
    [Display(Name = "Number Of Days")]
    public int NumberOfDays { get; set; }
    
    [Display(Name = "Allocation Period")]
    public PeriodViewModel Period { get; set; } = new PeriodViewModel();

    public LeaveTypeReadOnlyViewModel LeaveType { get; set; } = new LeaveTypeReadOnlyViewModel();
}