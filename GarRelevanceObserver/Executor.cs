using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarPullerClient;
using Microsoft.Extensions.Logging;
using Quartz;

namespace GarRelevanceObserver
{
    [DisallowConcurrentExecution]
    public class Executor : IJob
    {
 		private readonly ILogger<Executor> _logger;
		private readonly PullerClient _client;

		public Executor(ILogger<Executor> logger, PullerClient client)
		{
			_logger = logger;
			_client = client;
		}       
		public  Task Execute(IJobExecutionContext context)
		{   	
			_logger.LogInformation($"{DateTime.Now} - Executor");          
            
			var result = _client.DownloadFile().Result;
			if (result)
				_logger.LogInformation($"{DateTime.Now} - GarFile downloaded!");
			result = _client.ProcessFile().Result;
			if (result)
				_logger.LogInformation($"{DateTime.Now} - GarFile processed!");       
                         
        	return Task.CompletedTask;			
		}        
    }
}