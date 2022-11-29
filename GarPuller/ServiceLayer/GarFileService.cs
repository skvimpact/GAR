using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowControl;
using GarPublicClient;
using ServiceLayer;

namespace GarPuller.ServiceLayer
{
    public class GarFileService
    {
        private readonly FlowDbAccess _access;
        private readonly PublicClient _publicClient;
        private readonly DownloadService _downloadService;
        public DateTime LastProcessedDate =>
            _access.GarFiles
                .Where(f => f.ProcessedAt != null)
                .OrderBy(f => f.Date)
                .LastOrDefault()?.Date ?? DateTime.MinValue;
        public GarFile? GarFileToHandle =>
            _access.GarFiles
                .Where(f => f.Date > LastProcessedDate) 
                .OrderBy(f => f.Date)
                .FirstOrDefault();   
        //public GarFile? GarFileToProcess =>
        //    _access.GarFiles
        //        .Where(f => f.Date > LastProcessedDate 
        //            && f.DownloadedAt != null)
        //        .OrderBy(f => f.Date)
        //        .FirstOrDefault();                  
        public GarFileService(FlowDbAccess access, PublicClient publicClient, DownloadService downloadService)
        {
            _access = access;
            _publicClient = publicClient;
            _downloadService = downloadService;
        }

        public IEnumerable<GarFile> List() =>
            _access.GarFiles;
        public  bool UpdateList()
        {
			var existingDates = _access.GarFiles
				.Select(f => f.Date).
                ToArray();
                //var f = await _client.GetAllDownloadFileInfo();
			var serverGarFileList = _publicClient.GetAllDownloadFileInfo().Result?
				.Where(f => !string.IsNullOrEmpty(f.GarXMLDeltaURL))?
				.AsQueryable();
			var newGarFiles = serverGarFileList?				
				.MapDownloadFileInfoToGarFile()
				.Where(f => !existingDates.Contains(f.Date))?
                .ToArray();
            if (newGarFiles != null)
		        _access.AddRange(newGarFiles);

            return true;
        }

        public async Task<bool> DownloadFile()
        {
            bool result = false;
            var file = GarFileToHandle;
            if (file != null && file.DownloadRequestedAt == null && file.DownloadedAt == null)
            {
                result = await _publicClient.DownloadFiasFile(file.DeltaUrl, file.CorrelationId);
                if (result)
                    await _access.UpdateWhenDownloadRequested(file.CorrelationId);
            }
            return result;
        }

        public async Task<bool> ProcessFile()
        {
            var file = GarFileToHandle;
            if (file != null && file.DownloadedAt != null && file.ProcessRequestedAt == null)
            {
                await _access.UpdateWhenProcessRequested(file.CorrelationId);
                _downloadService.HandleZipFile(file.LocalPath);
                await _access.UpdateWhenProcessed(file.CorrelationId);
            }
            return true;
        }   
        public async Task<bool> ClearControlTable()
        {
            await _access.ClearControlTable();
            return true;
        }     
        public async Task<GarFile> PutDownloadedFile(
            Guid correlationId, 
            string filePath)
        {            
            return await _access.UpdateWhenDownloaded(correlationId, filePath);
        }        
    }
}