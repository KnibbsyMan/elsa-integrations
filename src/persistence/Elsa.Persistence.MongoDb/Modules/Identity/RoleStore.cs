using Elsa.Identity.Contracts;
using Elsa.Identity.Entities;
using Elsa.Identity.Models;
using Elsa.Persistence.MongoDb.Common;
using JetBrains.Annotations;
using MongoDB.Driver.Linq;

namespace Elsa.Persistence.MongoDb.Modules.Identity;

/// <summary>
/// A MongoDb implementation of <see cref="IRoleStore"/>.
/// </summary>
[UsedImplicitly]
public class MongoRoleStore(MongoDbStore<Role> roleMongoDbStore) : IRoleStore
{
    /// <inheritdoc />
    public Task SaveAsync(Role application, CancellationToken cancellationToken = default)
    {
        return roleMongoDbStore.SaveAsync(application, cancellationToken);
    }

    /// <inheritdoc />
    public Task AddAsync(Role role, CancellationToken cancellationToken = default)
    {
        return roleMongoDbStore.SaveAsync(role, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteAsync(RoleFilter filter, CancellationToken cancellationToken = default)
    {
        return roleMongoDbStore.DeleteWhereAsync<string>(query => Filter(query, filter), x => x.Id, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Role?> FindAsync(RoleFilter filter, CancellationToken cancellationToken = default)
    {
        return roleMongoDbStore.FindAsync(query => Filter(query, filter), cancellationToken);
    }

    /// <inheritdoc />
    public Task<IEnumerable<Role>> FindManyAsync(RoleFilter filter, CancellationToken cancellationToken = default)
    {
        return roleMongoDbStore.FindManyAsync(queryable => Filter(queryable, filter), cancellationToken);
    }

    private static IQueryable<Role> Filter(IQueryable<Role> query, RoleFilter filter)
    {
        return filter.Apply(query);
    }
}