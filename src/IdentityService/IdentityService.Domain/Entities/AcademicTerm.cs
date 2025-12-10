namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class AcademicTerm : BaseEntity {
    public string Name {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
}