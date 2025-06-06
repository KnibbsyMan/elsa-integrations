namespace Elsa.ServiceBus.Kafka;

public interface IConsumerDefinitionProvider
{
    Task<IEnumerable<ConsumerDefinition>> GetConsumerDefinitionsAsync(CancellationToken cancellationToken = default);
}