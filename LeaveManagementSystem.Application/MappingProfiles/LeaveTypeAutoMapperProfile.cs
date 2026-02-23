using AutoMapper;
using LeaveManagementSystem.Application.ViewModels.LeaveAllocations;
using LeaveManagementSystem.Application.ViewModels.LeaveTypes;

namespace LeaveManagementSystem.Application.MappingProfiles
{
    public class LeaveTypeAutoMapperProfile : Profile
    {
        public LeaveTypeAutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyViewModel>();
            CreateMap<LeaveType, LeaveAllocationEditViewModel>();
            CreateMap<LeaveType, LeaveTypeCreateViewModel>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeEditViewModel>().ReverseMap();
        }
    }
}
