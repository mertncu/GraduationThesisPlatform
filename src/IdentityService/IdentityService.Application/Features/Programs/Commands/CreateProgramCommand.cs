using IdentityService.Application.Features.Programs.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Programs.Commands;

public record CreateProgramCommand(CreateProgramDto Program) : IRequest<Guid>;

public class CreateProgramCommandValidator : AbstractValidator<CreateProgramCommand>
{
    private readonly IProgramRepository _programRepository;

    public CreateProgramCommandValidator(IProgramRepository programRepository)
    {
        _programRepository = programRepository;

        RuleFor(x => x.Program.DepartmentId).NotEmpty();
        
        RuleFor(x => x.Program.Name)
            .NotEmpty().WithMessage("Program name is required.")
            .MaximumLength(100).WithMessage("Program name must not exceed 100 characters.");

        RuleFor(x => x.Program.Code)
            .NotEmpty().WithMessage("Program code is required.")
            .MaximumLength(20).WithMessage("Program code must not exceed 20 characters.")
            .MustAsync(BeUniqueCode).WithMessage("Program code is already in use.");
    }

    private async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
    {
        return await _programRepository.GetByCodeAsync(code, cancellationToken) is null;
    }
}

public class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, Guid>
{
    private readonly IProgramRepository _programRepository;

    public CreateProgramCommandHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task<Guid> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Program.Adapt<Program>();
        await _programRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}
