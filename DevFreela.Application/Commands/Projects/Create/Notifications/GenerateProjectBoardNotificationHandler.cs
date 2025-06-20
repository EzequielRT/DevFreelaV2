using MediatR;

namespace DevFreela.Application.Commands.Projects.Create.Notifications;

public class GenerateProjectBoardNotificationHandler : INotificationHandler<ProjectCreatedNotification>
{
    public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Criando painel para o projeto {notification.Title}");

        return Task.CompletedTask;
    }
}
