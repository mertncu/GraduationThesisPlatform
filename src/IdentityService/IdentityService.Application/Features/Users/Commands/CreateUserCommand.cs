using IdentityService.Application.Features.Users.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Users.Commands;

public record CreateUserCommand(CreateUserDto User) : IRequest<Guid>;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.User.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
            .MustAsync(BeUniqueEmail).WithMessage("The specified email is already in use.");

        RuleFor(x => x.User.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.");

        RuleFor(x => x.User.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.User.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
            
        RuleFor(x => x.User.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.");
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByEmailAsync(email, cancellationToken) is null;
    }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = request.User.Adapt<User>();
        
        // In a real app, hash password here
        // entity.PasswordHash = _passwordHasher.Hash(request.User.Password);
        // For now, mapping directly assumes password is handled or we manually set it if DTO has raw password
        
        // Manual mapping for password specific logic if needed
        entity.PasswordHash = request.User.Password; // PLAIN TEXT FOR NOW as placeholder

        await _userRepository.AddAsync(entity, cancellationToken);
        
        return entity.Id;
    }
}
