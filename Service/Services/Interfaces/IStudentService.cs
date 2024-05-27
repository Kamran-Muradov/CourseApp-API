using Service.DTOs.Admin.Students;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateAsync(StudentCreateDto data);
        Task EditAsync(int? id, StudentEditDto data);
        Task DeleteAsync(int? id);
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int? id);
        Task<IEnumerable<StudentDto>> SearchAsync(string searchText);
        Task<IEnumerable<StudentDto>> FilterAsync(string name, string? surname, int? age);
        Task AddGroupAsync(StudentAddDeleteGroupDto data);
        Task DeleteGroupAsync(StudentAddDeleteGroupDto data);
    }
}
