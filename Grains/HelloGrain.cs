using Microsoft.Extensions.Logging;

namespace Snakes;

public class HelloGrain : Orleans.Grain, IHello
{
    private readonly ILogger<HelloGrain> _logger;

    public HelloGrain(ILogger<HelloGrain> logger)
    {
        _logger = logger;
    }

    Task<string> IHello.SayHello(string greeting)
    {
        _logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
        return Task.FromResult($"\n Client said: '{greeting}', so HelloGrain says: Hello!");
    }
}
