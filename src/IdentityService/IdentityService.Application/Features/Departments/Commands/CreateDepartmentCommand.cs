using IdentityService.Application.Features.Departments.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Departments.Commands;

public record CreateDepartmentCommand(CreateDepartmentDto Department) : IRequest<Guid>;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandValidator(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;

        RuleFor(x => x.Department.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(100).WithMessage("Department name must not exceed 100 characters.");

        RuleFor(x => x.Department.Code)
            .NotEmpty().WithMessage("Department code is required.")
            .MaximumLength(20).WithMessage("Department code must not exceed 20 characters.")
            .MustAsync(BeUniqueCode).WithMessage("Department code is already in use.");
    }

    private async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
    {
        return await _departmentRepository.GetByCodeAsync(code, cancellationToken) is null;
    }
}

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Department.Adapt<Department>();
        await _departmentRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}
