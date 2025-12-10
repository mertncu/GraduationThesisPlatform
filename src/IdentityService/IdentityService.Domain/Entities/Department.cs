namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class Department : BaseEntity {
    public string Name {get; set;}
    public string Code {get; set;}
}