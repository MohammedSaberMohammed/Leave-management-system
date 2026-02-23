using AutoMapper;
using LeaveManagementSystem.Application.ViewModels.LeaveAllocations;
using LeaveManagementSystem.Application.ViewModels.Periods;

namespace LeaveManagementSystem.Application.MappingProfiles
{
    public class LeaveAllocationAutoMapperProfile : Profile
    {
        public LeaveAllocationAutoMapperProfile()
        {
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>();
            CreateMap<LeaveAllocation, LeaveAllocationEditViewModel>();
            CreateMap<ApplicationUser, EmployeeListViewModel>();
            CreateMap<Period, PeriodViewModel>().ReverseMap();
        }
    }
}
