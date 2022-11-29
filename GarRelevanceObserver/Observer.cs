using System;
using System.Threading.Tasks;
using GarPullerClient;
using Microsoft.Extensions.Logging;
using Quartz;

namespace GarRelevanceObserver
{
	[DisallowConcurrentExecution]
	public class Observer : IJob
	{

		private readonly ILogger<Observer> _logger;
		private readonly PullerClient _client;

		public Observer(ILogger<Observer> logger, PullerClient client)
		{
			_logger = logger;
			_client = client;
		}

		public  Task Execute(IJobExecutionContext context)
		{   	
			_logger.LogInformation($"{DateTime.Now} - Observer");
			
			var result = _client.UpdateList().Result;
			if (result)
				_logger.LogInformation($"{DateTime.Now} - GarFileList updated!");
				
        	return Task.CompletedTask;			
		}
	}
}

