using Bogus;
using DevFreela.Application.Commands.Projects.Create;
using DevFreela.Application.Commands.Projects.Delete;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Fakes;

public class FakeDataHelper
{
    private static readonly Faker _faker = new();

    public static Project CreateFakeProjectV1()
        => new Project
            (
                _faker.Commerce.ProductName(),
                _faker.Lorem.Sentence(),
                _faker.Random.Int(1, 100),
                _faker.Random.Int(1, 100),
                _faker.Random.Decimal(1000, 10000)
            );

    private static readonly Faker<Project> _projectFaker = new Faker<Project>()
        .CustomInstantiator(f => new Project(
            f.Commerce.ProductName(),
            f.Lorem.Sentence(),
            f.Random.Int(1, 100),
            f.Random.Int(1, 100),
            f.Random.Decimal(1000, 10000)
        ));

    private static readonly Faker<CreateCommand> _createCommandFaker = new Faker<CreateCommand>()
        .RuleFor(c => c.Title, f => f.Commerce.ProductName())
        .RuleFor(c => c.Description, f => f.Lorem.Sentence())
        .RuleFor(c => c.FreelancerId, f => f.Random.Int(1, 100))
        .RuleFor(c => c.ClientId, f => f.Random.Int(1, 100))
        .RuleFor(c => c.TotalCost, f => f.Random.Decimal(1000, 10000));

    private static readonly Faker<CreateCommand> _createCommandFakerV2 = new Faker<CreateCommand>()
        .CustomInstantiator(f => new CreateCommand(
            f.Commerce.ProductName(),
            f.Lorem.Sentence(),
            f.Random.Int(1, 100),
            f.Random.Int(1, 100),
            f.Random.Decimal(1000, 10000)
        ));

    public static Project CreateFakeProject()
        => _projectFaker.Generate();

    public static List<Project> CreateFakeProjectList()
        => _projectFaker.Generate(5);

    public static DeleteCommand CreateFakeDeleteCommand(long id)
       => new(id);

    public static CreateCommand CreateFakeCreateCommand()
       => _createCommandFaker.Generate();

    public static CreateCommand CreateFakeCreateCommandV2()
       => _createCommandFakerV2.Generate();
}