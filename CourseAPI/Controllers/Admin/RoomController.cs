using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Rooms;
using Service.Services.Interfaces;

namespace CourseAPI.Controllers.Admin
{
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomService roomService, 
                              ILogger<RoomController> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll called");
            return Ok(await _roomService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation("GetById called");
            return Ok(await _roomService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Sort([FromQuery] string sortKey, [FromQuery] bool isDescending)
        {
            _logger.LogInformation("Sort called");
            return Ok(await _roomService.SortAsync(sortKey, isDescending));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            _logger.LogInformation("Search called");
            return Ok(await _roomService.SearchAsync(name));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomCreateDto request)
        {
            await _roomService.CreateAsync(request);
            _logger.LogInformation("Data successfully created");
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] RoomEditDto request)
        {
            await _roomService.EditAsync(id, request);
            _logger.LogInformation("Data successfully edited");
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _roomService.DeleteAsync(id);
            _logger.LogInformation("Data successfully deleted");
            return Ok();
        }
    }
}
