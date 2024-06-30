using Service.DTOs.Admin.Teachers;

namespace Service.Services.Interfaces
{
    public interface ITeacherService
    {
        Task CreateAsync(TeacherCreateDto data);
        Task EditAsync(int? id, TeacherEditDto data);
        Task DeleteAsync(int? id);
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto> GetByIdAsync(int? id);
        Task<IEnumerable<TeacherDto>> SearchAsync(string searchText);
        Task<IEnumerable<TeacherDto>> SortAsync(string sortKey, bool? isDescending = false);
        Task AddGroupAsync(TeacherAddDeleteGroupDto data);
        Task DeleteGroupAsync(TeacherAddDeleteGroupDto data);
    }
}
