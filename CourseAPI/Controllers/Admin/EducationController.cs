using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Educations;
using Service.Services.Interfaces;

namespace CourseAPI.Controllers.Admin
{
    public class EducationController : BaseController
    {
        private readonly IEducationService _educationService;
        private readonly ILogger<EducationController> _logger;

        public EducationController(IEducationService educationService,
                                   ILogger<EducationController> logger)
        {
            _educationService = educationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all called");
            return Ok(await _educationService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation("GetById called");
            return Ok(await _educationService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Sort([FromQuery] string sortKey, [FromQuery] bool isDescending)
        {
            _logger.LogInformation("Sort called");
            return Ok(await _educationService.SortAsync(sortKey, isDescending));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            _logger.LogInformation("Search called");
            return Ok(await _educationService.SearchAsync(name));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EducationCreateDto request)
        {
            await _educationService.CreateAsync(request);
            _logger.LogInformation("Data successfully created");
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EducationEditDto request)
        {
            await _educationService.EditAsync(id, request);
            _logger.LogInformation("Data successfully edited");
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _educationService.DeleteAsync(id);
            _logger.LogInformation("Data successfully deleted");
            return Ok();
        }
    }
}
