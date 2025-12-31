using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.ViewModels.LeaveTypes
{
    public class LeaveTypeReadOnlyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Maximum Allocation of days")]
        public int NumberOfDays { get; set; }
    }
}