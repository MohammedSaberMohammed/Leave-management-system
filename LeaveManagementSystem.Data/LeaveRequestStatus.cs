using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Data;

public class LeaveRequestStatus
{
    public int Id { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; }
}