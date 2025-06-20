using MediatR;

namespace DevFreela.Application.Commands.Projects.Create.Notifications;

public class ProjectCreatedNotification : INotification
{
    public ProjectCreatedNotification(long id, string title, decimal totalCost)
    {
        Id = id;
        Title = title;
        TotalCost = totalCost;
    }

    public long Id { get; private set; }
    public string Title { get; private set; }
    public decimal TotalCost { get; private set; }
}
