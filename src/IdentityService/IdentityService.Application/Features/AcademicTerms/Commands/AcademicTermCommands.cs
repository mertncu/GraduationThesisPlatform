using IdentityService.Application.Features.AcademicTerms.Dtos;
using Mapster;

namespace IdentityService.Application.Features.AcademicTerms.Commands;

public record UpdateAcademicTermCommand(UpdateAcademicTermDto AcademicTerm) : IRequest;

public class UpdateAcademicTermCommandValidator : AbstractValidator<UpdateAcademicTermCommand>
{
    public UpdateAcademicTermCommandValidator()
    {
        RuleFor(x => x.AcademicTerm.Id).NotEmpty();
        
        RuleFor(x => x.AcademicTerm.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.AcademicTerm.StartDate)
            .LessThan(x => x.AcademicTerm.EndDate).WithMessage("Start date must be before end date.");
            
        RuleFor(x => x.AcademicTerm.EndDate)
            .GreaterThan(x => x.AcademicTerm.StartDate).WithMessage("End date must be after start date.");
    }
}

public class UpdateAcademicTermCommandHandler : IRequestHandler<UpdateAcademicTermCommand>
{
    private readonly IAcademicTermRepository _repository;

    public UpdateAcademicTermCommandHandler(IAcademicTermRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateAcademicTermCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.AcademicTerm.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"AcademicTerm with ID {request.AcademicTerm.Id} not found.");

        request.AcademicTerm.Adapt(entity);
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}

public record DeleteAcademicTermCommand(Guid Id) : IRequest;

public class DeleteAcademicTermCommandHandler : IRequestHandler<DeleteAcademicTermCommand>
{
    private readonly IAcademicTermRepository _repository;

    public DeleteAcademicTermCommandHandler(IAcademicTermRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteAcademicTermCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.ExistsAsync(request.Id, cancellationToken))
            throw new KeyNotFoundException($"AcademicTerm with ID {request.Id} not found.");
            
        await _repository.DeleteAsync(request.Id, cancellationToken);
    }
}
