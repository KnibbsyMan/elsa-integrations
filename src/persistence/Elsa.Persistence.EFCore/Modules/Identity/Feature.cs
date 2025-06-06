using Elsa.Features.Attributes;
using Elsa.Features.Services;
using Elsa.Identity.Entities;
using Elsa.Identity.Features;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.Persistence.EFCore.Modules.Identity;

/// <summary>
/// Configures the <see cref="IdentityFeature"/> feature with Entity Framework Core persistence providers.
/// </summary>
[DependsOn(typeof(IdentityFeature))]
[PublicAPI]
public class EFCoreIdentityPersistenceFeature(IModule module) : PersistenceFeatureBase<EFCoreIdentityPersistenceFeature, IdentityElsaDbContext>(module)
{
    /// <inheritdoc />
    public override void Configure()
    {
        Module.Configure<IdentityFeature>(feature =>
        {
            feature.UserStore = sp => sp.GetRequiredService<EFCoreUserStore>();
            feature.ApplicationStore = sp => sp.GetRequiredService<EFCoreApplicationStore>();
            feature.RoleStore = sp => sp.GetRequiredService<EFCoreRoleStore>();
        });
    }

    /// <inheritdoc />
    public override void Apply()
    {
        base.Apply();
        AddEntityStore<User, EFCoreUserStore>();
        AddEntityStore<Application, EFCoreApplicationStore>();
        AddEntityStore<Role, EFCoreRoleStore>();
    }
}