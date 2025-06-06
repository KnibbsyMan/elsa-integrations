using Elsa.Common.Models;
using Elsa.Extensions;
using Elsa.Persistence.MongoDb.Common;
using Elsa.Persistence.MongoDb.Helpers;
using Elsa.Workflows.Runtime;
using Elsa.Workflows.Runtime.Entities;
using Elsa.Workflows.Runtime.Filters;
using Elsa.Workflows.Runtime.OrderDefinitions;
using JetBrains.Annotations;
using MongoDB.Driver.Linq;

namespace Elsa.Persistence.MongoDb.Modules.Runtime;

/// <inheritdoc />
[UsedImplicitly]
public class MongoActivityExecutionLogStore(MongoDbStore<ActivityExecutionRecord> mongoDbStore) : IActivityExecutionStore
{
    /// <inheritdoc />
    public Task SaveAsync(ActivityExecutionRecord record, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.SaveAsync(record, cancellationToken);
    }

    /// <inheritdoc />
    public Task SaveManyAsync(IEnumerable<ActivityExecutionRecord> records, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.SaveManyAsync(records, cancellationToken);
    }

    /// <inheritdoc />
    public Task AddManyAsync(IEnumerable<ActivityExecutionRecord> records, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.AddManyAsync(records, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ActivityExecutionRecord?> FindAsync(ActivityExecutionRecordFilter filter, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.FindAsync(queryable => Filter(queryable, filter), cancellationToken);
    }

    /// <inheritdoc />
    public Task<IEnumerable<ActivityExecutionRecord>> FindManyAsync(ActivityExecutionRecordFilter filter, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.FindManyAsync(queryable => Filter(queryable, filter), cancellationToken);
    }

    /// <inheritdoc />
    public Task<IEnumerable<ActivityExecutionRecordSummary>> FindManySummariesAsync<TOrderBy>(ActivityExecutionRecordFilter filter, ActivityExecutionRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.FindManyAsync(queryable => Order(Filter(queryable, filter), order), ExpressionHelpers.ActivityExecutionRecordSummary, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IEnumerable<ActivityExecutionRecordSummary>> FindManySummariesAsync(ActivityExecutionRecordFilter filter, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.FindManyAsync(queryable => Filter(queryable, filter), ExpressionHelpers.ActivityExecutionRecordSummary, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IEnumerable<ActivityExecutionRecord>> FindManyAsync<TOrderBy>(ActivityExecutionRecordFilter filter, ActivityExecutionRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.FindManyAsync(queryable => Filter(queryable, filter), cancellationToken);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(ActivityExecutionRecordFilter filter, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.CountAsync(queryable => Filter(queryable, filter).OrderBy(x => x.StartedAt), cancellationToken);
    }

    /// <inheritdoc />
    public Task<long> DeleteManyAsync(ActivityExecutionRecordFilter filter, CancellationToken cancellationToken = default)
    {
        return mongoDbStore.DeleteWhereAsync<string>(queryable => Filter(queryable, filter), x => x.Id, cancellationToken);
    }

    private IQueryable<ActivityExecutionRecord> Filter(IQueryable<ActivityExecutionRecord> queryable, ActivityExecutionRecordFilter filter)
    {
        return filter.Apply(queryable);
    }

    private IQueryable<ActivityExecutionRecord> Order<TOrderBy>(IQueryable<ActivityExecutionRecord> queryable, ActivityExecutionRecordOrder<TOrderBy> order)
    {
        return queryable.OrderBy(order);
    }

    private IQueryable<ActivityExecutionRecord> Paginate(IQueryable<ActivityExecutionRecord> queryable, PageArgs pageArgs)
    {
        return queryable.Paginate(pageArgs);
    }
}