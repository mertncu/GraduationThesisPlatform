namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class Student : BaseEntity {
    public Guid UserId {get; set;}
    public string StudentNumber {get; set;}
    public Guid DepartmentId {get; set;}
    public Guid ProgramId {get; set;}
    public int EnrollmentYear {get; set;}
    public bool IsActive {get; set;}
}