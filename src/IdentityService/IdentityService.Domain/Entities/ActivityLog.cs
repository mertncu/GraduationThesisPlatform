namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class ActivityLog : BaseEntity {
    public Guid UserId {get; set;}
    public string Action {get; set;}
    public string EntityName {get; set;}
    public Guid EntityId {get; set;}
    public DateTime Timestamp {get; set;}
    public string DetailJson {get; set;}
}