using Microsoft.AspNetCore.Mvc;
using GarPublicClient;

namespace GarProxy.Controllers
{
    [ApiController]
    [Route("WebServices/[controller]")]
    public class PublicController : ControllerBase
    {
        private readonly ILogger<PublicController> _logger;
        private readonly PublicClient _publicClient;
        public PublicController(ILogger<PublicController> logger, PublicClient publicClient)
        {
            _logger = logger;
            _publicClient = publicClient;
        }

        [HttpGet("GetAllDownloadFileInfo")]
        public async Task<IActionResult> GetAllDownloadFileInfo()
        {
            _logger.LogInformation("call GetAllDownloadFileInfo");
            return Ok(await _publicClient.GetAllDownloadFileInfo());
        }

        [HttpGet("GetLastDownloadFileInfo")]
        public async Task<IActionResult> GetLastDownloadFileInfo()
        {
            _logger.LogInformation("call GetLastDownloadFileInfo");
            return Ok(await _publicClient.GetLastDownloadFileInfo());
        }

        [HttpPost("DownloadFiasFile")]
        public async Task<IActionResult> DownloadFile(
            [FromHeader(Name = "X-correlationID")] Guid correlationId, 
            [FromBody]DownloadFileCmd cmd,
            [FromServices]DownloadServiceQueue queue)
        {
            _logger.LogInformation($"POST DownloadFiasFile {cmd.DownloadFileURL} correlationId = {correlationId}");
            await queue.Enqueue((cmd.DownloadFileURL, correlationId));
            return Ok();            
        }       
    }
}