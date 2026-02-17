using AutoMapper;
using LeaveManagementSystem.Web.ViewModels.LeaveAllocations;
using LeaveManagementSystem.Web.ViewModels.LeaveRequests;
using LeaveManagementSystem.Web.ViewModels.Periods;

namespace LeaveManagementSystem.Web.MappingProfiles;

public class LeaveRequestAutoMapperProfile : Profile
{
    public LeaveRequestAutoMapperProfile()
    {
        CreateMap<LeaveRequestCreateViewModel, LeaveRequest>();
    }
}