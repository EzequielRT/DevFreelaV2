using DevFreela.API.Entities;

namespace DevFreela.API.Models.View
{
    public class ProjectViewModel
    {
        public ProjectViewModel(
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

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long ClientId { get; set; }
        public string ClientFullName { get; set; }
        public long FreelancerId { get; set; }
        public string FreelancerFullName { get; set; }
        public decimal TotalCost { get; set; }
        public List<string> Comments { get; set; }

        public static ProjectViewModel FromEntity(Project project)
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
}
