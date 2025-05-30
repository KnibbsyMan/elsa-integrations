using Elsa.Extensions;
using Elsa.Features.Services;

namespace Elsa.ServiceBus.Kafka;

public static class ModuleExtensions
{
    public static IModule UseKafka(this IModule module, Action<KafkaFeature>? configure = null)
    {
        return module.Use(configure);
    }
}