using DevFreela.Application.Models.Input;
using DevFreela.Application.Models.View;
using DevFreela.Infra.Auth;
using DevFreela.Infra.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers.v1;

[Route("api/v1/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly DevFreelaDbContext _context;
    private readonly IAuthService _authService;

    public UsersController(DevFreelaDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(CreateUserInputModel input)
    {
        var hash = await _authService.ComputeHashAsync(input.Password);
        input.Password = hash;

        var user = input.ToEntity();

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, input);
    }

    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginInputModel input)
    {
        var hash = await _authService.ComputeHashAsync(input.Password);

        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == input.Email && x.Password == hash);

        if (user is null)
        {
            var error = ResultViewModel<LoginViewModel>.Error("Email ou senha inválidos!");
            return BadRequest(error);
        }

        var token = await _authService.GenerateTokenAsync(user.Email, user.Role);
        var model = new LoginViewModel(token);
        var result = ResultViewModel.Success(token);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _context.Users
            .Include(u => u.Skills)
                .ThenInclude(u => u.Skill)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (user is null)
            return NotFound();

        var model = UserViewModel.FromEntity(user);

        return Ok(model);
    }

    [HttpPost("{id}/skills")]
    public async Task<IActionResult> PostSkills(long id, UserSkillsInputModel input)
    {
        input.SetUserId(id);

        var userSkills = input.ToEntities();

        await _context.UserSkills.AddRangeAsync(userSkills);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/profile-picture")]
    public async Task<IActionResult> AddProfilePicture(long id, IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";

        // Processar a imagem

        return Ok(description);
    }
}
