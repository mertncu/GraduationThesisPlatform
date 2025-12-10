using IdentityService.Application.Common.Interfaces;

namespace IdentityService.Application.Features.AcademicTerms;

public interface IAcademicTermRepository : IRepository<AcademicTerm>
{
    Task<AcademicTerm?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
