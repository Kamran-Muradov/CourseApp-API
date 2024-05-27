using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<bool> ExistWithNameAsync(string name);
        Task<bool> ExistWithNameExceptIdAsync(int id, string name);
    }
}
