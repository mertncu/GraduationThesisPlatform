using IdentityService.Application.Common.Interfaces;

namespace IdentityService.Application.Features.Departments;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
}
