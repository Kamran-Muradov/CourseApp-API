using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Students;
using Service.Services.Interfaces;

namespace CourseAPI.Controllers.Admin
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, 
                                 ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll called");
            return Ok(await _studentService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation("GetById called");
            return Ok(await _studentService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            _logger.LogInformation("Search called");
            return Ok(await _studentService.SearchAsync(searchText));
        }

        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] string name, [FromQuery] string surname, [FromQuery] int? age)
        {
            _logger.LogInformation("Filter called");
            return Ok(await _studentService.FilterAsync(name, surname, age));
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateDto request)
        {
            await _studentService.CreateAsync(request);
            _logger.LogInformation("Data successfully created");
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] StudentEditDto request)
        {
            await _studentService.EditAsync(id, request);
            _logger.LogInformation("Data successfully edited");
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _studentService.DeleteAsync(id);
            _logger.LogInformation("Data successfully deleted");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup([FromBody] StudentAddDeleteGroupDto request)
        {
            await _studentService.AddGroupAsync(request);
            _logger.LogInformation("Group successfully added");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup([FromBody] StudentAddDeleteGroupDto request)
        {
            await _studentService.DeleteGroupAsync(request);
            _logger.LogInformation("Group successfully deleted");
            return Ok();
        }
    }
}
