using IdentityService.Application.Features.Students.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Students.Queries;

public record GetStudentByIdQuery(Guid Id) : IRequest<StudentDetailedDto>;
public record GetAllStudentsQuery : IRequest<List<StudentListDto>>;
public record SearchStudentsQuery(string Keyword) : IRequest<List<StudentListDto>>;

public class StudentQueriesHandler : 
    IRequestHandler<GetStudentByIdQuery, StudentDetailedDto>,
    IRequestHandler<GetAllStudentsQuery, List<StudentListDto>>,
    IRequestHandler<SearchStudentsQuery, List<StudentListDto>>
{
    private readonly IStudentRepository _studentRepository;

    public StudentQueriesHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentDetailedDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Student with ID {request.Id} not found.");
        return entity.Adapt<StudentDetailedDto>();
    }

    public async Task<List<StudentListDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _studentRepository.GetAllAsync(cancellationToken);
        return entities.Adapt<List<StudentListDto>>();
    }

    public async Task<List<StudentListDto>> Handle(SearchStudentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _studentRepository.SearchAsync(request.Keyword, cancellationToken);
        return entities.Adapt<List<StudentListDto>>();
    }
}
