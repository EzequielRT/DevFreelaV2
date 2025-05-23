using DevFreela.API.Configs;
using DevFreela.Application.Models.Input;
using DevFreela.Application.Models.View;
using DevFreela.Application.Queries.Projects.GetAll;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers.v1
{
    public class ProjectsController : BaseApiController
    {
        private readonly FreelanceTotalCostConfig _options;
        private readonly IProjectService _projectService;

        public ProjectsController(
            IOptions<FreelanceTotalCostConfig> options,
            IProjectService projectService,
            IMediator mediator) : base(mediator)
        {
            _options = options.Value;
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllQuery query, CancellationToken cancellationToken)
        {
            return await SendAsync(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _projectService.GetByIdAsync(id);
            
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProjectInputModel input)
        {
            if (input.TotalCost < _options.Minimum ||
                input.TotalCost > _options.Maximum)
                return BadRequest($"O valor deve estar entre {_options.Minimum} e {_options.Maximum}");
            
            var result = await _projectService.InsertAsync(input);

            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, UpdateProjectInputModel input)
        {
            input.SetProjectId(id);

            var result = await _projectService.UpdateAsync(input);

            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _projectService.DeleteAsync(id);

            return result.ToActionResult();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(long id)
        {
            var result = await _projectService.StartAsync(id);

            return result.ToActionResult();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(long id)
        {
            var result = await _projectService.CompleteAsync(id);

            return result.ToActionResult();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(long id, CreateProjectCommentInputModel input)
        {
            input.SetProjectId(id);

            var result = await _projectService.InsertCommentAsync(input);

            return result.ToActionResult();
        }
    }
}
