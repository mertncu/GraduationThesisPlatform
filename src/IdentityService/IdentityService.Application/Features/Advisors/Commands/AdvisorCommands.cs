using IdentityService.Application.Features.Advisors.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Advisors.Commands;

public record UpdateAdvisorCommand(UpdateAdvisorDto Advisor) : IRequest;

public class UpdateAdvisorCommandValidator : AbstractValidator<UpdateAdvisorCommand>
{
    public UpdateAdvisorCommandValidator()
    {
        RuleFor(x => x.Advisor.Id).NotEmpty();
        RuleFor(x => x.Advisor.DepartmentId).NotEmpty();
        RuleFor(x => x.Advisor.MaxQuota).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Advisor.CurrentQuota).GreaterThanOrEqualTo(0);
    }
}

public class UpdateAdvisorCommandHandler : IRequestHandler<UpdateAdvisorCommand>
{
    private readonly IAdvisorRepository _advisorRepository;

    public UpdateAdvisorCommandHandler(IAdvisorRepository advisorRepository)
    {
        _advisorRepository = advisorRepository;
    }

    public async Task Handle(UpdateAdvisorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _advisorRepository.GetByIdAsync(request.Advisor.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Advisor with ID {request.Advisor.Id} not found.");

        request.Advisor.Adapt(entity);
        await _advisorRepository.UpdateAsync(entity, cancellationToken);
    }
}

public record DeleteAdvisorCommand(Guid Id) : IRequest;

public class DeleteAdvisorCommandHandler : IRequestHandler<DeleteAdvisorCommand>
{
    private readonly IAdvisorRepository _advisorRepository;

    public DeleteAdvisorCommandHandler(IAdvisorRepository advisorRepository)
    {
        _advisorRepository = advisorRepository;
    }

    public async Task Handle(DeleteAdvisorCommand request, CancellationToken cancellationToken)
    {
        if (!await _advisorRepository.ExistsAsync(request.Id, cancellationToken))
            throw new KeyNotFoundException($"Advisor with ID {request.Id} not found.");
            
        await _advisorRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
