using DevFreela.Application.Commands.Projects.Delete;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class DeleteProjectHandlerTests
{
    [Fact]
    public async Task ProjectExists_Delete_Success_NSubstitute()
    {
        // Arrange
        const long ID = 1;

        var project = new Project(
            title: "Projeto A",
            description: "Descrição do Projeto",
            clientId: 1,
            freelancerId: 2,
            totalCost: 2500
        );

        var repository = Substitute.For<IProjectRepository>();
        repository
            .GetByIdAsync(ID)
            .Returns(project);

        repository
            .UpdateAsync(project)
            .Returns(Task.CompletedTask);

        var command = new DeleteCommand(ID);
        
        var handler = new DeleteHandler(repository);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);
        await repository.Received(1).GetByIdAsync(ID);
        await repository.Received(1).UpdateAsync(project);
    }

    [Fact]
    public async Task ProjectDoesNotExists_Delete_Error_NSubstitute()
    {
        // Arrange
        const int ID = 1;

        var repository = Substitute.For<IProjectRepository>();
        repository
            .GetByIdAsync(ID)
            .Returns(Task.FromResult((Project?)null));

        var command = new DeleteCommand(ID);
        
        var handler = new DeleteHandler(repository);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);

        await repository.Received(1).GetByIdAsync(ID);
        await repository.DidNotReceive().UpdateAsync(Arg.Any<Project>());
    }
}
