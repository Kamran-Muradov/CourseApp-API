using Service.DTOs.Admin.Groups;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Task CreateAsync(GroupCreateDto data);
        Task EditAsync(int? id, GroupEditDto data);
        Task DeleteAsync(int? id);
        Task<IEnumerable<GroupDto>> GetAllAsync();
        Task<GroupDto> GetByIdAsync(int? id);
        Task<IEnumerable<GroupDto>> SearchByNameAsync(string name);
        Task<IEnumerable<GroupDto>> SortAsync(string sortKey, bool? isDescending = false);
    }
}
