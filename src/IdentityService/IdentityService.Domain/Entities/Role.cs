namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class Role : BaseEntity {
    public string Name {get; set;}
    public string Description {get; set;}
}