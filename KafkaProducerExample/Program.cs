using Confluent.Kafka;

namespace KafkaProducerExample;

using static KafkaUtils;

public static class Program
{
    const string brokerList = "127.0.0.1:19092";
    const string topicName = "students1";
    //const string topicName = "test_topic";

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
