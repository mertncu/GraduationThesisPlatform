using IdentityService.Application.Features.Users.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Users.Commands;

public record UpdateUserCommand(UpdateUserDto User) : IRequest;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.User.Id)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.User.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.User.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
            
        RuleFor(x => x.User.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.");
    }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetByIdAsync(request.User.Id, cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"User with ID {request.User.Id} not found.");
        }

        request.User.Adapt(entity);

        await _userRepository.UpdateAsync(entity, cancellationToken);
    }
}
