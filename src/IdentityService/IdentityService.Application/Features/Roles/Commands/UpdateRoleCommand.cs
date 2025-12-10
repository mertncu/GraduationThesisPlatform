using IdentityService.Application.Features.Roles.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Roles.Commands;

public record UpdateRoleCommand(UpdateRoleDto Role) : IRequest;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Role.Id).NotEmpty().WithMessage("Role ID is required.");
        RuleFor(x => x.Role.Name)
            .NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.");
            
        RuleFor(x => x.Role.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
    }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _roleRepository.GetByIdAsync(request.Role.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Role with ID {request.Role.Id} not found.");

        request.Role.Adapt(entity);
        await _roleRepository.UpdateAsync(entity, cancellationToken);
    }
}
