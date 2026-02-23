using AutoMapper;
using LeaveManagementSystem.Application.ViewModels.LeaveAllocations;
using LeaveManagementSystem.Application.ViewModels.LeaveRequests;
using LeaveManagementSystem.Application.ViewModels.Periods;

namespace LeaveManagementSystem.Application.MappingProfiles;

public class LeaveRequestAutoMapperProfile : Profile
{
    public LeaveRequestAutoMapperProfile()
    {
        CreateMap<LeaveRequestCreateViewModel, LeaveRequest>();
    }
}