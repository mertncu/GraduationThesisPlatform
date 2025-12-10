using IdentityService.Application.Features.Advisors.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Advisors.Queries;

public record GetAdvisorByIdQuery(Guid Id) : IRequest<AdvisorDetailedDto>;
public record GetAllAdvisorsQuery : IRequest<List<AdvisorListDto>>;
public record SearchAdvisorsQuery(string Keyword) : IRequest<List<AdvisorListDto>>;

public class AdvisorQueriesHandler : 
    IRequestHandler<GetAdvisorByIdQuery, AdvisorDetailedDto>,
    IRequestHandler<GetAllAdvisorsQuery, List<AdvisorListDto>>,
    IRequestHandler<SearchAdvisorsQuery, List<AdvisorListDto>>
{
    private readonly IAdvisorRepository _advisorRepository;

    public AdvisorQueriesHandler(IAdvisorRepository advisorRepository)
    {
        _advisorRepository = advisorRepository;
    }

    public async Task<AdvisorDetailedDto> Handle(GetAdvisorByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _advisorRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Advisor with ID {request.Id} not found.");
        return entity.Adapt<AdvisorDetailedDto>();
    }

    public async Task<List<AdvisorListDto>> Handle(GetAllAdvisorsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _advisorRepository.GetAllAsync(cancellationToken);
        return entities.Adapt<List<AdvisorListDto>>();
    }

    public async Task<List<AdvisorListDto>> Handle(SearchAdvisorsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _advisorRepository.SearchAsync(request.Keyword, cancellationToken);
        return entities.Adapt<List<AdvisorListDto>>();
    }
}
