using Confluent.Kafka;

CancellationTokenSource consumerTokenSource = new CancellationTokenSource();

var config = new ConsumerConfig
{
    BootstrapServers = "192.168.185.171:9092", // Kafka running on wsl linux
    GroupId = Guid.NewGuid().ToString(),
    AutoOffsetReset = AutoOffsetReset.Earliest, // read from beginning if no committed offset
    EnableAutoCommit = true
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("vehicle-topic"); // change to your topic name


Task.Run(() => {
    while (!consumerTokenSource.Token.IsCancellationRequested)
    {
        Console.WriteLine("Consuming BMW");
        try
        {
            var result = consumer.Consume(consumerTokenSource.Token);
            Console.WriteLine($"Received: '{result.Message.Value}' at {result.TopicPartitionOffset}");
        }
        catch (ConsumeException e)
        {
            Console.WriteLine($"Consume error: {e.Error.Reason}");
        }
        Task.Delay(2000);

    }

    return Task.CompletedTask;
});

Console.WriteLine("Press Any key to cancel");
Console.Read();
consumerTokenSource.Cancel();
Console.WriteLine("Task Cancelled");
Console.Read();
