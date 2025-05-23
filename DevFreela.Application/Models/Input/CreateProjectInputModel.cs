using DevFreela.Core.Entities;

namespace DevFreela.Application.Models.Input;

public class CreateProjectInputModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public long ClientId { get; set; }        
    public long FreelancerId { get; set; }
    public decimal TotalCost { get; set; }

    public Project ToEntity()
        => new(Title, Description, ClientId, FreelancerId, TotalCost);
}
