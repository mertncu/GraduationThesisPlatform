using IdentityService.Application.Common.Interfaces;

namespace IdentityService.Application.Features.Roles;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
