using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using GarPullerClient;
using GarServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GarProxy
{
    public class DownloadService : BackgroundService
    {
        private const int MAX_PULLER_CALL_ATTEMPT = 3;
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _configuration;
        private readonly DownloadServiceQueue _queue;
        private readonly TimeSpan _refreshInterval = TimeSpan.FromSeconds(5);// FromMinutes(5);
        private readonly ILogger<DownloadService> _logger;    
            public DownloadService(
            DownloadServiceQueue queue,
            IServiceProvider provider,
            IConfiguration configuration,
            ILogger<DownloadService> logger)
        {
            _queue = queue;
            _provider = provider;
            _configuration = configuration;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {             
                var client = _provider.GetRequiredService<PullerClient>();
                var file = _queue.Dequeue();
                if (!string.IsNullOrEmpty(file.Item1))
                {
                    var fileName = $"{_configuration["Folder"]}{ExtractDate(file.Item1)}_{GetLast(file.Item1)}";
                    
                    try {
                        _logger.LogInformation($"Try to download {file.Item1}");
                        Retry.DoWithRetry(
                            () => DownloadFile(new Uri($"{file.Item1}"), fileName),
                            TimeSpan.FromSeconds(10),
                            (attempt) => _logger.LogInformation($"Attempt to Download. Left {attempt} attempt(s)"));
                        try {
                            var result = await Retry.DoWithRetryAsync<bool>(
                            async () => await client.PutDownloadedFile(fileName, file.Item2),
                            TimeSpan.FromSeconds(5),
                            (attempt) => _logger.LogInformation($"Attempt of POST PutDownloadedFile. Left {attempt} attempt(s)"));
                            _logger.LogInformation($"POST PutDownloadedFile OK {fileName} correlationId = {file.Item2}");
                            _queue.Archive();
                        } catch (Exception ex) {
                            _logger.LogError($"I cant' send OK to Puller {fileName} correlationId = {file.Item2}");
                            _logger.LogError(ex.Message);
                        }
                    } catch (Exception ex) {
                        _logger.LogError(ex.Message);
                    }
                }                
                await Task.Delay(_refreshInterval, stoppingToken);
            }
        } 
		public void DownloadFile(Uri url, string outputFilePath)
		{
			const int BUFFER_SIZE = 16 * 1024;
			using (var outputFileStream = File.Create(outputFilePath, BUFFER_SIZE))
			{
				var req = WebRequest.Create(url);
				using (var response = req.GetResponse())
				{
					using (var responseStream = response.GetResponseStream())
					{
						var buffer = new byte[BUFFER_SIZE];
						int bytesRead,totalBytesRead = 0;
                        int i = 0;
						do
						{
                            i++;
							bytesRead = responseStream.Read(buffer, 0, BUFFER_SIZE);
                            totalBytesRead += bytesRead;
						
						    outputFileStream.Write(buffer, 0, bytesRead);
						    if ((i % 100) == 0)
                                _logger.LogInformation(String.Format("Downloaded {0:n0} bytes", totalBytesRead));
						} while (bytesRead > 0);
                        _logger.LogInformation($"{url} is downloaded");
					}
				}
			}
		}   
        static string ExtractDate(string url) =>
            new Regex(@"\d{4}\.\d{1,2}.\d{1,2}")
                .Match(url).Value;
		static string GetLast(string url) =>		
			new Uri(url).Segments.Last();		  
    }   
}