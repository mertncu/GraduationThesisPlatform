namespace IdentityService.Application.Features.Users.Commands;

public record DeleteUserCommand(Guid Id) : IRequest;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("User ID is required.");
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await _userRepository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new KeyNotFoundException($"User with ID {request.Id} not found.");
        }

        await _userRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
