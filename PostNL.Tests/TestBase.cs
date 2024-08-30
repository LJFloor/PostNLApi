using PostNLApi;

namespace PostNL.Tests;

public class TestBase
{
    internal PostNLClient Client;

    [SetUp]
    public void Setup()
    {
        var apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? throw new Exception("No api key in environment variables");
        Client = new PostNLClient(apiKey, TestHelper.CustomerCode, TestHelper.CustomerNumber, sandbox: true)
        {
            EnableDebug = true
        };

        Client.Debug += (_, message) => Console.WriteLine(message);
    }
}