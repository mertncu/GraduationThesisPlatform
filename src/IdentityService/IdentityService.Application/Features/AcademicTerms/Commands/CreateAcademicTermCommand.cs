using IdentityService.Application.Features.AcademicTerms.Dtos;
using Mapster;

namespace IdentityService.Application.Features.AcademicTerms.Commands;

public record CreateAcademicTermCommand(CreateAcademicTermDto AcademicTerm) : IRequest<Guid>;

public class CreateAcademicTermCommandValidator : AbstractValidator<CreateAcademicTermCommand>
{
    private readonly IAcademicTermRepository _repository;

    public CreateAcademicTermCommandValidator(IAcademicTermRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.AcademicTerm.Name)
            .NotEmpty().WithMessage("Term name is required.")
            .MaximumLength(50).WithMessage("Term name must not exceed 50 characters.");

        RuleFor(x => x.AcademicTerm.StartDate)
            .LessThan(x => x.AcademicTerm.EndDate).WithMessage("Start date must be before end date.");
            
        RuleFor(x => x.AcademicTerm.EndDate)
            .GreaterThan(x => x.AcademicTerm.StartDate).WithMessage("End date must be after start date.");
    }
}

public class CreateAcademicTermCommandHandler : IRequestHandler<CreateAcademicTermCommand, Guid>
{
    private readonly IAcademicTermRepository _repository;

    public CreateAcademicTermCommandHandler(IAcademicTermRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateAcademicTermCommand request, CancellationToken cancellationToken)
    {
        var entity = request.AcademicTerm.Adapt<AcademicTerm>();
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}
