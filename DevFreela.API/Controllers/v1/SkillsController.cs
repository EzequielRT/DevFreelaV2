using DevFreela.API.Entities;
using DevFreela.API.Models.Input;
using DevFreela.API.Models.View;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers.v1
{
    [Route("api/v1/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;

        public SkillsController(DevFreelaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _context.Skills.ToListAsync();

            var model = skills.Select(SkillItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSkillInputModel input)
        {
            var skill = new Skill(input.Description);

            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
