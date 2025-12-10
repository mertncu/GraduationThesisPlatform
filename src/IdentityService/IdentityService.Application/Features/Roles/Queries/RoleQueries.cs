using IdentityService.Application.Features.Roles.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Roles.Queries;

public record GetRoleByIdQuery(Guid Id) : IRequest<RoleDetailedDto>;
public record GetAllRolesQuery : IRequest<List<RoleListDto>>;
public record SearchRolesQuery(string Keyword) : IRequest<List<RoleListDto>>;

public class RoleQueriesHandler : 
    IRequestHandler<GetRoleByIdQuery, RoleDetailedDto>,
    IRequestHandler<GetAllRolesQuery, List<RoleListDto>>,
    IRequestHandler<SearchRolesQuery, List<RoleListDto>>
{
    private readonly IRoleRepository _roleRepository;

    public RoleQueriesHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleDetailedDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Role with ID {request.Id} not found.");
        return entity.Adapt<RoleDetailedDto>();
    }

    public async Task<List<RoleListDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _roleRepository.GetAllAsync(cancellationToken);
        return entities.Adapt<List<RoleListDto>>();
    }

    public async Task<List<RoleListDto>> Handle(SearchRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _roleRepository.SearchAsync(request.Keyword, cancellationToken);
        return entities.Adapt<List<RoleListDto>>();
    }
}
