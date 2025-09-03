using DevFreela.Core.Entities;

namespace DevFreela.Application.Queries.Projects.GetAll;

public record GetAllResponse(int Page, int Size, int Count, IEnumerable<GetAllItemResponse> Projects);

public record GetAllItemResponse(
    long Id,
    string Title,
    string ClientFullName,
    string FreelancerFullName,
    decimal TotalCost)
{
    public static GetAllItemResponse FromEntity(Project project) =>
        new(
            project.Id,
            project.Title,
            project.Client.FullName,
            project.Freelancer.FullName,
            project.TotalCost
        );
}
