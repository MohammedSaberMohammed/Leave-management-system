using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.ViewModels;

namespace LeaveManagementSystem.Web.MappingProfiles
{
    public class LeaveAllocationAutoMapperProfile : Profile
    {
        public LeaveAllocationAutoMapperProfile()
        {
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>();
            CreateMap<Period, PeriodViewModel>().ReverseMap();
        }
    }
}
