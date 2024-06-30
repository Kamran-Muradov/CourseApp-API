using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Rooms;
using Service.DTOs.Admin.Students;
using Service.DTOs.Admin.Teachers;
using Service.Services;
using Service.Services.Interfaces;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });

            services.AddScoped<IValidator<GroupCreateDto>, GroupCreateDtoValidator>();
            services.AddScoped<IValidator<GroupEditDto>, GroupEditDtoValidator>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<IValidator<EducationCreateDto>, EducationCreateDtoValidator>();
            services.AddScoped<IValidator<EducationEditDto>, EducationEditDtoValidator>();
            services.AddScoped<IEducationService, EducationService>();

            services.AddScoped<IValidator<StudentCreateDto>, StudentCreateDtoValidator>();
            services.AddScoped<IValidator<StudentEditDto>, StudentEditDtoValidator>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IValidator<RoomCreateDto>, RoomCreateDtoValidator>();
            services.AddScoped<IValidator<RoomEditDto>, RoomEditDtoValidator>();
            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IValidator<TeacherCreateDto>, TeacherCreateDtoValidator>();
            services.AddScoped<IValidator<TeacherEditDto>, TeacherEditDtoValidator>();
            services.AddScoped<ITeacherService, TeacherService>();

            return services;
        }
    }
}
