using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Exceptions;
using FluentAssertions;

namespace DevFreela.UnitTests.Core;

public class ProjectTests
{
    [Fact]
    public void ProjectIsCreated_Start_Success()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição do Projeto", 1, 2, 2000);

        // Act
        project.Start();

        // Assert
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        project.Status.Should().Be(ProjectStatusEnum.InProgress);

        Assert.NotNull(project.StartedAt);
        project.StartedAt.Should().NotBeNull();
    }

    [Fact]
    public void ProjectIsInInvalidState_Start_ThrowsException()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição do Projeto", 1, 2, 2000);
        project.Start();

        // Act
        Action? start = project.Start;

        // Assert
        var exception = Assert.Throws<DomainException>(start);
        Assert.Contains(string.Format(Project.INVALID_STATE_MESSAGE, ProjectStatusEnum.InProgress), exception.Message);

        start.Should()
            .Throw<DomainException>()
            .WithMessage(string.Format(Project.INVALID_STATE_MESSAGE, ProjectStatusEnum.InProgress));
    }
}
