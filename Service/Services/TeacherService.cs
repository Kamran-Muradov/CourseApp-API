using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Teachers;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTeacherRepository _groupTeacherRepository;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepository,
                              IGroupRepository groupRepository,
                              IGroupTeacherRepository groupTeacherRepository,
                              IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _groupRepository = groupRepository;
            _groupTeacherRepository = groupTeacherRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(TeacherCreateDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(data));

            Teacher teacher = _mapper.Map<Teacher>(data);

            List<GroupTeacher> groupTeachers = new();

            foreach (var groupId in data.GroupIds)
            {
                if (!await _groupRepository.ExistWithIdAsync(groupId)) throw new NotFoundException("Group not found");

                groupTeachers.Add(new GroupTeacher { GroupId = groupId, TeacherId = teacher.Id });
            }

            teacher.GroupTeachers = groupTeachers;

            await _teacherRepository.CreateAsync(teacher);
        }

        public async Task EditAsync(int? id, TeacherEditDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            ArgumentNullException.ThrowIfNull(nameof(data));

            var teacher = await _teacherRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            _mapper.Map(data, teacher);
            await _teacherRepository.EditAsync(teacher);
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var teacher = await _teacherRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            await _teacherRepository.DeleteAsync(teacher);
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            var teachers = await _teacherRepository.Find(source => source
                .Include(m => m.GroupTeachers)
                .ThenInclude(m => m.Group)).ToListAsync();

            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var teacher = await _teacherRepository.FindBy(m => m.Id == id, source => source
                .Include(m => m.GroupTeachers)
                .ThenInclude(m => m.Group)).FirstOrDefaultAsync();

            return teacher is null ? throw new NotFoundException("Data not found") : _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<IEnumerable<TeacherDto>> SearchAsync(string searchText)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchText));

            var teachers = await _teacherRepository.FindBy(m => m.Name.Contains(searchText) || m.Surname.Contains(searchText), source => source
                .Include(m => m.GroupTeachers)
                .ThenInclude(m => m.Group)).ToListAsync();

            return teachers.Count == 0 ? throw new NotFoundException("Data not found") : _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<IEnumerable<TeacherDto>> SortAsync(string sortKey, bool? isDescending)
        {
            ArgumentNullException.ThrowIfNull(nameof(sortKey));
            ArgumentNullException.ThrowIfNull(nameof(isDescending));

            var teachers = await _teacherRepository.Find(source => source
                .Include(m => m.GroupTeachers)
                .ThenInclude(m => m.Group)).ToListAsync();

            switch (sortKey)
            {
                case "Name":
                    if ((bool)isDescending)
                    {
                        return _mapper.Map<IEnumerable<TeacherDto>>(teachers.OrderByDescending(m => m.Name));
                    }
                    return _mapper.Map<IEnumerable<TeacherDto>>(teachers.OrderBy(m => m.Name));

                case "Salary":
                    if ((bool)isDescending)
                    {
                        return _mapper.Map<IEnumerable<TeacherDto>>(teachers.OrderByDescending(m => m.Salary));
                    }
                    return _mapper.Map<IEnumerable<TeacherDto>>(teachers.OrderBy(m => m.Salary));

                case "Age":
                    if ((bool)isDescending)
                    {
                        return _mapper.Map<IEnumerable<TeacherDto>>(teachers.OrderByDescending(m => m.Age));
                    }
                    return _mapper.Map<IEnumerable<TeacherDto>>(teachers.OrderBy(m => m.Age));

                default:
                    return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
            }
        }

        public async Task AddGroupAsync(TeacherAddDeleteGroupDto data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var teacher = await _teacherRepository.FindBy(m => m.Id == data.TeacherId, source => source
                .Include(m => m.GroupTeachers)).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            if (teacher.GroupTeachers.Any(m => m.GroupId == data.GroupId))
            {
                throw new BadRequestException("Teacher is already in this group");
            }

            if (!await _groupRepository.ExistWithIdAsync(data.GroupId)) throw new NotFoundException("Group not found");

            teacher.GroupTeachers = new List<GroupTeacher> { new()
                {
                    TeacherId = data.TeacherId,
                    GroupId = data.GroupId
                }
            };

            await _teacherRepository.EditAsync(teacher);
        }

        public async Task DeleteGroupAsync(TeacherAddDeleteGroupDto data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var groupTeacher = await _groupTeacherRepository
                .FindBy(m => m.TeacherId == data.TeacherId && m.GroupId == data.GroupId).FirstOrDefaultAsync();

            if (groupTeacher is null) throw new NotFoundException("Data not found");

            await _groupTeacherRepository.DeleteAsync(groupTeacher);
        }
    }
}
