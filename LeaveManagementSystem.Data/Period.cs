namespace LeaveManagementSystem.Data;

public class Period
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}