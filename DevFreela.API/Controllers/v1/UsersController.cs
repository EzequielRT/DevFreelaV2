using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok();
        }
    }
}
