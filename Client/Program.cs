using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans;
using Snakes;

try
{
    using (var client = await ConnectClient())
    {
        await DoClientWork(client);
        Console.ReadKey();
    }

    return 0;
}
catch (Exception e)
{
    Console.WriteLine($"\nException while trying to run client: {e.Message}");
    Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
    Console.WriteLine("\nPress any key to exit.");
    Console.ReadKey();
    return 1;
}

static async Task<IClusterClient> ConnectClient()
{
    IClusterClient client;
    client = new ClientBuilder()
        .UseLocalhostClustering()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "Snakes";
        })
        .ConfigureLogging(logging => logging.AddConsole())
        .Build();

    await client.Connect();
    Console.WriteLine("Client successfully connected to silo host \n");
    return client;
}

static async Task DoClientWork(IClusterClient client)
{
    // example of calling grains from the initialized client
    var friend = client.GetGrain<IHello>(0);
    var response = await friend.SayHello("Good morning, HelloGrain!");
    Console.WriteLine($"\n\n{response}\n\n");
}