namespace Snakes;

public interface IHello : Orleans.IGrainWithIntegerKey
{
    Task<string> SayHello(string greeting);
}
