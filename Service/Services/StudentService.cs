using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Students;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentGroupRepository _studentGroupRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository,
                              IStudentGroupRepository studentGroupRepository,
                              IMapper mapper,
                              IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _studentGroupRepository = studentGroupRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task CreateAsync(StudentCreateDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(data));

            Student student = _mapper.Map<Student>(data);

            List<StudentGroup> studentGroups = new();

            foreach (var groupId in data.GroupIds)
            {
                if (!await _groupRepository.ExistWithIdAsync(groupId))
                {
                    throw new NotFoundException("Group not found");
                }

                studentGroups.Add(new StudentGroup { GroupId = groupId, StudentId = student.Id });
            }

            student.StudentGroups = studentGroups;

            await _studentRepository.CreateAsync(student);
        }

        public async Task EditAsync(int? id, StudentEditDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            ArgumentNullException.ThrowIfNull(nameof(data));

            var student = await _studentRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            _mapper.Map(data, student);
            await _studentRepository.EditAsync(student);
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var student = await _studentRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            await _studentRepository.DeleteAsync(student);
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.Find(source => source
                .Include(m => m.StudentGroups)
                .ThenInclude(m => m.Group)).ToListAsync();

            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var student = await _studentRepository.FindBy(m => m.Id == id, source => source
                .Include(m => m.StudentGroups)
                .ThenInclude(m => m.Group)).FirstOrDefaultAsync();

            return student is null ? throw new NotFoundException("Data not found") : _mapper.Map<StudentDto>(student);
        }

        public async Task<IEnumerable<StudentDto>> SearchAsync(string searchText)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchText));

            var students = await _studentRepository.FindBy(m => m.Name.Contains(searchText) || m.Surname.Contains(searchText), source => source
                    .Include(m => m.StudentGroups)
                    .ThenInclude(m => m.Group)).ToListAsync();

            return students.Count == 0 ? throw new NotFoundException("Data not found") : _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<IEnumerable<StudentDto>> FilterAsync(string? name, string? surname, int? age)
        {
            if (name is null && surname is null)
            {
                return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepository.FindBy(m => m.Age == age, source => source
                    .Include(m => m.StudentGroups)
                    .ThenInclude(m => m.Group)).ToListAsync());
            }

            if (name is null && age is null)
            {
                return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepository.FindBy(m => m.Surname.Contains(surname), source => source
                    .Include(m => m.StudentGroups)
                    .ThenInclude(m => m.Group)).ToListAsync());
            }

            if (surname is null && age is null)
            {
                return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepository.FindBy(m => m.Name.Contains(name), source => source
                    .Include(m => m.StudentGroups)
                    .ThenInclude(m => m.Group)).ToListAsync());
            }

            if (name is null)
            {
                return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepository.FindBy(m => m.Age == age && m.Surname.Contains(surname), source => source
                        .Include(m => m.StudentGroups)
                        .ThenInclude(m => m.Group)).ToListAsync());
            }

            if (surname is null)
            {
                return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepository.FindBy(m => m.Age == age && m.Name.Contains(name), source => source
                    .Include(m => m.StudentGroups)
                    .ThenInclude(m => m.Group)).ToListAsync());
            }

            if (age is null)
            {
                return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepository.FindBy(m => m.Name.Contains(name) && m.Surname.Contains(surname), source => source
                    .Include(m => m.StudentGroups)
                    .ThenInclude(m => m.Group)).ToListAsync());
            }

            if (name is null && surname is null && age is null)
            {
                throw new ArgumentNullException($"{nameof(name)}, {nameof(surname)}, {nameof(age)}");
            }

            throw new NotFoundException("Data not found");
        }

        public async Task AddGroupAsync(StudentAddDeleteGroupDto data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var student = await _studentRepository.FindBy(m => m.Id == data.StudentId, source => source
                .Include(m => m.StudentGroups)).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            if (student.StudentGroups.Any(m => m.GroupId == data.GroupId))
            {
                throw new BadRequestException("Student is already in this group");
            }

            var group = await _groupRepository.FindBy(m => m.Id == data.GroupId, source => source
                .Include(m => m.StudentGroups)).FirstOrDefaultAsync() ?? throw new NotFoundException("Group not found");

            if (group.StudentGroups.Count >= group.Capacity)
            {
                throw new BadRequestException("Group is full");
            }

            student.StudentGroups = new List<StudentGroup> { new()
                {
                    StudentId = data.StudentId,
                    GroupId = data.GroupId
                }
            };

            await _studentRepository.EditAsync(student);
        }

        public async Task DeleteGroupAsync(StudentAddDeleteGroupDto data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var studentGroup = await _studentGroupRepository
                .FindBy(m => m.StudentId == data.StudentId && m.GroupId == data.GroupId).FirstOrDefaultAsync();

            if (studentGroup is null) throw new NotFoundException("Data not found");

            await _studentGroupRepository.DeleteAsync(studentGroup);
        }
    }
}
