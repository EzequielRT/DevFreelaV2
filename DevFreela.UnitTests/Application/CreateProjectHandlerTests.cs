using DevFreela.Application.Commands.Projects.Create;
using DevFreela.Application.Settings;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class CreateProjectHandlerTests
{
    [Fact]
    public async Task InputDataAreOk_Insert_Success_NSubstitute()
    {
        // Arrange
        const long ID = 1;

        var mediator = Substitute.For<IMediator>();

        var options = Substitute.For<IOptions<FreelanceTotalCostSettings>>();
        options.Value.Returns(new FreelanceTotalCostSettings { Minimum = 1000, Maximum = 99999 });
                
        var validator = Substitute.For<IValidator<CreateCommand>>();
        validator
            .Validate(Arg.Any<CreateCommand>())
            .Returns(new ValidationResult());

        var repository = Substitute.For<IProjectRepository>();
        repository
            .AddAsync(Arg.Do<Project>(p => p.SetId(ID)))
            .Returns(ID);

        var command = new CreateCommand(
            Title: "Projeto A",
            Description: "Descrição do Projeto",
            ClientId: 1,
            FreelancerId: 2,
            TotalCost: 2500
        );
        
        var handler = new CreateHandler(
            repository,
            options,
            mediator,
            validator
        );

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data.Id);
        await repository.Received(1).AddAsync(Arg.Any<Project>());
    }
}
