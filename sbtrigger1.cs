using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace servicebusfa562
{
    public class sbtrigger1
    {
        private readonly ILogger<sbtrigger1> _logger;

        public sbtrigger1(ILogger<sbtrigger1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(sbtrigger1))]
        public async Task Run(
            [ServiceBusTrigger("myqueue2", Connection = "sbconnstring")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            var sleepTime = Convert.ToInt32(Environment.GetEnvironmentVariable("sleepTime")); // milliseconds

            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            Thread.Sleep(sleepTime); // 15 seconds

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
