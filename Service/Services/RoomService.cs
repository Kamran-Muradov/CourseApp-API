using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Rooms;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(RoomCreateDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(data));

            if (await _roomRepository.ExistWithNameAsync(data.Name))
            {
                throw new BadRequestException("Room with this name already exists");
            }

            await _roomRepository.CreateAsync(_mapper.Map<Room>(data));
        }

        public async Task EditAsync(int? id, RoomEditDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            ArgumentNullException.ThrowIfNull(nameof(data));

            var room = await _roomRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            if (await _roomRepository.ExistWithNameExceptIdAsync((int)id, data.Name))
            {
                throw new BadRequestException("Room with this name already exists");
            }

            _mapper.Map(data, room);
            await _roomRepository.EditAsync(room);
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var room = await _roomRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            await _roomRepository.DeleteAsync(room);
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            var rooms = await _roomRepository.Find(source => source
                .Include(m => m.Groups)).ToListAsync();

            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var room = await _roomRepository.FindBy(m => m.Id == id, source => source
                .Include(m => m.Groups)).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<IEnumerable<RoomDto>> SearchAsync(string searchText)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchText));

            var rooms = await _roomRepository.FindBy(m => m.Name.Contains(searchText), source => source
                .Include(m => m.Groups)).ToListAsync();

            if (rooms.Count == 0)
            {
                throw new NotFoundException("Data not found");
            }

            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<IEnumerable<RoomDto>> SortAsync(string sortKey, bool? isDescending)
        {
            ArgumentNullException.ThrowIfNull(nameof(sortKey));
            ArgumentNullException.ThrowIfNull(nameof(isDescending));

            var rooms = await _roomRepository.Find(source => source
                .Include(m => m.Groups)).ToListAsync();

            switch (sortKey)
            {
                case "Name":
                    if ((bool)isDescending)
                    {
                        return _mapper.Map<IEnumerable<RoomDto>>(rooms.OrderByDescending(m => m.Name));
                    }
                    return _mapper.Map<IEnumerable<RoomDto>>(rooms.OrderBy(m => m.Name));

                case "SeatCount":
                    if ((bool)isDescending)
                    {
                        return _mapper.Map<IEnumerable<RoomDto>>(rooms.OrderByDescending(m => m.SeatCount));
                    }
                    return _mapper.Map<IEnumerable<RoomDto>>(rooms.OrderBy(m => m.SeatCount));

                default:
                    return _mapper.Map<IEnumerable<RoomDto>>(rooms);
            }
        }
    }
}
