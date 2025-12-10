namespace IdentityService.Application.Features.Advisors.Dtos;

public class CreateAdvisorDto
{
    public Guid UserId { get; set; }
    public Guid DepartmentId { get; set; }
    public int MaxQuota { get; set; }
}

public class UpdateAdvisorDto
{
    public Guid Id { get; set; }
    public Guid DepartmentId { get; set; }
    public int MaxQuota { get; set; }
    public int CurrentQuota { get; set; }
    public bool IsActive { get; set; }
}

public class AdvisorDetailedDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid DepartmentId { get; set; }
    public int MaxQuota { get; set; }
    public int CurrentQuota { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AdvisorListDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int MaxQuota { get; set; }
    public int CurrentQuota { get; set; }
    public bool IsActive { get; set; }
}
