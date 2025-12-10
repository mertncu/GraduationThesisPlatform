using IdentityService.Application.Common.Interfaces;

namespace IdentityService.Application.Features.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
