using DevFreela.Core.Entities;

namespace DevFreela.Application.Queries.Projects.GetById;

public class GetByIdResponse
{
    public GetByIdResponse(
        long id,
        string title,
        string description,
        long clientId,
        string clientName,
        long freelancerId,
        string freelancerName,
        decimal totalCost,
        List<ProjectComment> comments)
    {
        Id = id;
        Title = title;
        Description = description;
        ClientId = clientId;
        ClientFullName = clientName;
        FreelancerId = freelancerId;
        FreelancerFullName = freelancerName;
        TotalCost = totalCost;
        Comments = comments.Select(c => c.Content).ToList();
    }

    public long Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public long ClientId { get; private set; }
    public string ClientFullName { get; private set; }
    public long FreelancerId { get; private set; }
    public string FreelancerFullName { get; private set; }
    public decimal TotalCost { get; private set; }
    public List<string> Comments { get; private set; }

    public static GetByIdResponse FromEntity(Project project)
    {
        return new(
            project.Id,
            project.Title,
            project.Description,
            project.ClientId,
            project.Client.FullName,
            project.FreelancerId,
            project.Freelancer.FullName,
            project.TotalCost,
            project.Comments);
    }
}
