using FluentValidation;

namespace DevFreela.Application.Commands.Projects.Create;

public class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
                .WithMessage("O título não pode ser vazio.")
            .MaximumLength(50)
                .WithMessage("O tamanho máximo do título é 50 caracteres.");

        RuleFor(p => p.TotalCost)
            .GreaterThanOrEqualTo(1000)
                .WithMessage("O projeto deve custar pelo menos R$ 1000,00");
    }
}
