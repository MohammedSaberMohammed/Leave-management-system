using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.ViewModels;

namespace LeaveManagementSystem.Web.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyViewModel>();
            CreateMap<LeaveTypeCreateViewModel, LeaveType>();
            CreateMap<LeaveType, LeaveTypeEditViewModel>();
            CreateMap<LeaveTypeEditViewModel, LeaveType>();
        }
    }
}
