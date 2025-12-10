namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class UserRole : BaseEntity {
    public Guid UserId {get; set;}
    public Guid RoleId {get; set;}
}