using IdentityService.Application.Features.AcademicTerms.Dtos;
using Mapster;

namespace IdentityService.Application.Features.AcademicTerms.Queries;

public record GetAcademicTermByIdQuery(Guid Id) : IRequest<AcademicTermDetailedDto>;
public record GetAllAcademicTermsQuery : IRequest<List<AcademicTermListDto>>;
public record SearchAcademicTermsQuery(string Keyword) : IRequest<List<AcademicTermListDto>>;

public class AcademicTermQueriesHandler : 
    IRequestHandler<GetAcademicTermByIdQuery, AcademicTermDetailedDto>,
    IRequestHandler<GetAllAcademicTermsQuery, List<AcademicTermListDto>>,
    IRequestHandler<SearchAcademicTermsQuery, List<AcademicTermListDto>>
{
    private readonly IAcademicTermRepository _repository;

    public AcademicTermQueriesHandler(IAcademicTermRepository repository)
    {
        _repository = repository;
    }

    public async Task<AcademicTermDetailedDto> Handle(GetAcademicTermByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"AcademicTerm with ID {request.Id} not found.");
        return entity.Adapt<AcademicTermDetailedDto>();
    }

    public async Task<List<AcademicTermListDto>> Handle(GetAllAcademicTermsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Adapt<List<AcademicTermListDto>>();
    }

    public async Task<List<AcademicTermListDto>> Handle(SearchAcademicTermsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.SearchAsync(request.Keyword, cancellationToken);
        return entities.Adapt<List<AcademicTermListDto>>();
    }
}
