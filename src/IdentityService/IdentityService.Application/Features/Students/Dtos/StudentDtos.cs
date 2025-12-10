namespace IdentityService.Application.Features.Students.Dtos;

public class CreateStudentDto
{
    public Guid UserId { get; set; }
    public string StudentNumber { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public Guid ProgramId { get; set; }
    public int EnrollmentYear { get; set; }
}

public class UpdateStudentDto
{
    public Guid Id { get; set; }
    public string StudentNumber { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public Guid ProgramId { get; set; }
    public int EnrollmentYear { get; set; }
    public bool IsActive { get; set; }
}

public class StudentDetailedDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string StudentNumber { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public Guid ProgramId { get; set; }
    public int EnrollmentYear { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class StudentListDto
{
    public Guid Id { get; set; }
    public string StudentNumber { get; set; } = string.Empty;
    public int EnrollmentYear { get; set; }
    public bool IsActive { get; set; }
}
