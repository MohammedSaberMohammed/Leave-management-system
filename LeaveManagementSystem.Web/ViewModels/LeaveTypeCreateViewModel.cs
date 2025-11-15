using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.ViewModels
{
    public class LeaveTypeCreateViewModel
    {
        [Required]
        [Length(4, 150, ErrorMessage = "You have violated the length requirements")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 90)]
        [Display(Name = "Maximum Allocation of days")]
        public int NumberOfDays { get; set; }
    }
}