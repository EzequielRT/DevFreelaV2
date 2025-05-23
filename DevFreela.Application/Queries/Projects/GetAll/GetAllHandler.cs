using DevFreela.Application.Models.View;
using DevFreela.Infra.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.Projects.GetAll
{
    public class GetAllHandler : IRequestHandler<GetAllQuery, ResultViewModel<List<GetAllResponse>>>
    {
        private readonly DevFreelaDbContext _context;

        public GetAllHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<GetAllResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
             var query = _context.Projects
                .AsNoTracking()
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => p.DeletedAt == null)
                .OrderBy(p => p.Title)
                .Skip(request.Page * request.Size)
                .Take(request.Size)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query
                    .Where(p => p.Title.Contains(request.Search) ||
                                p.Description.Contains(request.Search));
            }

            var queryResult = await query.ToListAsync(cancellationToken);

            var model = queryResult.Select(GetAllResponse.FromEntity).ToList();

            return ResultViewModel<List<GetAllResponse>>.Success(model);
        }
    }
}
