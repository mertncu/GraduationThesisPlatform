using IdentityService.Application.Features.Users.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Users.Queries;

public record SearchUsersQuery(string Keyword) : IRequest<List<UserListDto>>;

public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, List<UserListDto>>
{
    private readonly IUserRepository _userRepository;

    public SearchUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserListDto>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _userRepository.SearchAsync(request.Keyword, cancellationToken);
        return entities.Adapt<List<UserListDto>>();
    }
}
