using DevFreela.Application.Models.View;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
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

        await _paymentService.ProcessPaymentAsync
        (
            request.ProjectId,
            request.CreditCardNumber,
            request.Cvv,
            request.ExpiresAt,
            request.FullName,
            request.Amount
        );
        
        return ResultViewModel.Success();
    }
}