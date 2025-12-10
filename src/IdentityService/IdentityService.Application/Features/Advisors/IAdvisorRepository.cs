using IdentityService.Application.Common.Interfaces;

namespace IdentityService.Application.Features.Advisors;

public interface IAdvisorRepository : IRepository<Advisor>
{
    Task<Advisor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
