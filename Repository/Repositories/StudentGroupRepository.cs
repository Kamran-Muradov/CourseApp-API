using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class StudentGroupRepository : BaseRepository<StudentGroup>, IStudentGroupRepository
    {
        public StudentGroupRepository(AppDbContext context) : base(context) { }
    }
}
