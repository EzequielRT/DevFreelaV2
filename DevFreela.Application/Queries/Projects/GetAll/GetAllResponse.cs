using DevFreela.Core.Entities;

namespace DevFreela.Application.Queries.Projects.GetAll;

public class GetAllResponse
{
    public GetAllResponse(
        long id,
        string title,
        string clientName,
        string freelancerName,
        decimal totalCost)
    {
        Id = id;
        Title = title;
        ClientFullName = clientName;
        FreelancerFullName = freelancerName;
        TotalCost = totalCost;
    }

    public long Id { get; private set; }
    public string Title { get; private set; }
    public string ClientFullName { get; private set; }
    public string FreelancerFullName { get; private set; }
    public decimal TotalCost { get; private set; }

    public static GetAllResponse FromEntity(Project project)
    {
        return new(
            project.Id,
            project.Title,
            project.Client.FullName,
            project.Freelancer.FullName,
            project.TotalCost);
    }
}
