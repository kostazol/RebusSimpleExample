using Amazon;
using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;

namespace RebusSimpleExample
{
    public static class Extensions
    {
        public static void AddRebusAsOneWayClient(this IServiceCollection services)
        {
            AmazonSQSConfig _sqsConfig = new AmazonSQSConfig();
            _sqsConfig.RegionEndpoint = RegionEndpoint.USEast2;
            var credentials = new Amazon.Runtime.BasicAWSCredentials("your-iam-access-key", "your-iam-secret-key");

            services.AddRebus(
                rebus => rebus
                    .Routing(r => r.TypeBased().Map<Message>("QueueName"))
                    .Transport(t => t.UseAmazonSQSAsOneWayClient(credentials, _sqsConfig))
                    .Options(t => t.SimpleRetryStrategy(errorQueueAddress: "ErrorQueue")));
        }
    }
}
