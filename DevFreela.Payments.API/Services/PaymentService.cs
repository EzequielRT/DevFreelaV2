using DevFreela.Payments.API.Models;
using DevFreela.Payments.API.Repositories;

namespace DevFreela.Payments.API.Services;

public class PaymentService : IPaymentService
{    
    private readonly IProjectRepository _projectRepository;

    public PaymentService(IProjectRepository projectRepository)
    {        
        _projectRepository = projectRepository;
    }

    public async Task<bool> ProcessAsync(PaymentInfoInputModel model)
    {
        var project = await _projectRepository.GetByIdAsync(model.ProjectId);

        if (project is null)
            return false;

        project.SetPaymentPending();

        await _projectRepository.UpdateAsync(project);

        return await Task.FromResult(true);
    }
}
