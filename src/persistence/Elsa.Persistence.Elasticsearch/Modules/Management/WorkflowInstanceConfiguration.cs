using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elsa.Persistence.Elasticsearch.Common;
using Elsa.Persistence.Elasticsearch.Options;
using Elsa.Workflows.Management.Entities;
using Microsoft.Extensions.Options;

namespace Elsa.Persistence.Elasticsearch.Modules.Management;

/// <summary>
/// Configures Elasticsearch with mappings for <see cref="WorkflowInstance"/>.
/// </summary>
public class WorkflowInstanceConfiguration : IndexConfiguration<WorkflowInstance>
{
    private readonly ElasticsearchOptions _options;

    /// <inheritdoc />
    public WorkflowInstanceConfiguration(IOptions<ElasticsearchOptions> options)
    {
        _options = options.Value;
    }
    
    /// <inheritdoc />
    public override void ConfigureClientSettings(ElasticsearchClientSettings settings)
    {
        var alias = _options.GetIndexNameFor<WorkflowInstance>();
        var indexName = IndexNamingStrategy.GenerateName(alias);
        settings.DefaultMappingFor<WorkflowInstance>(m => m.IndexName(indexName));
    }

    /// <inheritdoc />
    public override async ValueTask ConfigureClientAsync(ElasticsearchClient client, CancellationToken cancellationToken)
    {
        var alias = _options.GetIndexNameFor<WorkflowInstance>();
        var indexName = IndexNamingStrategy.GenerateName(alias);
        var descriptor = new CreateIndexRequestDescriptor<WorkflowInstance>(indexName);
        descriptor.Mappings(m => m.Properties(p => p.Flattened(d => d.WorkflowState.Properties)));
        await client.Indices.CreateAsync(descriptor, cancellationToken);
    }
}