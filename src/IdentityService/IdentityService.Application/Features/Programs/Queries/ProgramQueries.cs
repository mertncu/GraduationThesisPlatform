using IdentityService.Application.Features.Programs.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Programs.Queries;

public record GetProgramByIdQuery(Guid Id) : IRequest<ProgramDetailedDto>;
public record GetAllProgramsQuery : IRequest<List<ProgramListDto>>;
public record SearchProgramsQuery(string Keyword) : IRequest<List<ProgramListDto>>;

public class ProgramQueriesHandler : 
    IRequestHandler<GetProgramByIdQuery, ProgramDetailedDto>,
    IRequestHandler<GetAllProgramsQuery, List<ProgramListDto>>,
    IRequestHandler<SearchProgramsQuery, List<ProgramListDto>>
{
    private readonly IProgramRepository _programRepository;

    public ProgramQueriesHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task<ProgramDetailedDto> Handle(GetProgramByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _programRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Program with ID {request.Id} not found.");
        return entity.Adapt<ProgramDetailedDto>();
    }

    public async Task<List<ProgramListDto>> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _programRepository.GetAllAsync(cancellationToken);
        return entities.Adapt<List<ProgramListDto>>();
    }

    public async Task<List<ProgramListDto>> Handle(SearchProgramsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _programRepository.SearchAsync(request.Keyword, cancellationToken);
        return entities.Adapt<List<ProgramListDto>>();
    }
}
