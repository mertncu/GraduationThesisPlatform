using IdentityService.Application.Features.Users.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Users.Queries;

public record GetAllUsersQuery : IRequest<List<UserListDto>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserListDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserListDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _userRepository.GetAllAsync(cancellationToken);
        return entities.Adapt<List<UserListDto>>();
    }
}
