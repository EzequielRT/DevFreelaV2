using DevFreela.API.Models.Input;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserInputModel input)
        {
            return Ok();
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(UserSkillsInputModel input)
        {
            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public async Task<IActionResult> AddProfilePicture(IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            // Processar a imagem

            return Ok(description);
        }
    }
}
