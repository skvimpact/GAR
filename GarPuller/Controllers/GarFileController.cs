using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FlowControl;
using GarPublicClient;
using GarPuller.ServiceLayer;
using GarPuller.Queue;
using GarServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.EventLog;
using System.Diagnostics;

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

// curl -X GET 'http://localhost:5125/GarFile/List'
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

/*         [HttpGet("Download")]
        public async Task<ServiceState> Download()
        {
            return await _service.DownloadFile();
        }

        [HttpGet("Process")]
        public async Task<ServiceState> Process()
        {
            return await _service.ProcessFile();
        } */

// curl -X GET 'http://localhost:5125/GarFile/Go'
        [HttpGet("Go")]
        public async Task<IActionResult> Go([FromServices] GarPullerGoQueue queue)
        {
            _logger.LogInformation("attempt to Go");
            await queue.Start(); // zzz
            return Ok("GarPuller.GarFile.Get.Go");
        }
        [HttpGet("Clear")]
        public async Task<bool> ClearControlTable()
        {
            return await _service.ClearControlTable();
        }

// curl -H "Content-Type: application/json" -X POST 'http://localhost:5125/GarFile/PutDownloadFIASFile' -H 'X-correlationID: c4580c5495344951856fd0c16785fc90' -d '{"filePath":"\\\\NetShare\\NetFolder\\AA.zip"}'
        [HttpPost("PutDownloadFIASFile")]
        public async Task<IActionResult> PutDownloadedFile(
            [FromHeader(Name = "X-correlationID")] Guid correlationId, 
            [FromBody]PutDownloadedFileCmd cmd,
            [FromServices] GarPullerDownloadedQueue queue)
        {


string message = "This is a test message.";
using (EventLog eventLog = new EventLog("Application"))
{
    eventLog.Source = "Application";
    eventLog.WriteEntry(message, EventLogEntryType.Information);
}


            _logger.LogInformation($"PutDownloadedFile {cmd.filePath} correlationId = {correlationId}"); 
            //await _service.PutDownloadedFile(correlationId, cmd.filePath);  
            await queue.Enqueue((cmd.filePath, correlationId));
            return Ok($"GarPuller.GarFile.Post.PutDownloadFIASFile. filePath = {cmd.filePath}. correlationId = {correlationId}");
        }      
    }
}