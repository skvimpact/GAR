using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GarPublicClient;
using GarPullerClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace GarProxy.Controllers
{
    [Route("WebServices/[controller]")]
    public class PublicController : Controller
    {
        private readonly ILogger<PublicController> _logger;
        private readonly PublicClient _publicClient;
        private readonly PullerClient _pullerClient;
        public PublicController(ILogger<PublicController> logger, PublicClient publicClient, PullerClient pullerClient)
        {
            _logger = logger;
            _publicClient = publicClient;
            _pullerClient = pullerClient;
        }

        [HttpGet("GetAllDownloadFileInfo")]
        public async Task<IEnumerable<DownloadFileInfo>?> GetAllDownloadFileInfo()
        {
            _logger.LogInformation("call GetAllDownloadFileInfo");
            return await _publicClient.GetAllDownloadFileInfo();
        }

        [HttpGet("GetLastDownloadFileInfo")]
        public async Task<DownloadFileInfo?> GetLastDownloadFileInfo()
        {
            _logger.LogInformation("call GetLastDownloadFileInfo");
            return await _publicClient.GetLastDownloadFileInfo();
        }


        [HttpPost("DownloadFiasFile")]
        public async Task<bool> DownloadFile(
            [FromHeader(Name = "X-correlationID")] Guid correlationId, 
            [FromBody]DownloadFileCmd cmd,
            [FromServices]DownloadServiceQueue queue)
        {
            _logger.LogInformation($"Request to DownloadFile {cmd.DownloadFileURL} correlationId = {correlationId}");
            await queue.Enqueue((cmd.DownloadFileURL, correlationId));
            return true;            
        }

        [HttpGet("Alive")]
        public string Alive()
        {
            _logger.LogInformation("call Alive");
            return "I'm really alive";
        }        
    }
}