using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Exceptions;
using DevFreela.UnitTests.Fakes;
using FluentAssertions;

namespace DevFreela.UnitTests.Core;

public class ProjectTests
{
    [Fact]
    public void ProjectIsCreated_Start_Success()
    {
        // Arrange
        var project = FakeDataHelper.CreateFakeProject();

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
        var project = FakeDataHelper.CreateFakeProject();
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
