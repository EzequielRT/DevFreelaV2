using DevFreela.API.Models.Config;
using DevFreela.API.Models.Input;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers.v1
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelanceTotalCostConfig _options;
        private readonly IConfigService _configService;

        public ProjectsController(
            IOptions<FreelanceTotalCostConfig> options,
            IConfigService configService)
        {
            _options = options.Value;
            _configService = configService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search = "")
        {
            return Ok(_configService.GetValue());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProjectInputModel input)
        {
            if (input.TotalCost < _options.Minimum ||
                input.TotalCost > _options.Maximum)
                return BadRequest($"O valor deve estar entre {_options.Minimum} e {_options.Maximum}");

            return CreatedAtAction(nameof(GetById), new { id = 1 }, input);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateProjectInputModel input)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(CreateProjectCommentInputModel input)
        {
            return Ok();
        }
    }
}
