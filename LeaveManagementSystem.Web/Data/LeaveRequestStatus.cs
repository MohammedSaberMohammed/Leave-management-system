using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Data;

public class LeaveRequestStatus
{
    public int Id { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; }
}