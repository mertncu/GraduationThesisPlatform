using IdentityService.Application.Features.Users.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDetailedDto>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailedDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDetailedDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (entity == null)
        {
            throw new KeyNotFoundException($"User with ID {request.Id} not found.");
        }

        return entity.Adapt<UserDetailedDto>();
    }
}
