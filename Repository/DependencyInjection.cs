using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IStudentGroupRepository, StudentGroupRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IGroupTeacherRepository, GroupTeacherRepository>();

            return services;
        }
    }
}
