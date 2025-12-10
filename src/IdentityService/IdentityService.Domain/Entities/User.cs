namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class User : BaseEntity {
    public string Email {get; set;}
    public string PasswordHash {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string PhoneNumber {get; set;}
    public bool IsActive {get; set;}
    public DateTime LastLoginAt {get; set;}
}