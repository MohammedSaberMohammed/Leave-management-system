using LeaveManagementSystem.Web.ViewModels;

namespace LeaveManagementSystem.Web.Services
{
    public interface ILeaveTypesService
    {
        Task Create(LeaveTypeCreateViewModel model);
        Task Edit(LeaveTypeEditViewModel model);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<LeaveTypeReadOnlyViewModel>> GetAll();
        Task Remove(int id);
        Task<bool> LeaveTypeExists(int id);
    }
}