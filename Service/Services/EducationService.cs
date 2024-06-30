using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Educations;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;

        public EducationService(IEducationRepository educationRepository,
                                IMapper mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(EducationCreateDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(data));

            if (await _educationRepository.ExistWithNameAsync(data.Name))
            {
                throw new BadRequestException("Education with this name already exists");
            }

            await _educationRepository.CreateAsync(_mapper.Map<Education>(data));
        }

        public async Task EditAsync(int? id, EducationEditDto data)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            ArgumentNullException.ThrowIfNull(nameof(data));

            var education = await _educationRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            if (await _educationRepository.ExistWithNameExceptIdAsync((int)id, data.Name))
            {
                throw new BadRequestException("Education with this name already exists");
            }

            _mapper.Map(data, education);
            await _educationRepository.EditAsync(education);
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var education = await _educationRepository.FindBy(m => m.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            await _educationRepository.DeleteAsync(education);
        }

        public async Task<IEnumerable<EducationDto>> GetAllAsync()
        {
            var educations = await _educationRepository.Find(source => source
                    .Include(m => m.Groups)).ToListAsync();

            return _mapper.Map<IEnumerable<EducationDto>>(educations);
        }

        public async Task<EducationDto> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var education = await _educationRepository.FindBy(m => m.Id == id, source => source
                .Include(m => m.Groups)).FirstOrDefaultAsync() ?? throw new NotFoundException("Data not found");

            return _mapper.Map<EducationDto>(education);
        }

        public async Task<IEnumerable<EducationDto>> SearchAsync(string searchText)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchText));

            var educations = await _educationRepository.FindBy(m => m.Name.Contains(searchText), source => source
                .Include(m => m.Groups)).ToListAsync();

            if (educations.Count == 0)
            {
                throw new NotFoundException("Data not found");
            }

            return _mapper.Map<IEnumerable<EducationDto>>(educations);
        }

        public async Task<IEnumerable<EducationDto>> SortAsync(string sortKey, bool? isDescending = false)
        {
            ArgumentNullException.ThrowIfNull(nameof(sortKey));
            ArgumentNullException.ThrowIfNull(nameof(isDescending));

            var educations = await _educationRepository.Find(source => source
                .Include(m => m.Groups)).ToListAsync();

            if (sortKey != "Name") return _mapper.Map<IEnumerable<EducationDto>>(educations);

            if ((bool)isDescending)
            {
                return _mapper.Map<IEnumerable<EducationDto>>(educations.OrderByDescending(m => m.Name));
            }

            return _mapper.Map<IEnumerable<EducationDto>>(educations.OrderBy(m => m.Name));
        }
    }
}
