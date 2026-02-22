using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.Periods;

public class PeriodsService(ApplicationDbContext _context) : IPeriodsService
{
    public async Task<Period> GetCurrentPeriod()
    {
        var currentYear = DateTime.Now.Year;
        var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentYear);
        
        return period;
    }
}