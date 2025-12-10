namespace IdentityService.Application.Features.Roles.Commands;

public record DeleteRoleCommand(Guid Id) : IRequest;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        if (!await _roleRepository.ExistsAsync(request.Id, cancellationToken))
        {
            throw new KeyNotFoundException($"Role with ID {request.Id} not found.");
        }
        await _roleRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
