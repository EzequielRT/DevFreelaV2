using DevFreela.Core.Entities;

namespace DevFreela.Application.Queries.Projects.GetAll;

public record GetAllResponse(
    long Id,
    string Title,
    string ClientFullName,
    string FreelancerFullName,
    decimal TotalCost)
{
    public static GetAllResponse FromEntity(Project project) =>
        new(
            project.Id,
            project.Title,
            project.Client.FullName,
            project.Freelancer.FullName,
            project.TotalCost
        );
}
