using MassTransit;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages;

const string connectionString = "amqp://guest:guest@raspberrypi.local";
var busControl = Bus.Factory.CreateUsingRabbitMq(configurator => configurator.Host(connectionString));

await busControl.StartAsync();

try
{
    do
    {
        Console.WriteLine("Enter message (or quit to exit)");
        Console.Write("> ");
        var value = Console.ReadLine();

        if(value is null or "quit" || !int.TryParse(value, out var id))
            break;

        await busControl.Publish<ContestStageCreated>(new(id));
    }
    while (true);
}
finally
{
    await busControl.StopAsync();
}
