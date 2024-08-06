using System.Text.Json;
using Confluent.Kafka;

namespace KafkaProducerExample;

using static KafkaUtils;

public static class Program
{
    const string brokerList = "127.0.0.1:19092";
    const string topicName = "students1";

    public static async Task Main(string[] args)
    {
        Person[] persons = GetTestData("data.json") ?? throw new NullReferenceException("Deserialize");

        using var producer = GetProducerBuilder(brokerList).Build();

        try
        {
            // Note: Awaiting the asynchronous produce request below prevents flow of execution
            // from proceeding until the acknowledgement from the broker is received (at the
            // expense of low throughput).


            foreach (var person in persons)
            {
                DeliveryResult<string, string> deliveryReport = await producer.ProducePersonAsync(topicName, person);

                Console.WriteLine($"delivered to: {deliveryReport.TopicPartitionOffset}");
                Thread.Sleep(1);
            }
        }
        catch (ProduceException<string, string> e)
        {
            Console.WriteLine($"failed to deliver message: {e.Message} [{e.Error.Code}]");
        }
    }
}

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
