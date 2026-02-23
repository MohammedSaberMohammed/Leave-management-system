using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.ViewModels.LeaveRequests;

public class EmployeeLeaveRequestListViewModel
{
    [Display(Name = "Total Number Of Requests")]
    public int TotalRequests { get; set; }
    
    [Display(Name = "Approved Requests")]
    public int ApprovedRequests { get; set; }
    
    [Display(Name = "Pending Requests")]
    public int PendingRequests { get; set; }
    
    [Display(Name = "Declined Requests")]
    public int DeclinedRequests { get; set; }

    public List<LeaveRequestReadOnlyViewModel> LeaveRequests { get; set; } = [];
}