using DevFreela.API.Entities;

namespace DevFreela.API.Models.Input
{
    public class CreateProjectInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }        
        public int FreelancerId { get; set; }
        public decimal TotalCost { get; set; }

        public Project ToEntity()
            => new(Title, Description, ClientId, FreelancerId, TotalCost);
    }
}
