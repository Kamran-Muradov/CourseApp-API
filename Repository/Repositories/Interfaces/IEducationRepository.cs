using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IEducationRepository : IBaseRepository<Education>
    {
        Task<bool> ExistWithNameAsync(string name);
        Task<bool> ExistWithNameExceptIdAsync(int id, string name);
    }
}
