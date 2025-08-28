using DevFreela.Payments.API.Enums;
using DevFreela.Payments.API.Exceptions;

namespace DevFreela.Payments.API.Entities;

public class Project : BaseEntity
{    
    public const string INVALID_STATE_MESSAGE =
        "O status atual não permite esta operação. Status: {0}";

    // EF Constructor
    protected Project() { }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public long ClientId { get; private set; }
    public long FreelancerId { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; }

    public void SetPaymentPending()
    {
        if (Status != ProjectStatusEnum.InProgress)
            throw new DomainException(string.Format(INVALID_STATE_MESSAGE, Status));

        Status = ProjectStatusEnum.PaymentPending;
    }
}
