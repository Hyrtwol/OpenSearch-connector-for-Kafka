using System.Text.Json;
using Confluent.Kafka;

namespace KafkaProducerExample;

public static class KafkaUtils
{
    public static async Task<DeliveryResult<string, string>> ProducePersonAsync(this IProducer<string, string> producer,
        string topicName, Person person, string key = null!)
    {
        string json = JsonSerializer.Serialize(person);
        var message = new Message<string, string> { Key = key, Value = json };
        var deliveryResult = await producer.ProduceAsync(topicName, message);
        return deliveryResult;
    }

    public static ProducerBuilder<string, string> GetProducerBuilder(string brokerList)
    {
        var config = new ProducerConfig { BootstrapServers = brokerList };
        var producerBuilder = new ProducerBuilder<string, string>(config);
        return producerBuilder;
    }

    public static Person[]? GetTestData(string path)
    {
        using var stream = File.OpenRead(path);
        return JsonSerializer.Deserialize<Person[]>(stream);
    }
}
