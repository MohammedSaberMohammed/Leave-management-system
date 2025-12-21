using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.ViewModels;

namespace LeaveManagementSystem.Web.MappingProfiles
{
    public class LeaveTypeAutoMapperProfile : Profile
    {
        public LeaveTypeAutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyViewModel>();
            CreateMap<LeaveType, LeaveTypeEditViewModel>().ReverseMap();
        }
    }
}
