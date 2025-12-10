namespace IdentityService.Domain.Entities;
using Shared.Domain.Common;

public class SystemSetting : BaseEntity {
    public string SettingKey {get; set;}
    public string SettingValue {get; set;}
    public string Description {get; set;}
}