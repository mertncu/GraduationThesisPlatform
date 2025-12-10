using IdentityService.Application.Features.Students.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Students.Commands;

public record UpdateStudentCommand(UpdateStudentDto Student) : IRequest;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Student.Id).NotEmpty();
        RuleFor(x => x.Student.DepartmentId).NotEmpty();
        RuleFor(x => x.Student.ProgramId).NotEmpty();
        
        RuleFor(x => x.Student.StudentNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Student.EnrollmentYear)
            .GreaterThan(1900);
    }
}

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
{
    private readonly IStudentRepository _studentRepository;

    public UpdateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _studentRepository.GetByIdAsync(request.Student.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Student with ID {request.Student.Id} not found.");

        request.Student.Adapt(entity);
        await _studentRepository.UpdateAsync(entity, cancellationToken);
    }
}

public record DeleteStudentCommand(Guid Id) : IRequest;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
{
    private readonly IStudentRepository _studentRepository;

    public DeleteStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        if (!await _studentRepository.ExistsAsync(request.Id, cancellationToken))
            throw new KeyNotFoundException($"Student with ID {request.Id} not found.");
            
        await _studentRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
