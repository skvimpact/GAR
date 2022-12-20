using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowControl;
using GarPublicClient;
using GarServices;
using Microsoft.Extensions.Logging;
using ServiceLayer;

namespace GarPuller.ServiceLayer
{
    public class GarFileService
    {
        private readonly FlowDbAccess _access;
        private readonly PublicClient _publicClient;
        private readonly ILogger<GarFileService> _logger;
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

        public GarFile? GarFileToDownload {
            get {
                var file = GarFileToHandle;
                if (file is null)
                    return null;
                if (file.DownloadRequestedAt is not null) 
                    return null;                  
                return file;
            }
        }
        public GarFileService(FlowDbAccess access, PublicClient publicClient, DownloadService downloadService, ILogger<GarFileService> logger)
        {
            _access = access;
            _publicClient = publicClient;
            _downloadService = downloadService;
            _logger = logger;
        }
        public IEnumerable<GarFile> List() =>
            _access.GarFiles;
        public  bool UpdateList()
        {
                var existingDates = _access.GarFiles
                    .Select(f => f.Date).
                    ToArray();
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
         public async Task<GarFile> UpdateWhenDownloadRequested(Guid correlationId) 
            => await _access.UpdateFile(correlationId, UpdateMode.SetDownloadRequestedAt);
        public async Task<GarFile> UpdateWhenDownloadHanged(Guid correlationId)
            => await _access.UpdateFile(correlationId, UpdateMode.ResetDownloadRequestedAt);
        public async Task <GarFile> UpdateWhenDownloaded(Guid correlationId, string path)
            => await _access.UpdateFile(correlationId, UpdateMode.SetDownloadedAt, path);
        public async Task<GarFile> UpdateWhenProcessRequested(Guid correlationId)
            => await _access.UpdateFile(correlationId, UpdateMode.SetProcessRequestedAt);
        public async Task<GarFile> UpdateWhenProcessHanged(Guid correlationId)
            => await _access.UpdateFile(correlationId, UpdateMode.ResetProcessRequestedAt);            
        public async Task<GarFile> UpdateWhenProcessed(Guid correlationId)
            => await _access.UpdateFile(correlationId, UpdateMode.SetProcessedAt);

        public async Task<GarFile?> ResetHangedDownloadState() {
            var file = GarFileToHandle;
            if (file is null)    
                return null;
            if (file.DownloadRequestedAt is null)
                return null;
            if (file.DownloadedAt is not null)
                return null;                
            if (DateTime.Now - file.DownloadRequestedAt < TimeSpan.FromMinutes(5))
                return null; 

            return await UpdateWhenDownloadHanged(file.CorrelationId);
        }     
        public async Task<GarFile?> ResetHangedProcessState() {
            var file = GarFileToHandle;
            if (file is null)    
                return null;
            if (file.DownloadedAt is null)
                return null;

            return await UpdateWhenProcessHanged(file.CorrelationId);
        }            
        public async Task<bool> ClearControlTable()
        {
            await _access.ClearControlTable();
            return true;
        }         
    }
}