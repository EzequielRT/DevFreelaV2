using DevFreela.Application.Models.View;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Payments;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Complete;

public class CompleteHandler : IRequestHandler<CompleteCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IPaymentService _paymentService;

    public CompleteHandler(IProjectRepository projectRepository, IPaymentService paymentService)
    {
        _projectRepository = projectRepository;
        _paymentService = paymentService;
    }

    public async Task<ResultViewModel> Handle(CompleteCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (project is null)
            return ResultViewModel.NotFound("Projeto não encontrado");

        var paymentModel = new PaymentModel
        (
            request.ProjectId,
            request.CreditCardNumber,
            request.Cvv,
            request.ExpiresAt,
            request.FullName,
            request.Amount
        );

        var result = await _paymentService.ProcessPayment(paymentModel);

        if (!result)
            project.SetPaymentPending();

        project.Complete();

        await _projectRepository.UpdateAsync(project);

        return ResultViewModel.Success();
    }
}