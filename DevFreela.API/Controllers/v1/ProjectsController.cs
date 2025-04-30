using DevFreela.API.Configs;
using DevFreela.Application.Models.Input;
using DevFreela.Application.Models.View;
using DevFreela.Infra.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers.v1
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelanceTotalCostConfig _options;
        private readonly DevFreelaDbContext _context;

        public ProjectsController(
            IOptions<FreelanceTotalCostConfig> options,
            DevFreelaDbContext context)
        {
            _options = options.Value;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? search = null, int page = 0, int size = 10)
        {
            var query = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => p.DeletedAt == null)
                .Skip(page * size)
                .Take(size)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query
                    .Where(p => p.Title.Contains(search) ||
                                p.Description.Contains(search));
            }

            var projects = await query
                .ToListAsync();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project is null)
                return NotFound();

            var model = ProjectViewModel.FromEntity(project);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProjectInputModel input)
        {
            if (input.TotalCost < _options.Minimum ||
                input.TotalCost > _options.Maximum)
                return BadRequest($"O valor deve estar entre {_options.Minimum} e {_options.Maximum}");

            var project = input.ToEntity();

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, input);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, UpdateProjectInputModel input)
        {
            input.SetProjectId(id);

            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == input.ProjectId);

            if (project == null)
                return NotFound();

            project.Update(input.Title, input.Description, input.TotalCost);

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            project.SetAsDeleted();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(long id)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            project.Start();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(long id)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            project.Complete();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(long id, CreateProjectCommentInputModel input)
        {
            input.SetProjectId(id);

            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == input.ProjectId);

            if (project == null)
                return NotFound();

            var comment = input.ToEntity();

            await _context.ProjectComments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
