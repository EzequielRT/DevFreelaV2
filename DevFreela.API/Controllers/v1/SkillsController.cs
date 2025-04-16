using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v1
{
    [Route("api/v1/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok();
        }
    }
}
