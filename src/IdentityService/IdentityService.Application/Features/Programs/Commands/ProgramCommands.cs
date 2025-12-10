using IdentityService.Application.Features.Programs.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Programs.Commands;

public record UpdateProgramCommand(UpdateProgramDto Program) : IRequest;

public class UpdateProgramCommandValidator : AbstractValidator<UpdateProgramCommand>
{
    public UpdateProgramCommandValidator()
    {
        RuleFor(x => x.Program.Id).NotEmpty();
        RuleFor(x => x.Program.DepartmentId).NotEmpty();
        
        RuleFor(x => x.Program.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Program.Code)
            .NotEmpty()
            .MaximumLength(20);
    }
}

public class UpdateProgramCommandHandler : IRequestHandler<UpdateProgramCommand>
{
    private readonly IProgramRepository _programRepository;

    public UpdateProgramCommandHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
    {
        var entity = await _programRepository.GetByIdAsync(request.Program.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Program with ID {request.Program.Id} not found.");

        request.Program.Adapt(entity);
        await _programRepository.UpdateAsync(entity, cancellationToken);
    }
}

public record DeleteProgramCommand(Guid Id) : IRequest;

public class DeleteProgramCommandHandler : IRequestHandler<DeleteProgramCommand>
{
    private readonly IProgramRepository _programRepository;

    public DeleteProgramCommandHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
    {
        if (!await _programRepository.ExistsAsync(request.Id, cancellationToken))
            throw new KeyNotFoundException($"Program with ID {request.Id} not found.");
            
        await _programRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
