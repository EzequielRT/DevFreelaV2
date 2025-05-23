using DevFreela.Core.Entities;

namespace DevFreela.Application.Queries.Projects.GetById;

public record GetByIdResponse(
    long Id,
    string Title,
    string Description,
    long ClientId,
    string ClientFullName,
    long FreelancerId,
    string FreelancerFullName,
    decimal TotalCost,
    List<string> Comments)
{
    public static GetByIdResponse FromEntity(Project project)
        => new(
            project.Id,
            project.Title,
            project.Description,
            project.ClientId,
            project.Client.FullName,
            project.FreelancerId,
            project.Freelancer.FullName,
            project.TotalCost,
            project.Comments.Select(c => c.Content).ToList()
        );
}