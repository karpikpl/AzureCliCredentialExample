using Azure.Identity;
using Azure.ResourceManager;

namespace AzureCliCredentialExample
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;

        public Worker(IConfiguration configuration, ILogger<Worker> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bool connected = false;
            while (!stoppingToken.IsCancellationRequested && !connected)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    var creds = new DefaultAzureCredential(includeInteractiveCredentials: false);

                    ArmClient client = new ArmClient(new DefaultAzureCredential());
                    var subscriptionResource = await client.GetDefaultSubscriptionAsync();
                    var subscription = await subscriptionResource.GetAsync();
                    _logger.LogCritical($"Connected! Subscription displayname: {subscription.Value.Data.DisplayName}");
                    connected = true;
                }
                catch (CredentialUnavailableException ex)
                {
                    _logger.LogCritical(ex, "Error");
                    _logger.LogInformation("Waiting 10 seconds before retrying");
                    await Task.Delay(10000, stoppingToken);
                }
            }
        }
    }
}