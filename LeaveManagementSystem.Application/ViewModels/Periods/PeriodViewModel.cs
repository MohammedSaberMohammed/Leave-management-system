using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.ViewModels.Periods;

public class PeriodViewModel
{
    public int Id { get; set; }
    public int Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

}