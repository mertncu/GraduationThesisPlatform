using IdentityService.Application.Features.Roles.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Roles.Commands;

public record CreateRoleCommand(CreateRoleDto Role) : IRequest<Guid>;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandValidator(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;

        RuleFor(x => x.Role.Name)
            .NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.")
            .MustAsync(BeUniqueName).WithMessage("The specified role name is already in use.");
            
        RuleFor(x => x.Role.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _roleRepository.GetByNameAsync(name, cancellationToken) is null;
    }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Role.Adapt<Role>();
        await _roleRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}
