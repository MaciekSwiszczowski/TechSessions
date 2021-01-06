using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Polly.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test()
        {
            var policy = Policy
                .Handle<Exception>()
                .OrResult<bool>(_ => true)
                .WaitAndRetryAsync(3,
                    retryAttempt => TimeSpan.FromSeconds(1),
                    (response, delay, retryCount, context) =>
                    {
                        Console.WriteLine(
                            $"Retry {retryCount} after {delay.Seconds} seconds delay due to {response.Exception.Message}");
                    });

            int counter = 0;

            var result = await policy.ExecuteAsync(() =>
            {
                counter++;

                if (counter == 0)
                    throw new Exception("Test!");



                return GetResult();

            });
        }

        private Task<bool> GetResult()
        {
            return Task.Run(() =>
            {
                throw new Exception("No znowu!");
                return true;
            });
        }
    }
}