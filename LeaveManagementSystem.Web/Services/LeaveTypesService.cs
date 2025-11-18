using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services
{
    public class LeaveTypesService : ILeaveTypesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LeaveTypesService(ApplicationDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public async Task<List<LeaveTypeReadOnlyViewModel>> GetAll()
        {
            var data = await _context.LeaveTypes.ToListAsync();
            var viewData = _mapper.Map<List<LeaveTypeReadOnlyViewModel>>(data);

            return viewData;
        }

        public async Task<T?> Get<T>(int id) where T : class
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                return null;
            }

            var viewData = _mapper.Map<T>(data);

            return viewData;
        }


        public async Task Remove(int id)
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Create(LeaveTypeCreateViewModel model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);

            _context.Add(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(LeaveTypeEditViewModel model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);

            _context.Update(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> LeaveTypeExists(int id)
        {
            return await _context.LeaveTypes.AnyAsync(e => e.Id == id);
        }
    }
}
