namespace Elsa.ServiceBus.Kafka;

public record CreateConsumerContext(ConsumerDefinition ConsumerDefinition, SchemaRegistryDefinition? SchemaRegistryDefinition);