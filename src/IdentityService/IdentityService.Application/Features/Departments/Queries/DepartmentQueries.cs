using IdentityService.Application.Features.Departments.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Departments.Queries;

public record GetDepartmentByIdQuery(Guid Id) : IRequest<DepartmentDetailedDto>;
public record GetAllDepartmentsQuery : IRequest<List<DepartmentListDto>>;
public record SearchDepartmentsQuery(string Keyword) : IRequest<List<DepartmentListDto>>;

public class DepartmentQueriesHandler : 
    IRequestHandler<GetDepartmentByIdQuery, DepartmentDetailedDto>,
    IRequestHandler<GetAllDepartmentsQuery, List<DepartmentListDto>>,
    IRequestHandler<SearchDepartmentsQuery, List<DepartmentListDto>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentQueriesHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentDetailedDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _departmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Department with ID {request.Id} not found.");
        return entity.Adapt<DepartmentDetailedDto>();
    }

    public async Task<List<DepartmentListDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _departmentRepository.GetAllAsync(cancellationToken);
        return entities.Adapt<List<DepartmentListDto>>();
    }

    public async Task<List<DepartmentListDto>> Handle(SearchDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _departmentRepository.SearchAsync(request.Keyword, cancellationToken);
        return entities.Adapt<List<DepartmentListDto>>();
    }
}
