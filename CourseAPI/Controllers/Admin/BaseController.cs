using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
