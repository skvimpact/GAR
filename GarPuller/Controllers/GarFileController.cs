using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FlowControl;
using GarPublicClient;
using GarPuller.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GarPuller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GarFileController : ControllerBase
    {
        private readonly ILogger<GarFileController> _logger;
        private readonly GarFileService _service;

        public GarFileController(ILogger<GarFileController> logger, GarFileService service)
        {
            _logger = logger;
            _service = service;
        }    

        [HttpGet("List")]
        public IEnumerable<GarFile> List()
        {
            return _service.List();
        }    

        [HttpGet("UpdateList")]
        public IActionResult UpdateList()
        {
            _service.UpdateList();
            return Ok();
        }

        [HttpGet("Download")]
        public async Task<bool> Download()
        {
            return await _service.DownloadFile();
        }

        [HttpGet("Process")]
        public async Task<bool> Process()
        {
            return await _service.ProcessFile();
        }

        [HttpGet("Clear")]
        public async Task<bool> ClearControlTable()
        {
            return await _service.ClearControlTable();
        }

        [HttpPost("PutDownloadFIASFile")]
        public async Task<bool> PutDownloadedFile(
            [FromHeader(Name = "X-correlationID")] Guid correlationId, 
            [FromBody]PutDownloadedFileCmd cmd)
        {

            _logger.LogInformation($"PutDownloadedFile {cmd.filePath} correlationId = {correlationId}"); 
            await _service.PutDownloadedFile(correlationId, cmd.filePath);            
            return true;
        }      
    }
}