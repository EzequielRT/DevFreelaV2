using DevFreela.API.Entities;

namespace DevFreela.API.Models.View
{
    public class ProjectItemViewModel
    {
        public ProjectItemViewModel(
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

        public long Id { get; set; }
        public string Title { get; set; }
        public string ClientFullName { get; set; }
        public string FreelancerFullName { get; set; }
        public decimal TotalCost { get; set; }

        public static ProjectItemViewModel FromEntity(Project project)
        {
            return new (
                project.Id,
                project.Title,
                project.Client.FullName,
                project.Freelancer.FullName,
                project.TotalCost);
        }
    }
}
