using IdentityService.Application.Common.Interfaces;

namespace IdentityService.Application.Features.Programs;

public interface IProgramRepository : IRepository<Program>
{
    Task<Program?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
}
