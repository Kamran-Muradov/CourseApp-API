using Service.DTOs.Admin.Rooms;

namespace Service.Services.Interfaces
{
    public interface IRoomService
    {
        Task CreateAsync(RoomCreateDto data);
        Task EditAsync(int? id, RoomEditDto data);
        Task DeleteAsync(int? id);
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<RoomDto> GetByIdAsync(int? id);
        Task<IEnumerable<RoomDto>> SearchAsync(string searchText);
        Task<IEnumerable<RoomDto>> SortAsync(string sortKey, bool? isDescending = false);
    }
}
