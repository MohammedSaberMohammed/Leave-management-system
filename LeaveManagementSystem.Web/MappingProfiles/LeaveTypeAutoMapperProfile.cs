using AutoMapper;
using LeaveManagementSystem.Web.ViewModels.LeaveAllocations;
using LeaveManagementSystem.Web.ViewModels.LeaveTypes;

namespace LeaveManagementSystem.Web.MappingProfiles
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
