using IdentityService.Application.Common.Interfaces;

namespace IdentityService.Application.Features.Students;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetByStudentNumberAsync(string studentNumber, CancellationToken cancellationToken = default);
    Task<Student?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
