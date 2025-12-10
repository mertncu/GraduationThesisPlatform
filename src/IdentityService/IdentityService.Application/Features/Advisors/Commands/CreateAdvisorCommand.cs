using IdentityService.Application.Features.Advisors.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Advisors.Commands;

public record CreateAdvisorCommand(CreateAdvisorDto Advisor) : IRequest<Guid>;

public class CreateAdvisorCommandValidator : AbstractValidator<CreateAdvisorCommand>
{
    private readonly IAdvisorRepository _advisorRepository;

    public CreateAdvisorCommandValidator(IAdvisorRepository advisorRepository)
    {
        _advisorRepository = advisorRepository;

        RuleFor(x => x.Advisor.UserId).NotEmpty();
        RuleFor(x => x.Advisor.DepartmentId).NotEmpty();
        RuleFor(x => x.Advisor.MaxQuota)
            .GreaterThanOrEqualTo(0).WithMessage("Max quota cannot be negative.");
    }
}

public class CreateAdvisorCommandHandler : IRequestHandler<CreateAdvisorCommand, Guid>
{
    private readonly IAdvisorRepository _advisorRepository;

    public CreateAdvisorCommandHandler(IAdvisorRepository advisorRepository)
    {
        _advisorRepository = advisorRepository;
    }

    public async Task<Guid> Handle(CreateAdvisorCommand request, CancellationToken cancellationToken)
    {
        if (await _advisorRepository.GetByUserIdAsync(request.Advisor.UserId, cancellationToken) != null)
        {
            throw new ValidationException("User is already registered as an advisor.");
        }

        var entity = request.Advisor.Adapt<Advisor>();
        // Default values
        entity.CurrentQuota = 0;
        
        await _advisorRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}
