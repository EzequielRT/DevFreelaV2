using DevFreela.Core.Enums;
using DevFreela.Core.Exceptions;

namespace DevFreela.Core.Entities;

public class Project : BaseEntity
{
    // EF Constructor
    protected Project() { }

    public Project(string title, string description, long clientId, long freelancerId, decimal totalCost)
    {
        Title = title;
        Description = description;
        ClientId = clientId;
        FreelancerId = freelancerId;
        TotalCost = totalCost;

        Status = ProjectStatusEnum.Created;
        Comments = new List<ProjectComment>();
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public long ClientId { get; private set; }
    public User Client { get; private set; }
    public long FreelancerId { get; private set; }
    public User Freelancer { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

    public void Cancel()
    {
        if (Status == ProjectStatusEnum.InProgress ||
            Status == ProjectStatusEnum.Suspended)
            throw new DomainException($"Status atual não permite o cancelamento do projeto. Status: {Status}");

        Status = ProjectStatusEnum.Canceled;
    }

    public void Start()
    {
        if (Status != ProjectStatusEnum.Created)
            throw new DomainException($"Status atual não permite o início do projeto. Status: {Status}");

        StartedAt = DateTime.Now;
        Status = ProjectStatusEnum.InProgress;
    }     

    public void SetPaymentPending()
    {
        if (Status != ProjectStatusEnum.InProgress)
            throw new DomainException($"Status atual não permite o pagamento. Status: {Status}");

        Status = ProjectStatusEnum.PaymentPending;
    }

    public void Complete()
    {
        if (Status != ProjectStatusEnum.PaymentPending)
            throw new DomainException($"Status atual não permite a conclusão do projeto. Status: {Status}");

        CompletedAt = DateTime.Now;
        Status = ProjectStatusEnum.Completed;
    }

    public void Update(string title, string description, decimal totalCost)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
    }
}
