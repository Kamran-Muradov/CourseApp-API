using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Teachers;
using Service.Services.Interfaces;

namespace CourseAPI.Controllers.Admin
{
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService, 
                                 ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll called");
            return Ok(await _teacherService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation("GetById called");
            return Ok(await _teacherService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            _logger.LogInformation("Search called");
            return Ok(await _teacherService.SearchAsync(searchText));
        }

        [HttpGet]
        public async Task<IActionResult> Sort([FromQuery] string sortKey, [FromQuery] bool isDescending)
        {
            _logger.LogInformation("Sort called");
            return Ok(await _teacherService.SortAsync(sortKey, isDescending));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherCreateDto request)
        {
            await _teacherService.CreateAsync(request);
            _logger.LogInformation("Data successfully created");
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] TeacherEditDto request)
        {
            await _teacherService.EditAsync(id, request);
            _logger.LogInformation("Data successfully edited");
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _teacherService.DeleteAsync(id);
            _logger.LogInformation("Data successfully deleted");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup([FromBody] TeacherAddDeleteGroupDto request)
        {
            await _teacherService.AddGroupAsync(request);
            _logger.LogInformation("Group successfully added");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup([FromBody] TeacherAddDeleteGroupDto request)
        {
            await _teacherService.DeleteGroupAsync(request);
            _logger.LogInformation("Group successfully deleted");
            return Ok();
        }
    }
}
