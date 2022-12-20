using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarPullerClient;
using GarServices;
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
            try {
				ServiceState result = _client.Go().Result;
				_logger.LogInformation($"{DateTime.Now} {result} - GarPuller.Go");

			} catch (Exception ex) {
				_logger.LogError($"{DateTime.Now} - GarPuller.Go failed {ex.Message}");
			}                       
        	return Task.CompletedTask;			
		}        
    }
}