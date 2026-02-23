using LeaveManagementSystem.Application.Services.Email;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using LeaveManagementSystem.Application.Services.Periods;
using LeaveManagementSystem.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace LeaveManagementSystem.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped<ILeaveTypesService, LeaveTypesService>();
        services.AddScoped<ILeaveAllocationsService, LeaveAllocationsService>();
        services.AddScoped<ILeaveRequestsService, LeaveRequestsService>();
        services.AddScoped<IPeriodsService, PeriodsService>();
        services.AddScoped<IUsersService, UsersService>();
        
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ApplicationServiceRegistration).Assembly));

        return services;
    }
}