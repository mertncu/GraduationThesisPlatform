using Mapster;
using System.Reflection;

namespace IdentityService.Application.Common.Mappings;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BaseEntity, object>()
            .Ignore("DomainEvents");
            
        config.Scan(Assembly.GetExecutingAssembly());
    }
}
