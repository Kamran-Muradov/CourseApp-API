using Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task EditAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> Find(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<bool> ExistWithIdAsync(int id);
    }
}
