namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class Advisor : BaseEntity {
    public Guid UserId {get; set;}
    public Guid DepartmentId {get; set;}
    public int MaxQuota {get; set;}
    public int CurrentQuota {get; set;}
    public bool IsActive {get; set;}
}