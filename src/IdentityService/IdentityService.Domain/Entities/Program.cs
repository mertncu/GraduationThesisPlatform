namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class Program : BaseEntity {
    public Guid DepartmentId {get; set;}
    public string Name {get; set;}
    public string Code {get; set;}
    public bool IsActive {get; set;}
}