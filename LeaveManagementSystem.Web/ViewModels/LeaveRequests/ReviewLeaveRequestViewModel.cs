using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LeaveManagementSystem.Web.ViewModels.LeaveAllocations;

namespace LeaveManagementSystem.Web.ViewModels.LeaveRequests;

public class ReviewLeaveRequestViewModel : LeaveRequestReadOnlyViewModel
{
    [DisplayName("Additional Information")]
    public string RequestComments { get; set; }
    
    public EmployeeListViewModel Employee { get; set; } = new EmployeeListViewModel();
}