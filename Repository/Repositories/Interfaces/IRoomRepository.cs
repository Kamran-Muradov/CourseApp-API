using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<bool> ExistWithNameAsync(string name);
        Task<bool> ExistWithNameExceptIdAsync(int id, string name);
    }
}
