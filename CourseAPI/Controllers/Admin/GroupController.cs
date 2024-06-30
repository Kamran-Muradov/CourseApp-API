using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Groups;
using Service.Services.Interfaces;

namespace CourseAPI.Controllers.Admin
{
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IGroupService groupService, 
                               ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll called");
            return Ok(await _groupService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation("GetById called");
            return Ok(await _groupService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            _logger.LogInformation("Search called");
            return Ok(await _groupService.SearchByNameAsync(name));
        }

        [HttpGet]
        public async Task<IActionResult> Sort([FromQuery] string sortKey, [FromQuery] bool isDescending)
        {
            _logger.LogInformation("Sort called");
            return Ok(await _groupService.SortAsync(sortKey, isDescending));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupCreateDto request)
        {
            await _groupService.CreateAsync(request);
            _logger.LogInformation("Data successfully created");
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] GroupEditDto request)
        {
            await _groupService.EditAsync(id, request);
            _logger.LogInformation("Data successfully edited");
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _groupService.DeleteAsync(id);
            _logger.LogInformation("Data successfully deleted");
            return Ok();
        }
    }
}
