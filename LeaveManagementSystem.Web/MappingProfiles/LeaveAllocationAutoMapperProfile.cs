using AutoMapper;
using LeaveManagementSystem.Web.ViewModels.LeaveAllocations;
using LeaveManagementSystem.Web.ViewModels.Periods;

namespace LeaveManagementSystem.Web.MappingProfiles
{
    public class LeaveAllocationAutoMapperProfile : Profile
    {
        public LeaveAllocationAutoMapperProfile()
        {
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>();
            CreateMap<ApplicationUser, EmployeeListViewModel>();
            CreateMap<Period, PeriodViewModel>().ReverseMap();
        }
    }
}
