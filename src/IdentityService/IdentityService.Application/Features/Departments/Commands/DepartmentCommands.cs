using IdentityService.Application.Features.Departments.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Departments.Commands;

public record UpdateDepartmentCommand(UpdateDepartmentDto Department) : IRequest;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.Department.Id).NotEmpty();
        
        RuleFor(x => x.Department.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Department.Code)
            .NotEmpty()
            .MaximumLength(20);
    }
}

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _departmentRepository.GetByIdAsync(request.Department.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Department with ID {request.Department.Id} not found.");

        request.Department.Adapt(entity);
        await _departmentRepository.UpdateAsync(entity, cancellationToken);
    }
}

public record DeleteDepartmentCommand(Guid Id) : IRequest;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        if (!await _departmentRepository.ExistsAsync(request.Id, cancellationToken))
            throw new KeyNotFoundException($"Department with ID {request.Id} not found.");
            
        await _departmentRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
