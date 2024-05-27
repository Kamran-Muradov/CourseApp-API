using Service.DTOs.Admin.Educations;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        Task CreateAsync(EducationCreateDto data);
        Task EditAsync(int? id, EducationEditDto data);
        Task DeleteAsync(int? id);
        Task<IEnumerable<EducationDto>> GetAllAsync();
        Task<EducationDto> GetByIdAsync(int? id);
        Task<IEnumerable<EducationDto>> SearchAsync(string searchText);
        Task<IEnumerable<EducationDto>> SortAsync(string sortKey, bool? isDescending = false);
    }
}
