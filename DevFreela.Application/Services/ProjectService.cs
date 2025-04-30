using DevFreela.Application.Models.Input;
using DevFreela.Application.Models.View;
using DevFreela.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public interface IProjectService
    {
        Task<ResultViewModel<List<ProjectItemViewModel>>> GetAllAsync(string? search = null, int page = 0, int size = 10);
        Task<ResultViewModel<ProjectViewModel>> GetByIdAsync(long id);
        Task<ResultViewModel<long>> InsertAsync(CreateProjectInputModel model);
        Task<ResultViewModel> UpdateAsync(UpdateProjectInputModel model);
        Task<ResultViewModel> DeleteAsync(long id);
        Task<ResultViewModel> StartAsync(long id);
        Task<ResultViewModel> CompleteAsync(long id);        
        Task<ResultViewModel> InsertCommentAsync(CreateProjectCommentInputModel model);
    }

    public class ProjectService : IProjectService
    {        
        private readonly DevFreelaDbContext _context;

        public ProjectService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> GetAllAsync(string? search = null, int page = 0, int size = 10)
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

            var queryResult = await query.ToListAsync();

            var model = queryResult.Select(ProjectItemViewModel.FromEntity).ToList();

            return new ResultViewModel<List<ProjectItemViewModel>>(model);
        }

        public async Task<ResultViewModel<ProjectViewModel>> GetByIdAsync(long id)
        {
            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project is null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            var model = ProjectViewModel.FromEntity(project);

            return new ResultViewModel<ProjectViewModel>(model);
        }

        public async Task<ResultViewModel<long>> InsertAsync(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return new ResultViewModel<long>(project.Id);
        }

        public async Task<ResultViewModel> UpdateAsync(UpdateProjectInputModel model)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == model.ProjectId);

            if (project == null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
        
        public async Task<ResultViewModel> DeleteAsync(long id)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            project.SetAsDeleted();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }

        public async Task<ResultViewModel> StartAsync(long id)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            project.Start();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }

        public async Task<ResultViewModel> CompleteAsync(long id)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            project.Complete();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }

        public async Task<ResultViewModel> InsertCommentAsync(CreateProjectCommentInputModel model)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == model.ProjectId);

            if (project == null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            var comment = model.ToEntity();

            await _context.ProjectComments.AddAsync(comment);
            await _context.SaveChangesAsync();
            
            return ResultViewModel.Success();
        }
    }
}
