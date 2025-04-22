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

        [HttpPut("{id}/profile-picture")]
        public async Task<IActionResult> AddProfilePicture(IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            // Processar a imagem

            return Ok(description);
        }
    }
}
