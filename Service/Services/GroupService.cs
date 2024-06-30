using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Groups;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository,
                            IMapper mapper, 
                            IEducationRepository educationRepository, 
                            IRoomRepository roomRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _educationRepository = educationRepository;
            _roomRepository = roomRepository;
        }

        public async Task CreateAsync(GroupCreateDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(data));

            if (await _groupRepository.ExistWithNameAsync(data.Name))
            {
                throw new BadRequestException("Group with this name already exists");
            }

            if (!await _educationRepository.ExistWithIdAsync(data.EducationId)) throw new NotFoundException("Education not found");
            if (!await _roomRepository.ExistWithIdAsync(data.RoomId)) throw new NotFoundException("Room not found");

            await _groupRepository.CreateAsync(_mapper.Map<Group>(data));
        }

        public async Task EditAsync(int? id, GroupEditDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            ArgumentNullException.ThrowIfNull(nameof(data));

            var group = await _groupRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            if (await _groupRepository.ExistWithNameExceptIdAsync((int)id, data.Name))
            {
                throw new BadRequestException("Group with this name already exists");
            }

            if (!await _educationRepository.ExistWithIdAsync(data.EducationId)) throw new NotFoundException("Education not found");
            if (!await _roomRepository.ExistWithIdAsync(data.RoomId)) throw new NotFoundException("Room not found");

            _mapper.Map(data, group);
            await _groupRepository.EditAsync(group);
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var group = await _groupRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            await _groupRepository.DeleteAsync(group);
        }

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            var groups = await _groupRepository.Find(source => source
                    .Include(m => m.StudentGroups)
                    .Include(m => m.GroupTeachers)
                    .ThenInclude(m => m.Teacher)
                    .Include(m => m.Room)
                    .Include(m => m.Education)).ToListAsync();

            return _mapper.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<GroupDto> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var group = await _groupRepository.FindBy(m => m.Id == id, source => source
                    .Include(m => m.StudentGroups)
                    .Include(m => m.GroupTeachers)
                    .ThenInclude(m => m.Teacher)
                    .Include(m => m.Room)
                    .Include(m => m.Education)).FirstOrDefaultAsync();

            return group is null ? throw new NotFoundException("Data not found") : _mapper.Map<GroupDto>(group);
        }

        public async Task<IEnumerable<GroupDto>> SearchByNameAsync(string name)
        {
            ArgumentNullException.ThrowIfNull(nameof(name));

            var groups = await _groupRepository.FindBy(m => m.Name.Contains(name), source => source
                    .Include(m => m.GroupTeachers)
                    .ThenInclude(m => m.Teacher)
                    .Include(m => m.StudentGroups)
                    .Include(m => m.Room)
                    .Include(m => m.Education)).ToListAsync();

            return groups.Count == 0 ? throw new NotFoundException("Data not found") : _mapper.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<IEnumerable<GroupDto>> SortAsync(string sortKey, bool? isDescending = false)
        {
            ArgumentNullException.ThrowIfNull(nameof(sortKey));
            ArgumentNullException.ThrowIfNull(nameof(isDescending));

            var groups = await _groupRepository.Find(source => source
                .Include(m => m.StudentGroups)
                .Include(m => m.GroupTeachers)
                .ThenInclude(m => m.Teacher)
                .Include(m => m.Room)
                .Include(m => m.Education)).ToListAsync();

            switch (sortKey)
            {
                case "Name":
                    if ((bool)isDescending)
                    {
                        return _mapper.Map<IEnumerable<GroupDto>>(groups.OrderByDescending(m => m.Name));
                    }
                    return _mapper.Map<IEnumerable<GroupDto>>(groups.OrderBy(m => m.Name));

                case "Capacity":
                    if ((bool)isDescending)
                    {
                        return _mapper.Map<IEnumerable<GroupDto>>(groups.OrderByDescending(m => m.Capacity));
                    }
                    return _mapper.Map<IEnumerable<GroupDto>>(groups.OrderBy(m => m.Capacity));

                default:
                    return _mapper.Map<IEnumerable<GroupDto>>(groups);
            }
        }
    }
}
