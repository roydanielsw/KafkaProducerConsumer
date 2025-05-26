
using Confluent.Kafka;
using KafkaProducer;
// See https://aka.ms/new-console-template for more information

CancellationTokenSource bmwProducerToken = new CancellationTokenSource();

var config = new ProducerConfig
{
    BootstrapServers = "192.168.185.171:9092"
};

using var bmwProducer = new ProducerBuilder<Null, string>(config).Build();

await Task.Run(async () =>
{
    while (!bmwProducerToken.Token.IsCancellationRequested)
    {
        Console.WriteLine("Creating BMW");
        var bmw = new BMW() { Model = "X1" };
        var deliveryResult = await bmwProducer.ProduceAsync(
              "vehicle-topic",
              new Message<Null, string> { Value = bmw.ToString() });
        Task.Delay(15000);

    }
});

Console.WriteLine("Hello");
Console.Read();
bmwProducerToken.Cancel();
Console.Read();