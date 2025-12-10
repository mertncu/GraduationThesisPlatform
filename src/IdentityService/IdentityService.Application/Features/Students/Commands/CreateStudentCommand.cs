using IdentityService.Application.Features.Students.Dtos;
using Mapster;

namespace IdentityService.Application.Features.Students.Commands;

public record CreateStudentCommand(CreateStudentDto Student) : IRequest<Guid>;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandValidator(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;

        RuleFor(x => x.Student.UserId).NotEmpty();
        RuleFor(x => x.Student.DepartmentId).NotEmpty();
        RuleFor(x => x.Student.ProgramId).NotEmpty();
        
        RuleFor(x => x.Student.StudentNumber)
            .NotEmpty().WithMessage("Student number is required.")
            .MaximumLength(20).WithMessage("Student number must not exceed 20 characters.")
            .MustAsync(BeUniqueStudentNumber).WithMessage("Student number is already in use.");

        RuleFor(x => x.Student.EnrollmentYear)
            .GreaterThan(1900).WithMessage("Invalid enrollment year.")
            .LessThanOrEqualTo(DateTime.UtcNow.Year + 1).WithMessage("Enrollment year cannot be in the far future.");
    }

    private async Task<bool> BeUniqueStudentNumber(string studentNumber, CancellationToken cancellationToken)
    {
        return await _studentRepository.GetByStudentNumberAsync(studentNumber, cancellationToken) is null;
    }
}

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        if (await _studentRepository.GetByUserIdAsync(request.Student.UserId, cancellationToken) != null)
        {
            throw new ValidationException("User is already registered as a student.");
        }

        var entity = request.Student.Adapt<Student>();
        await _studentRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}
