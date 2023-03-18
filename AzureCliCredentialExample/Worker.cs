using Azure.Identity;
using Azure.Storage.Blobs;

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
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    Uri blobContainerUri = _configuration.GetValue<Uri>("blobContainerUri");
                    _logger.LogCritical($"Getting Files from {blobContainerUri}");

                    var creds = new DefaultAzureCredential(includeInteractiveCredentials: false);

                    BlobContainerClient containerClient = new BlobContainerClient(blobContainerUri, creds);
                    var blobs = containerClient.GetBlobsAsync(Azure.Storage.Blobs.Models.BlobTraits.None);
                    int ind = 1;
                    await foreach (var blob in blobs)
                    {
                        _logger.LogCritical($"{ind++} : {blob.Name}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Error");
                    _logger.LogInformation("Waiting 10 seconds before retrying");
                    await Task.Delay(10000, stoppingToken);
                }
            }
        }
    }
}