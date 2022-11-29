using Microsoft.Extensions.Logging;
using Quartz;
using ServiceLayer;
using System;
using System.Threading.Tasks;

namespace DownloadApp
{
	[DisallowConcurrentExecution]
	public class DownloadJob : IJob
	{

		private readonly ILogger<DownloadJob> logger;
		private readonly DownloadService downloadService;
		public DownloadJob(ILogger<DownloadJob> logger, DownloadService downloadService)
		{
			this.logger = logger;
			this.downloadService = downloadService;
		}

		public Task Execute(IJobExecutionContext context)
		{
        	logger.LogInformation($"{DateTime.Now} - DownloadJob alive!");
			downloadService.HandleZipFile("gar_delta_xml.zip");
			//downloadService.HandleZipFile("gar_xml.zip");
			//Console.WriteLine($"{DateTime.Now} - DownloadJob alive!");
        	return Task.CompletedTask;			
		}
	}
}

