using AutoMapper;
using Domain.Entities;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Rooms;
using Service.DTOs.Admin.Students;
using Service.DTOs.Admin.Teachers;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Group
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupEditDto, Group>();
            CreateMap<Group, GroupDto>()
                .ForMember(d => d.StudentCount, opt => opt.MapFrom(s => s.StudentGroups.Count))
                .ForMember(d => d.Teachers, opt => opt.MapFrom(s => s.GroupTeachers.Select(m => m.Teacher.Name)))
                .ForMember(d => d.Education, opt => opt.MapFrom(s => s.Education.Name))
                .ForMember(d => d.Room, opt => opt.MapFrom(s => s.Room.Name));

            //Education
            CreateMap<EducationCreateDto, Education>();
            CreateMap<EducationEditDto, Education>();
            CreateMap<Education, EducationDto>()
                .ForMember(d => d.Groups, opt => opt.MapFrom(s => s.Groups.Select(m => m.Name)));

            //Student
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentEditDto, Student>();
            CreateMap<Student, StudentDto>()
                .ForMember(d => d.Groups, opt => opt.MapFrom(s => s.StudentGroups.Select(m => m.Group.Name)));

            //Room
            CreateMap<RoomCreateDto, Room>();
            CreateMap<RoomEditDto, Room>();
            CreateMap<Room, RoomDto>()
                .ForMember(d => d.Groups, opt => opt.MapFrom(s => s.Groups.Select(m => m.Name)));

            //Teacher
            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<TeacherEditDto, Teacher>();
            CreateMap<Teacher, TeacherDto>()
                .ForMember(d => d.Groups, opt => opt.MapFrom(s => s.GroupTeachers.Select(m => m.Group.Name)));
        }
    }
}
